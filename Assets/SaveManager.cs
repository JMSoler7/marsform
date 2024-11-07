using UnityEngine;
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
}
