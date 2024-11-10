using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class GridGenerator : MonoBehaviour
{
    public GameObject parcelPrefab;
    public GameState gameState;
    public int gridSize = 10;
    private string saveFilePath = "gameSave.json";

    void Start()
    {
        // GenerateGrid();
    }

    public void GenerateGrid(bool load = false)
    {
        float tileWidth = 1f;
        float tileHeight = 1f;

        float offsetX = -(gridSize / 2f) * tileWidth;
        float offsetY = -(gridSize / 2f) * tileHeight;

        int center = gridSize / 2;
        int centerRangeStart = center - 1;
        int centerRangeEnd = center + 1;

        if (gameState == null)
        {
            gameState = new GameState();
        }

        List<ParcelState> loadedParcels = null;

        if (load && File.Exists(saveFilePath))
        {
            // Carga del archivo JSON si `load` es true
            string json = File.ReadAllText(saveFilePath);
            GameState loadedGameState = JsonUtility.FromJson<GameState>(json);
            loadedParcels = loadedGameState?.parcels;
            Debug.Log("Game loaded!");
        }
        
        if (gameState.parcels == null)
        {
            gameState.parcels = new List<ParcelState>();
        }

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                float parcel_x_position = offsetX + x * tileWidth;
                float parcel_y_position = offsetY + y * tileHeight;
                Vector2 position = new Vector2(parcel_x_position, parcel_y_position);
                GameObject parcel = Instantiate(parcelPrefab, position, Quaternion.identity);
                parcel.name = "Parcel_" + x + "_" + y;
                AdjustParcelScale(parcel);

                ParcelInteraction parcelInteraction = parcel.GetComponent<ParcelInteraction>();
                if (parcelInteraction != null)
                {
                    parcelInteraction.x = x;
                    parcelInteraction.y = y;

                    ParcelState parcelState;

                    if (load && loadedParcels != null)
                    {
                        // Busca el estado en la lista cargada si `load` es true
                        parcelState = loadedParcels.FirstOrDefault(p => p.x == x && p.y == y);

                        if (parcelState == null)
                        {
                            // Si no hay estado guardado para esta parcela, crea uno nuevo
                            parcelState = new ParcelState { x = x, y = y, state = DetermineParcelState(x, y, centerRangeStart, centerRangeEnd) };
                            Debug.LogWarning($"No saved state found for parcel at ({x}, {y}). Using default state.");
                        }
                        else
                        {
                            Debug.Log($"Loaded state for parcel at ({x}, {y}) with state: {parcelState.state}");
                        }
                    }
                    else
                    {
                        // Crea un nuevo estado para una nueva partida
                        parcelState = new ParcelState { x = x, y = y, state = DetermineParcelState(x, y, centerRangeStart, centerRangeEnd) };
                    }

                    parcelInteraction.currentParcelState = parcelState;
                    gameState.parcels.Add(parcelState);
                    parcelInteraction.gameState = gameState;
                    parcelInteraction.UpdateParcelColor();
                }
            }
        }
        Debug.Log("Finished grid generation. Total parcels in gameState: " + gameState.parcels.Count);
        parcelPrefab.SetActive(false);
    }

    // Método para determinar el estado de la parcela
    ParcelCondition DetermineParcelState(int x, int y, int centerRangeStart, int centerRangeEnd)
    {
        if (x >= centerRangeStart && x < centerRangeEnd && y >= centerRangeStart && y < centerRangeEnd)
        {
            return ParcelCondition.Empty; // Parcela central vacía
        }
        else if ((x == centerRangeStart - 1 || x == centerRangeEnd) && (y >= centerRangeStart - 1 && y <= centerRangeEnd) ||
                 (y == centerRangeStart - 1 || y == centerRangeEnd) && (x >= centerRangeStart - 1 && x <= centerRangeEnd))
        {
            return ParcelCondition.Conquerable; // Parcela colindante conquistable
        }
        else
        {
            return ParcelCondition.Unavailable; // Parcela no disponible
        }
    }

    void AdjustParcelScale(GameObject parcel)
    {
        SpriteRenderer spriteRenderer = parcel.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Vector2 spriteSize = spriteRenderer.bounds.size;

            float scaleX = 1f / spriteSize.x;
            float scaleY = 1f / spriteSize.y;

            parcel.transform.localScale = new Vector2(scaleX, scaleY);
        }
    }
}
