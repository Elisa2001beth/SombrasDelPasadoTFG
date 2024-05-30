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
        
    }

    public void ZoomBack()
    {
        
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
