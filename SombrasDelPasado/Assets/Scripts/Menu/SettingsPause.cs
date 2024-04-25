using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPause : MonoBehaviour
{
    /* public OptionsController panelOpciones;

    // Start is called before the first frame update
    void Start()
    {
        panelOpciones = GameObject.FindGameObjectWithTag("Opciones").GetComponent<OptionsController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {MostrarOpciones();}
        
    }

    public void MostrarOpciones(){
        panelOpciones.pantallaOpciones.SetActive(true);
    } */

//////////////////////////////////////////////////////////////////////////

    /* public GameObject menuPausa;



    private bool menuOn;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            menuOn = !menuOn;
        }

        if(menuOn==true){
            menuPausa.SetActive(true);
            Time.timeScale = 0;
        }
        else{
            menuPausa.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Continuar(){
        menuPausa.SetActive(false);
        menuOn = false;
        Time.timeScale = 1;

    } */

   


    public GameObject menuPausa;
    public GameObject panelOpciones;
    public GameObject panelControles;
    
    private bool menuOn;

    void Start()
    {
        // Oculta el menú de pausa y los paneles al inicio
        menuPausa.SetActive(false);
        panelOpciones.SetActive(false);
        panelControles.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }

        if (menuOn)
        {
            Time.timeScale = 0; // Pausa el juego
        }
        else
        {
            Time.timeScale = 1; // Reanuda el juego
        }
    }

    public void ToggleMenu()
    {
        menuOn = !menuOn;

        if (menuOn)
        {
            menuPausa.SetActive(true);
            panelOpciones.SetActive(false);
            panelControles.SetActive(false);
        }
        else
        {
            menuPausa.SetActive(false);
        }
    }

    public void MostrarOpciones()
    {
        FindObjectOfType<SettingsManager>().AplicarConfiguraciones(); // Aplicar las configuraciones actuales
        panelOpciones.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void MostrarControles()
    {
        panelControles.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Continuar()
    {
        menuPausa.SetActive(false);
        menuOn = false;
        Time.timeScale = 1; // Reanuda el juego
    }

    public void VolverMenu()
    {
        panelOpciones.SetActive(false);
        panelControles.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void SalirJuego()
    {
        FindObjectOfType<SettingsManager>().GuardarConfiguraciones(); // Guardar las configuraciones
        Application.Quit();
    }


    
       


    
}
