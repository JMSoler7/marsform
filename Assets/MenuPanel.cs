using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;
    public Button setBankButton;
    public Button setPatchButton;
    public Button setSoldierButton;
    public Button salirButton;
    private ParcelInteraction selectedParcel;

    void Start()
    {
        setBankButton.onClick.AddListener(OnSetBankButtonClick);
        setPatchButton.onClick.AddListener(OnSetPatchButtonClick);
        setSoldierButton.onClick.AddListener(OnSetSoldierButtonClick);
        salirButton.onClick.AddListener(OnSalirButtonClick);
    }

    void OnSetBankButtonClick()
    {
        if (selectedParcel != null)
        {
            selectedParcel.Bank();
            selectedParcel.ExitParcel();
        }
    }

    void OnSetPatchButtonClick()
    {
        if (selectedParcel != null)
        {
            selectedParcel.Patch();
            selectedParcel.ExitParcel();
        }
    }

    void OnSetSoldierButtonClick()
    {
        if (selectedParcel != null)
        {
            selectedParcel.Soldier();
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
