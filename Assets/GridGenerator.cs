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
    float tileWidth = 1f;  // Ajusta esto si tus parcelas tienen otro tamaño
    float tileHeight = 1f; // Ajusta esto si tus parcelas tienen otro tamaño

    // Centra la cuadrícula alrededor del origen
    float offsetX = -(gridSize / 2f) * tileWidth;
    float offsetY = -(gridSize / 2f) * tileHeight;

    for (int x = 0; x < gridSize; x++)
    {
        for (int y = 0; y < gridSize; y++)
        {
            // Instancia una parcela en la posición correcta
            Vector3 position = new Vector3(offsetX + x * tileWidth, offsetY + y * tileHeight, 0);
            GameObject parcel = Instantiate(parcelPrefab, position, Quaternion.identity);
            parcel.name = "Parcel_" + x + "_" + y;

            SpriteRenderer sr = parcel.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.white;  // Asignar color blanco inicialmente
            }

            BoxCollider2D collider = parcel.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                collider.size = new Vector2(tileWidth, tileHeight);  // Asegura que el collider tenga el tamaño correcto
            }
        }
    }
}

}