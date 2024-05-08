using UnityEngine;

public class EyeBlinkFade : MonoBehaviour
{
    public Material blinkMaterial;
    public float blinkSpeed = 1.0f; // Velocidad de parpadeo
    private float blinkAmount; // Cantidad de parpadeo

    void Update()
    {
        // Incrementar el valor de _BlinkAmount para simular el efecto de parpadeo
        blinkAmount = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);
        // Actualizar el valor del parámetro en el material del shader
        blinkMaterial.SetFloat("_BlinkAmount", blinkAmount);
    }
}
