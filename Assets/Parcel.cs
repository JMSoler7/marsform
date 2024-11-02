using UnityEngine;

public class ParcelInteraction : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color hoverColor = Color.red;
    public Color plantedColor = Color.blue;
    public Color wateredColor = Color.green;
    public Color unavailableColor = Color.white;

    public enum ParcelState
    {
        Empty,
        Conquerable,
        Unavailable,
        Planted,
        Watered,
        Harvested
    }

    public ParcelState currentState = ParcelState.Empty;
    public Sprite emptySprite;
    public Sprite conquerableSprite;
    public GameObject menuControllerObject; // Objeto con el script del menú
    private MenuController menuController;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        menuController = menuControllerObject.GetComponent<MenuController>();

        // Añade un BoxCollider2D si no existe
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
        }

        // Asegúrate de que el collider se ajuste al tamaño del sprite
        collider.size = spriteRenderer.bounds.size;

        AdjustScale();
        UpdateParcelColor();
    }

    void AdjustScale()
    {
        Vector2 spriteSize = spriteRenderer.bounds.size;
        float scaleX = 1f / spriteSize.x;
        float scaleY = 1f / spriteSize.y;
        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }

    public void UpdateParcelColor()
    {
        if (spriteRenderer != null)
        {
            switch (currentState)
            {
                case ParcelState.Empty:
                    spriteRenderer.color = Color.white;
                    if (emptySprite != null)
                    {
                        spriteRenderer.sprite = emptySprite;
                    }
                    break;
                case ParcelState.Planted:
                    spriteRenderer.color = plantedColor;
                    break;
                case ParcelState.Watered:
                    spriteRenderer.color = wateredColor;
                    break;
                case ParcelState.Harvested:
                    spriteRenderer.color = Color.yellow; // ejemplo para cosechado
                    break;
                case ParcelState.Conquerable:
                    spriteRenderer.color = Color.white;
                    if (conquerableSprite != null)
                    {
                        spriteRenderer.sprite = conquerableSprite;
                    }
                    break;
                case ParcelState.Unavailable:
                    spriteRenderer.color = unavailableColor;
                    break;
            }
        }
    }

    void OnMouseEnter()
    {
        if (
            !PlayerManager.isMenuOpen
            && spriteRenderer != null
            && (currentState == ParcelState.Empty || currentState == ParcelState.Conquerable)
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

    // Método para plantar en esta parcela
    public void Plant()
    {
        Debug.Log("Planting on parcel: " + gameObject.name);
        Debug.Log("Plant action called");
        currentState = ParcelState.Planted;
        UpdateParcelColor();
        PlayerManager.Instance.AddFood(1);
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
}
