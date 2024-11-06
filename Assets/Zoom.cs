using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera mainCamera;               // La cámara que vamos a controlar
    public float zoomSpeed = 1f;            // Velocidad del zoom (ajustable)
    public float minZoom = 5f;              // Zoom mínimo (valor de `orthographicSize`)
    public float maxZoom = 20f;             // Zoom máximo (valor de `orthographicSize`)

    void Update()
    {
        // Obtener la cantidad de scroll del ratón (puedes ajustar la sensibilidad aquí)
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Si el input de scroll es mayor o menor que 0, se hace zoom in o zoom out
        if (scrollInput != 0f)
        {
            // Cambiar el tamaño del zoom en función de la dirección del scroll
            mainCamera.orthographicSize -= scrollInput * zoomSpeed;

            // Asegurarse de que el zoom esté entre los valores mínimo y máximo
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);
        }
    }
}
