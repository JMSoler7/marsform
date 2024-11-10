using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para salir del juego en el editor y aplicaciones

public class MainMenu : MonoBehaviour
{
    public SaveManager saveManager;
    public GridGenerator gridGenerator;

    private void Start()
    {
        // Mostrar el menú al iniciar el juego
        gameObject.SetActive(true);
    }

    public void OnContinueButtonClick()
    {
        if (SaveManager.Instance != null)
        {
            Debug.Log("Loading game!");
            SaveManager.Instance.LoadGame();
        }
        else
        {
            Debug.LogError("SaveManager instance not found.");
        }

        gameObject.SetActive(false);
    }

    public void OnNewGameButtonClick()
    {
        // Llamar al método para generar un nuevo grid
        if (gridGenerator != null)
        {
            gridGenerator.GenerateGrid();
        }

        // Ocultar el menú principal
        gameObject.SetActive(false);
    }

    public void OnExitButtonClick()
    {
        // Salir del juego o detener el editor si estamos en el modo de prueba
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
