using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueCity: MonoBehaviour
{
    public NPCConversation _conversation; // Referencia al gestor de diálogo
    public float delayBeforeDialogue = 2.0f; // Retraso antes de iniciar los diálogos

    private PlayerController playerController;
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        Invoke("StartDialogue", delayBeforeDialogue);
    }

    void StartDialogue()
    {
        playerController.PermitirMovimiento(false);
        ConversationManager.Instance.StartConversation(_conversation); // Método para iniciar los diálogos
    }

    public void EndConversation()
    {
        playerController.PermitirMovimiento(true);
    }

}