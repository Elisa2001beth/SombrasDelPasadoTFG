using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelCombate : MonoBehaviour
{
    public GameObject panelCombate;


    private PlayerController playerController;

    // Array de objetos a desactivar/activar
    public GameObject[] objetosADesactivar;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

    }

    public void IniciarCombate()
    {
        // Desactivar objetos de la escena
        foreach (GameObject objeto in objetosADesactivar)
        {
            objeto.SetActive(false);
        }

        panelCombate.SetActive(true);

        // Restringir movimientos del jugador
        playerController.puedeMoverse = false;
    }

    public void FinalizarCombate()
    {
        playerController.puedeMoverse = true;
    }

    
    
}

