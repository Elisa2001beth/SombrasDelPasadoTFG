using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;  // Referencia al transform del personaje
    public Vector3 offset;             // Distancia entre la cámara y el personaje

    void Update()
    {
        // Actualizar la posición de la cámara basándose en la posición del personaje y el offset
        transform.position = playerTransform.position + offset;
    }
}

