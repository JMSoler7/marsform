using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject parcelPrefab;
    public GameState gameState;
    public int gridSize = 10;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
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

                    ParcelState parcelState = new ParcelState
                    {
                        x = x,
                        y = y,
                        state = DetermineParcelState(x, y, centerRangeStart, centerRangeEnd)
                    };

                    parcelInteraction.currentParcelState = parcelState;
                    gameState.parcels.Add(parcelState);
                    // Debug.Log($"Parcel added to gameState: ({x}, {y})");
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
