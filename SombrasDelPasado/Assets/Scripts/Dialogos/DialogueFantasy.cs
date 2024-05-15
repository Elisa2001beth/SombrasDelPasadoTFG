using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueFantasy : MonoBehaviour
{
    public NPCConversation _conversation; // Referencia al gestor de di�logo
    public float delayBeforeDialogue = 2.0f; // Retraso antes de iniciar los di�logos
    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        StartConversation(); // Inicia la conversaci�n al comienzo de la escena
    }

    private void StartInitialConversationWithDelay()
    {
        playerController.PermitirMovimiento(false); // Deshabilita el movimiento del jugador
        ConversationManager.Instance.StartConversation(_conversation);
    }

    void StartConversation()
    {
        Invoke("StartInitialConversationWithDelay", delayBeforeDialogue);
    }

    public void EndConversationWithDelay()
    {
        playerController.PermitirMovimiento(true); // Habilita el movimiento del jugador al finalizar el di�logo
    }
}