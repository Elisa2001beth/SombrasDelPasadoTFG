//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DialogueEditor;

//public class DialogueParkAlbum : MonoBehaviour
//{
//    public NPCConversation _conversation;
//    public float delayBeforeDialogue = 2.0f;

//    private PlayerController playerController; // Referencia al controlador del jugador

//    void Start()
//    {
//        playerController = FindObjectOfType<PlayerController>(); // Encontrar la referencia al controlador del jugador
//    }

//    public void OnTriggerEnterPlayerCollider()
//    {
//        Debug.Log("Colisi�n con el jugador detectada. Iniciando conversaci�n en " + delayBeforeDialogue + " segundos.");
//        Invoke("StartDialogue", delayBeforeDialogue); // Esperar antes de iniciar la conversaci�n
//    }

//    void StartDialogue()
//    {
//        playerController.PermitirMovimiento(false); // Restringir el movimiento del jugador
//        ConversationManager.Instance.StartConversation(_conversation);
//    }

//    // Agrega este m�todo para llamar cuando el di�logo termine y permitir que el jugador se mueva nuevamente
//    public void EndDialogue()
//    {
//        playerController.PermitirMovimiento(true); // Permitir que el jugador se mueva nuevamente
//    }
//}
using UnityEngine;
using DialogueEditor;

public class DialogueParkAlbum : MonoBehaviour
{
    public NPCConversation firstConversation;
    public NPCConversation secondConversation; // Segunda conversaci�n
    public float delayBeforeDialogue = 2.0f;

    private PlayerController playerController; // Referencia al controlador del jugador

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Encontrar la referencia al controlador del jugador
    }

    public void OnTriggerEnterPlayerCollider()
    {
        Debug.Log("Colisi�n con el jugador detectada. Iniciando conversaci�n en " + delayBeforeDialogue + " segundos.");
        Invoke("StartDialogue", delayBeforeDialogue); // Esperar antes de iniciar la conversaci�n
    }

    void StartDialogue()
    {
        playerController.PermitirMovimiento(false); // Restringir el movimiento del jugador
        ConversationManager.Instance.StartConversation(firstConversation);
    }

    // M�todo para llamar cuando termine la primera conversaci�n
    public void EndFirstConversation()
    {
        playerController.PermitirMovimiento(true); // Permitir que el jugador se mueva nuevamente
        // Aqu� puedes iniciar la segunda conversaci�n si es necesario
        StartSecondConversation();
    }

    void StartSecondConversation()
    {
        // Comienza la segunda conversaci�n
        ConversationManager.Instance.StartConversation(secondConversation);
    }
}
