using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueParkAlbum : MonoBehaviour
{
    public NPCConversation _conversation;
    public float delayBeforeDialogue = 2.0f;

    private PlayerController playerController; // Referencia al controlador del jugador

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Encontrar la referencia al controlador del jugador
    }

    public void OnTriggerEnterPlayerCollider()
    {
        Debug.Log("Colisión con el jugador detectada. Iniciando conversación en " + delayBeforeDialogue + " segundos.");
        Invoke("StartDialogue", delayBeforeDialogue); // Esperar antes de iniciar la conversación
    }

    void StartDialogue()
    {
        playerController.PermitirMovimiento(false); // Restringir el movimiento del jugador
        ConversationManager.Instance.StartConversation(_conversation);
    }

    // Agrega este método para llamar cuando el diálogo termine y permitir que el jugador se mueva nuevamente
    public void EndDialogue()
    {
        playerController.PermitirMovimiento(true); // Permitir que el jugador se mueva nuevamente
    }
}
