
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Album : MonoBehaviour
{
    [SerializeField] List<Transform> pages;
    [SerializeField] List<Sprite> images; // Lista de imágenes para el zoom
    [SerializeField] List<string> dialogues; // Lista de diálogos para cada imagen

    // Panel y elementos para el zoom
    [SerializeField] GameObject zoomPanel;
    [SerializeField] Image zoomImage;
    [SerializeField] Button zoomForwardButton;
    [SerializeField] Button zoomBackButton;
    [SerializeField] TMP_Text zoomDialogText; // Texto del diálogo
    private int zoomIndex = 0;

 
    private void Start()
    {
        InitialState();
    }

    public void InitialState()
    {
        zoomPanel.SetActive(false); // Ocultar el panel de zoom al iniciar
        zoomForwardButton.onClick.AddListener(ZoomForward);
        zoomBackButton.onClick.AddListener(ZoomBack);
        UpdateZoomButtons();
    }

    // Mostrar el panel de zoom con la imagen seleccionada
    public void ShowZoomPanel(int imageIndex)
    {
        zoomIndex = imageIndex;
        zoomImage.sprite = images[zoomIndex];
        zoomDialogText.text = ""; // Inicializar el texto
        zoomPanel.SetActive(true);
        StartCoroutine(TypeDialogue(dialogues[zoomIndex])); // Iniciar efecto de máquina de escribir
        UpdateZoomButtons();
        
    }

    private void UpdateZoomButtons()
    {
        zoomForwardButton.interactable = zoomIndex < images.Count - 1;
        zoomBackButton.interactable = zoomIndex > 0;
        
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        foreach (char letter in dialogue.ToCharArray())
        {
            zoomDialogText.text += letter;
            yield return new WaitForSeconds(0.05f); // Tiempo de espera entre letras
        }
    }

    


    public void ZoomForward()
    {
        if (zoomIndex < images.Count - 1)
        {
            zoomIndex++;
            Debug.Log("Zoom Index: " + zoomIndex);
            Debug.Log("Image Count: " + images.Count);
            UpdateZoomContent();
        }
    }

    public void ZoomBack()
    {
        if (zoomIndex > 0)
        {
            zoomIndex--;
            
            UpdateZoomContent();
        }
    }

    private void UpdateZoomContent()
    {
        zoomImage.sprite = images[zoomIndex];
        zoomDialogText.text = ""; // Reiniciar el texto
        StartCoroutine(TypeDialogue(dialogues[zoomIndex])); // Iniciar efecto de máquina de escribir
        UpdateZoomButtons();
    }

    public void CloseZoomPanel()
    {
        zoomPanel.SetActive(false);
    }

    // Métodos para manejar los clics en las imágenes
    public void OnImageClick(int imageIndex)
    {
        ShowZoomPanel(imageIndex);
    }
}
