using UnityEngine;
using System.Linq;

public class ParcelInteraction : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color hoverColor = Color.red;
    public Color unavailableColor = Color.white;

    public ParcelState currentParcelState;
    public Sprite emptySprite;
    public Sprite conquerableSprite;
    public Sprite unavailableSprite;
    public Sprite bankSprite;
    public Sprite patchSprite;
    public Sprite soldierSprite;
    public GameObject menuControllerObject; // Objeto con el script del menú
    private MenuController menuController;

    public int x, y;

    public GameState gameState;
    public SaveManager saveManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        x = (int)transform.position.x; // Asumiendo que la posición X es la columna de la cuadrícula
        y = (int)transform.position.y; // Asumiendo que la posición Y es la fila de la cuadrícula
        if (gameState.parcels == null)
        {
            Debug.LogError("gameState.parcels is not initialized.");
        }   
        menuController = menuControllerObject.GetComponent<MenuController>();

        // Añade un BoxCollider2D si no existe
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
        }

        // Asegúrate de que el collider se ajuste al tamaño del sprite
        collider.size = spriteRenderer.bounds.size;

        //AdjustScale();
        UpdateParcelColor();
    }

    void AdjustScale()
    {
        Vector2 spriteSize = spriteRenderer.bounds.size;
        
        // Verifica que el tamaño del sprite no sea cero para evitar divisiones por cero
        if (spriteSize.x == 0 || spriteSize.y == 0)
        {
            Debug.LogError("El tamaño del sprite es cero en al menos una dimensión, no se puede ajustar la escala.");
            return;
        }

        float scaleX = 1f / spriteSize.x;
        float scaleY = 1f / spriteSize.y;
        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }

    public void UpdateParcelColor()
    {
        if (spriteRenderer != null)
        {
            switch (currentParcelState.state)
            {
                case ParcelCondition.Empty:
                    spriteRenderer.color = Color.white;
                    spriteRenderer.sprite = emptySprite;
                    break;
                case ParcelCondition.Patch:
                    spriteRenderer.color = Color.white;
                    spriteRenderer.sprite = patchSprite;
                    break;
                case ParcelCondition.Bank:
                    spriteRenderer.color = Color.white;
                    spriteRenderer.sprite = bankSprite;
                    break;
                case ParcelCondition.Soldier:
                    spriteRenderer.color = Color.white;
                    spriteRenderer.sprite = soldierSprite;
                    break;
                case ParcelCondition.Conquerable:
                    spriteRenderer.color = Color.white;
                    spriteRenderer.sprite = conquerableSprite;
                    break;
                case ParcelCondition.Unavailable:
                    spriteRenderer.color = Color.white;
                    spriteRenderer.sprite = unavailableSprite;
                    break;
            }
        }
    }

    void OnMouseEnter()
    {
        if (
            !PlayerManager.isMenuOpen
            && spriteRenderer != null
            && (currentParcelState.state == ParcelCondition.Empty || currentParcelState.state == ParcelCondition.Conquerable)
        )
        {
            spriteRenderer.color = hoverColor;
        }
    }

    void OnMouseExit()
    {
        if (!PlayerManager.isMenuOpen)
        {
            UpdateParcelColor();
        }
    }

    void OnMouseDown()
    {
        if (!PlayerManager.isMenuOpen){
            PlayerManager.isMenuOpen = true;
            if (menuController != null)
            {
                menuController.OpenMenu(this);  // Llama al controlador de menú y pasa esta parcela como seleccionada
            }
        }
    }

    public void Bank()
    {
        // Verificar las coordenadas
        Debug.Log("Attempting to bank at coordinates: " + x + ", " + y);

        if (gameState.parcels == null)
        {
            Debug.LogError("gameState.parcels is null!");
            return;
        }

        // Buscar la parcela con las coordenadas correctas
        ParcelState parcelToUpdate = gameState.parcels.FirstOrDefault(parcel => parcel.x == x && parcel.y == y);

        if (parcelToUpdate != null)
        {
            parcelToUpdate.state = ParcelCondition.Bank;
            parcelToUpdate.x = x;
            parcelToUpdate.y = y;

            UpdateParcelColor();

            Debug.Log("Banking parcel at " + x + ", " + y);
            Debug.Log("Current parcel state: " + parcelToUpdate.state);

            saveManager.SaveGame(gameState);
        }
        else
        {
            Debug.LogError("Parcel not found in gameState at (" + x + ", " + y + ")");

            // Imprimir todas las celdas disponibles
            Debug.Log("Listing all available parcels:");
            foreach (var parcel in gameState.parcels)
            {
                Debug.Log($"Parcel at ({parcel.x}, {parcel.y}) with state: {parcel.state}");
            }
        }
        currentParcelState.state = ParcelCondition.Bank;
        UpdateParcelColor();
    }




    public void Patch()
    {
        currentParcelState.state = ParcelCondition.Patch;
        UpdateParcelColor();
        // PlayerManager.Instance.AddFood(1);
    }

    public void Soldier()
    {
        currentParcelState.state = ParcelCondition.Soldier;
        UpdateParcelColor();
    }

    // Método para cerrar el menú de esta parcela
    public void ExitParcel()
    {
        if (menuController != null)
        {
            menuController.CloseMenu();
        }
        PlayerManager.isMenuOpen = false;
    }

    public void Empty()
    {
        currentParcelState.state = ParcelCondition.Empty;
        UpdateParcelColor();
    }
}
