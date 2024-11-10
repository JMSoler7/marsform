using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; } // Singleton instance

    private string saveFilePath = "gameSave.json"; // Ruta del archivo donde se guardará el estado del juego

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener SaveManager en todas las escenas
        }
        else
        {
            Destroy(gameObject); // Eliminar duplicados si ya existe una instancia
        }
    }

    // Guardar el estado del juego
    public void SaveGame(GameState gameState)
    {
        if (gameState == null)
        {
            Debug.LogError("gameState is null!");
            return;
        }
        string json = JsonUtility.ToJson(gameState, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved!");
    }

    // Cargar el estado del juego
    public GameState LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameState gameState = JsonUtility.FromJson<GameState>(json);
            FindObjectOfType<GridGenerator>().GenerateGrid(load: true);
            Debug.Log("Game loaded!");
            return gameState;
        }
        else
        {
            Debug.Log("No save file found.");
            return null;
        }
    }

    public void SaveAllParcelStates()
    {
        ParcelInteraction[] allParcels = FindObjectsOfType<ParcelInteraction>();
        List<ParcelState> parcelStates = new List<ParcelState>();

        foreach (var parcel in allParcels)
        {
            if (parcel != null && parcel.currentParcelState != null)
            {
                parcelStates.Add(parcel.currentParcelState);
            }
        }

        GameState gameState = new GameState();
        gameState.parcels = parcelStates;
        SaveGame(gameState);

        Debug.Log("All parcels saved to JSON!");
    }

    public void LoadAllParcelStates()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameState gameState = JsonUtility.FromJson<GameState>(json);
            Debug.Log("Game loaded!");

            if (gameState != null && gameState.parcels != null)
            {
                ParcelInteraction[] allParcels = FindObjectsOfType<ParcelInteraction>();

                foreach (var parcelState in gameState.parcels)
                {
                    ParcelInteraction parcel = allParcels.FirstOrDefault(p => p.x == parcelState.x && p.y == parcelState.y);

                    if (parcel != null)
                    {
                        parcel.currentParcelState = parcelState;
                        parcel.UpdateParcelColor();
                        Debug.Log($"Loaded parcel at ({parcelState.x}, {parcelState.y}) with state: {parcelState.state}");
                    }
                    else
                    {
                        Debug.LogWarning($"No parcel found at ({parcelState.x}, {parcelState.y}) in the scene.");
                    }
                }

                Debug.Log("All parcels have been loaded from JSON!");
            }
            else
            {
                Debug.LogError("GameState or parcels list is null.");
            }
        }
        else
        {
            Debug.LogError("No save file found at: " + saveFilePath);
        }
    }
}
