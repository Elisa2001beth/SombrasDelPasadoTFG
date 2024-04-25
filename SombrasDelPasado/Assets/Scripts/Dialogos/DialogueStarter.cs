using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueStarter : MonoBehaviour
{
    public NPCConversation _conversation; // Referencia al gestor de diálogo
    public float delayBeforeDialogue = 2.0f; // Retraso antes de iniciar los diálogos

    void Start()
    {
        Invoke("StartDialogue", delayBeforeDialogue);
    }

    void StartDialogue()
    {
        ConversationManager.Instance.StartConversation(_conversation); // Método para iniciar los diálogos
    }
}