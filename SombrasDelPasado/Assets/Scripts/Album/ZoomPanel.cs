using UnityEngine;
using UnityEngine.UI;

public class ZoomPanel : MonoBehaviour
{
    [SerializeField] Image mainImage; // Imagen principal
    [SerializeField] Button backButton; // Botón de retroceso
    [SerializeField] Button forwardButton; // Botón de avance

    public void SetImage(Sprite sprite)
    {
        mainImage.sprite = sprite;
    }

    public void ZoomForward()
    {
        // Aquí puedes implementar la lógica para avanzar a la siguiente imagen
    }

    public void ZoomBack()
    {
        // Aquí puedes implementar la lógica para retroceder a la imagen anterior
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
