using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SalirJuego : MonoBehaviour
{
    public void Salir()
    {
        FindObjectOfType<SettingsManager>().GuardarConfiguraciones(); // Guardar las configuraciones
        Application.Quit();
        
    }


}
