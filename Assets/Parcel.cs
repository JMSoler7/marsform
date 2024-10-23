using UnityEngine;

public class ParcelInteraction : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Color defaultColor = Color.white;
    public Color hoverColor = Color.red;

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
        HideParcel();
    }
}
