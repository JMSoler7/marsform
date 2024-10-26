using UnityEngine;

public class ParcelInteraction : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Color defaultColor = Color.white;
    public Color hoverColor = Color.red;

    public GameObject menuPanel;

    void AdjustScale()
    {
        Vector2 spriteSize = spriteRenderer.bounds.size;
        float scaleX = 1f / spriteSize.x;
        float scaleY = 1f / spriteSize.y;
        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
    void HideParcel()
    {
        spriteRenderer.enabled = false;
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = defaultColor;
        }
        if (GetComponent<BoxCollider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
        AdjustScale();
    }

    void OnMouseEnter()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hoverColor;
        }
    }

    void OnMouseExit()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = defaultColor;
        }
    }
    void OnMouseDown()
    {
        Debug.Log("Parcela clicada: " + gameObject.name);
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }
    }

    public void Plant()
    {
        Debug.Log("Plantar en la parcela: " + gameObject.name);
        // menuPanel.SetActive(false);  // Cerrar el menú después de plantar
    }

    public void ExitParcel()
    {
        Debug.Log("Salir del menú de la parcela: " + gameObject.name);
        menuPanel.SetActive(false);  // Cerrar el menú al salir
    }
}
