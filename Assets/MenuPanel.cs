using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;
    public Button plantarButton;
    public Button salirButton;
    private ParcelInteraction selectedParcel;

    void Start()
    {
        plantarButton.onClick.AddListener(OnPlantarButtonClick);
        salirButton.onClick.AddListener(OnSalirButtonClick);
    }

    void OnPlantarButtonClick()
    {
        if (selectedParcel != null)
        {
            Debug.Log("Plantar seleccionado");
            selectedParcel.Plant();  // Llama al método de la parcela seleccionada
            selectedParcel.ExitParcel();
        }
    }

    public void OpenMenu(ParcelInteraction parcel)
    {
        selectedParcel = parcel;
        menuPanel.SetActive(true);
    }

    void OnSalirButtonClick()
    {
        Debug.Log("Salir seleccionado");
        // Application.Quit();
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        selectedParcel = null;  // Limpia la referencia a la parcela seleccionada
    }
}
