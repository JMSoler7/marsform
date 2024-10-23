using UnityEngine;

public class ParcelInteraction : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Color defaultColor = Color.white;
    public Color hoverColor = Color.red;

    void Start()
    {
        // Obtenemos el SpriteRenderer del objeto
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Aseguramos que la parcela empiece con el color blanco
        if (spriteRenderer != null)
        {
            spriteRenderer.color = defaultColor;
        }
        // Hide it
    }

    // Este evento se dispara cuando el ratón entra en el objeto
    void OnMouseEnter()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hoverColor;  // Cambiar el color a rojo cuando el ratón esté encima
        }
    }

    // Este evento se dispara cuando el ratón sale del objeto
    void OnMouseExit()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = defaultColor;  // Volver al color blanco cuando el ratón salga
        }
    }
}
