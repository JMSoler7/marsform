using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject parcelPrefab;
    public int gridSize = 3;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        float tileWidth = 1f;
        float tileHeight = 1f;

        float offsetX = -(gridSize / 2f) * tileWidth;
        float offsetY = -(gridSize / 2f) * tileHeight;

        Debug.Log("offsetX: " + offsetX);
        Debug.Log("offsetY: " + offsetY);

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                float parcel_x_position = offsetX + x * tileWidth;
                float parcel_y_position = offsetY + y * tileHeight;
                Vector3 position = new Vector3(parcel_x_position, parcel_y_position, 0);
                Debug.Log("Parcel position: " + position);
                GameObject parcel = Instantiate(parcelPrefab, position, Quaternion.identity);
                parcel.name = "Parcel_" + x + "_" + y;

                AdjustParcelScale(parcel);

                ChangeParcelColor(parcel, Color.white);
            }
        }
    }

    void AdjustParcelScale(GameObject parcel)
    {
        SpriteRenderer spriteRenderer = parcel.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Vector2 spriteSize = spriteRenderer.bounds.size;

            float scaleX = 1f / spriteSize.x;
            float scaleY = 1f / spriteSize.y;

            parcel.transform.localScale = new Vector3(scaleX, scaleY, 1f);
        }
    }

    void ChangeParcelColor(GameObject parcel, Color color)
    {
        SpriteRenderer spriteRenderer = parcel.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer found on " + parcel.name);
        }
    }
}
