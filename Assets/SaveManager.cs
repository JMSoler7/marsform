using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private string saveFilePath = "gameSave.json"; // Ruta del archivo donde se guardará el estado del juego

    // Guardar el estado del juego
    public void SaveGame(GameState gameState)
    {
        if (gameState == null)
        {
            Debug.LogError("gameState is null!");
            return;
        }
        string json = JsonUtility.ToJson(gameState, true); // Serializa a JSON (con formato legible)
        File.WriteAllText(saveFilePath, json); // Escribe el JSON en el archivo
        Debug.Log("Game saved!");
    }

    // Cargar el estado del juego
    public GameState LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath); // Lee el archivo JSON
            GameState gameState = JsonUtility.FromJson<GameState>(json); // Deserializa el JSON
            Debug.Log("Game loaded!");
            return gameState;
        }
        else
        {
            Debug.Log("No save file found.");
            return null; // Si no existe el archivo, retorna null
        }
    }

    public void SaveAllParcelStates()
    {
        // Recoger todas las parcelas de la escena
        ParcelInteraction[] allParcels = FindObjectsOfType<ParcelInteraction>();

        // Crear una lista para guardar el estado de las parcelas
        List<ParcelState> parcelStates = new List<ParcelState>();

        // Recorrer todas las parcelas y añadir sus estados a la lista
        foreach (var parcel in allParcels)
        {
            if (parcel != null && parcel.currentParcelState != null)
            {
                // Añadir el estado de cada parcela a la lista
                parcelStates.Add(parcel.currentParcelState);
            }
        }

        // Crear un objeto GameState y asignar la lista de parcelas
        GameState gameState = new GameState();
        gameState.parcels = parcelStates;

        // Guardar el estado del juego (con las parcelas) en un archivo JSON
        SaveGame(gameState);

        Debug.Log("All parcels saved to JSON!");
    }

    public void LoadAllParcelStates()
    {
        // Verificar si el archivo existe
        if (File.Exists(saveFilePath))
        {
            // Leer el contenido del archivo JSON
            string json = File.ReadAllText(saveFilePath); // Lee el archivo JSON

            // Deserializar el JSON a un objeto GameState
            GameState gameState = JsonUtility.FromJson<GameState>(json); // Deserializa el JSON
            Debug.Log("Game loaded!");

            // Comprobar si gameState y su lista de parcelas son válidas
            if (gameState != null && gameState.parcels != null)
            {
                // Recoger todas las parcelas de la escena
                ParcelInteraction[] allParcels = FindObjectsOfType<ParcelInteraction>();

                // Recorrer todas las parcelas cargadas desde el archivo JSON
                foreach (var parcelState in gameState.parcels)
                {
                    // Buscar la parcela correspondiente en la escena
                    ParcelInteraction parcel = allParcels.FirstOrDefault(p => p.x == parcelState.x && p.y == parcelState.y);

                    if (parcel != null)
                    {
                        // Actualizar el estado de la parcela con el estado cargado desde JSON
                        parcel.currentParcelState = parcelState;

                        // Actualizar el color de la parcela (puedes agregar este método si quieres)
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
