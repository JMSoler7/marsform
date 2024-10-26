using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;
    public Button plantarButton;
    public Button salirButton;

    void Start()
    {
        plantarButton.onClick.AddListener(OnPlantarButtonClick);
        salirButton.onClick.AddListener(OnSalirButtonClick);
    }

    void OnPlantarButtonClick()
    {
        Debug.Log("Plantar seleccionado");
    }

    void OnSalirButtonClick()
    {
        Debug.Log("Salir seleccionado");
        // Application.Quit();
    }
}
