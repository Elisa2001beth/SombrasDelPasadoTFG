
using UnityEngine;
using DialogueEditor;

public class DialogueParkAlbum : MonoBehaviour
{
    public NPCConversation firstConversation;
    public NPCConversation secondConversation; // Segunda conversación
    public float delayBeforeDialogue = 2.0f;

    private PlayerController playerController; // Referencia al controlador del jugador

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Encontrar la referencia al controlador del jugador
    }

    public void OnTriggerEnterPlayerCollider()
    {
        Invoke("StartDialogue", delayBeforeDialogue); // Esperar antes de iniciar la conversación
    }

    void StartDialogue()
    {
        playerController.PermitirMovimiento(false); // Restringir el movimiento del jugador
        ConversationManager.Instance.StartConversation(firstConversation);
    }

    // Método para llamar cuando termine la primera conversación
    public void EndFirstConversation()
    {
        playerController.PermitirMovimiento(true); // Permitir que el jugador se mueva nuevamente
        StartSecondConversation();
    }

    void StartSecondConversation()
    {
        // Comienza la segunda conversación
        ConversationManager.Instance.StartConversation(secondConversation);
    }
}
