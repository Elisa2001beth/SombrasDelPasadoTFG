using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    // Referencias a los controles de la UI
    public Toggle toggleFullScreen;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;

    // Resoluciones disponibles
    Resolution[] resolutions;

    void Start()
    {
       

        // Verificar si los objetos están asignados
        if (toggleFullScreen == null || resolutionDropdown == null || qualityDropdown == null || volumeSlider == null)
        {
            return;
        }

        // Revisar y cargar las resoluciones
        RevisarResolucion();

        // Cargar configuraciones guardadas
        CargarConfiguraciones();

        // Aplicar configuraciones al iniciar
        AplicarConfiguraciones();

        // Agregar eventos OnValueChanged a los controles de la UI
        toggleFullScreen.onValueChanged.AddListener(delegate { ActivarPantallaCompleta(toggleFullScreen.isOn); });
        resolutionDropdown.onValueChanged.AddListener(delegate { CambiarResolucion(resolutionDropdown.value); });
        qualityDropdown.onValueChanged.AddListener(delegate { AdjustQuality(); });
        volumeSlider.onValueChanged.AddListener(delegate { ChangeSlider(volumeSlider.value); });
    }

    void OnApplicationQuit()
    {
        GuardarConfiguraciones();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            GuardarConfiguraciones();
        }
    }

    public void GuardarConfiguraciones()
    {
        // Verificar si los objetos están asignados
        if (toggleFullScreen == null || resolutionDropdown == null || qualityDropdown == null || volumeSlider == null)
        {

            return;
        }

        // Guardar configuraciones en PlayerPrefs
        PlayerPrefs.SetInt("PantallaCompleta", toggleFullScreen.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Resolucion", resolutionDropdown.value);
        PlayerPrefs.SetInt("Calidad", qualityDropdown.value);
        PlayerPrefs.SetFloat("Volumen", volumeSlider.value);

        PlayerPrefs.Save();
        
    }

    public void CargarConfiguraciones()
    {
        // Cargar configuraciones desde PlayerPrefs
        toggleFullScreen.isOn = PlayerPrefs.GetInt("PantallaCompleta", 1) == 1 ? true : false;
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolucion", 0);
        qualityDropdown.value = PlayerPrefs.GetInt("Calidad", 3);
        volumeSlider.value = PlayerPrefs.GetFloat("Volumen", 0.5f);

        // Aplicar configuraciones
        AplicarConfiguraciones();

        Debug.Log("Configuraciones Cargadas");
    }

    public void AplicarConfiguraciones()
    {
        // Aplicar configuraciones a los elementos de la UI
        ActivarPantallaCompleta(toggleFullScreen.isOn);
        CambiarResolucion(resolutionDropdown.value);
        AdjustQuality();
        ChangeSlider(volumeSlider.value);
    }

    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void RevisarResolucion()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string opcion = resolutions[i].width + " x " + resolutions[i].height;
            opciones.Add(opcion);

            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }

        resolutionDropdown.AddOptions(opciones);
        resolutionDropdown.value = resolucionActual;
        resolutionDropdown.RefreshShownValue();
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        if (resolutions == null)
        {
            Debug.LogError("Las resoluciones no están inicializadas.");
            return;
        }

        if (indiceResolucion < 0 || indiceResolucion >= resolutions.Length)
        {

            return;
        }

        Resolution resolucion = resolutions[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    public void AdjustQuality()
    {
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }

    public void ChangeSlider(float valor)
    {
        AudioListener.volume = valor;
    }
}
