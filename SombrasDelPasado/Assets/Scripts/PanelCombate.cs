/* using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelCombate : MonoBehaviour
{
    public GameObject panelCombate;
    //public TMP_Text textoAccion;
    //public Button botonAtacar;
    //public Button botonDefender;
    //public Button botonHuir;
    //public Button botonUsarObjeto; // Botón para usar un objeto del inventario

    private PlayerController playerController;

    // Array de objetos a desactivar/activar
    public GameObject[] objetosADesactivar;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        // Asignar el método UsarObjeto al botón botonUsarObjeto
        //botonUsarObjeto.onClick.AddListener(UsarObjeto);
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
        playerController.PuedeMoverse = false;

        // Aquí podrías inicializar el combate, establecer los turnos, etc.
    }

    public void FinalizarCombate()
    {
        // Reactivar objetos de la escena
        

        // Permitir que el jugador se mueva de nuevo
        playerController.PuedeMoverse = true;
    }

    public void Atacar()
    {
        //textoAccion.text = "¡El jugador ataca!";
        // Lógica de ataque

        // Finalizar turno
        FinalizarTurno();
    }

    public void Defender()
    {
        //textoAccion.text = "¡El jugador se defiende!";
        // Lógica de defensa

        // Finalizar turno
        FinalizarTurno();
    }

    public void Huir()
    {
        //textoAccion.text = "¡El jugador intenta huir!";
        // Lógica para intentar huir

        // Finalizar turno
        FinalizarTurno();
    }

    public void UsarObjeto()
    {
        //textoAccion.text = "¡El jugador usa un objeto!";
        
        // Aquí podrías implementar la lógica para seleccionar y usar un objeto del inventario
        if (Inventario.instancia.listaObjetos.Count > 0)
        {
            // Por ejemplo, podrías instanciar el objeto en el escenario
            GameObject objetoInstanciado = Instantiate(Inventario.instancia.listaObjetos[0].prefab, playerController.transform.position + new Vector3(1, 0, 0), Quaternion.identity);
            objetoInstanciado.transform.SetParent(playerController.transform);

            // Eliminar el objeto usado del inventario
            Inventario.instancia.listaObjetos.RemoveAt(0);
        }
        else
        {
            Debug.Log("No hay objetos en el inventario");
        }

        // Finalizar turno
        FinalizarTurno();
    }

    private void FinalizarTurno()
    {
        // Aquí podrías implementar la lógica para el turno del enemigo
    }
}





 */