using UnityEngine;

public class ParcelInteraction : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color hoverColor = Color.red;
    public Color unavailableColor = Color.white;

    public enum ParcelState
    {
        Empty,
        Bank,
        Patch,
        Soldier,
        Conquerable,
        Unavailable
    }

    public ParcelState currentState = ParcelState.Empty;
    public Sprite emptySprite;
    public Sprite conquerableSprite;
    public Sprite unavailableSprite;
    public Sprite bankSprite;
    public Sprite patchSprite;
    public Sprite soldierSprite;
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
                    spriteRenderer.sprite = emptySprite;
                    break;
                case ParcelState.Patch:
                    spriteRenderer.color = Color.white;
                    spriteRenderer.sprite = patchSprite;
                    break;
                case ParcelState.Bank:
                    spriteRenderer.color = Color.white;
                    spriteRenderer.sprite = bankSprite;
                    break;
                case ParcelState.Soldier:
                    spriteRenderer.color = Color.white;
                    spriteRenderer.sprite = soldierSprite;
                    break;
                case ParcelState.Conquerable:
                    spriteRenderer.color = Color.white;
                    spriteRenderer.sprite = conquerableSprite;
                    break;
                case ParcelState.Unavailable:
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

    public void Bank()
    {
        currentState = ParcelState.Bank;
        UpdateParcelColor();
    }

    public void Patch()
    {
        currentState = ParcelState.Patch;
        UpdateParcelColor();
        // PlayerManager.Instance.AddFood(1);
    }

    public void Soldier()
    {
        currentState = ParcelState.Soldier;
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
        currentState = ParcelState.Empty;
        UpdateParcelColor();
    }
}
