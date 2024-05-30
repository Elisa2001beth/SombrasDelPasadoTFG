



using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Album : MonoBehaviour
{
    public Image[] smallImages; // Array de imágenes pequeñas
    public Sprite[] largeImages; // Array de imágenes grandes
    public string[] imageTexts; // Array de textos asociados a las imágenes

    public GameObject panel; // Panel que contiene la imagen ampliada y el texto
    public Image largeImageDisplay; // Componente Image para mostrar la imagen ampliada
    public TMP_Text imageTextDisplay; // Componente Text para mostrar el texto asociado
    public Button nextButton; // Botón para ir a la siguiente imagen
    public Button prevButton; // Botón para ir a la imagen anterior
    public Button closeButton; // Botón para cerrar el panel

    private int currentIndex;
    private Coroutine typingCoroutine;

    void Start()
    {
        // Asignar eventos de clic a cada imagen pequeña
        for (int i = 0; i < smallImages.Length; i++)
        {
            int index = i;
            smallImages[i].GetComponent<Button>().onClick.AddListener(() => OnImageClick(index));
        }

        // Asignar eventos a los botones del panel
        nextButton.onClick.AddListener(OnNextButtonClick);
        prevButton.onClick.AddListener(OnPrevButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);

        panel.SetActive(false); // Ocultar el panel al inicio
    }

    void OnImageClick(int index)
    {
        currentIndex = index;
        ShowImage();
    }

    void ShowImage()
    {
        largeImageDisplay.sprite = largeImages[currentIndex];

        // Si hay una corrutina de tipeo en curso, la detenemos antes de iniciar una nueva
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(imageTexts[currentIndex]));

        panel.SetActive(true);
        UpdateButtons();
    }

    IEnumerator TypeText(string text)
    {
        imageTextDisplay.text = "";
        foreach (char letter in text.ToCharArray())
        {
            imageTextDisplay.text += letter;
            yield return new WaitForSeconds(0.05f); // Tiempo de espera entre cada letra (ajustable)
        }
    }

    void OnNextButtonClick()
    {
        if (currentIndex < largeImages.Length - 1)
        {
            currentIndex++;
            ShowImage();
        }
    }

    void OnPrevButtonClick()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowImage();
        }
    }

    void OnCloseButtonClick()
    {
        panel.SetActive(false);
    }

    void UpdateButtons()
    {
        nextButton.interactable = currentIndex < largeImages.Length - 1;
        prevButton.interactable = currentIndex > 0;
    }
}
