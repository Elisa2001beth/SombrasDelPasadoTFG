using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialoguesVacio : MonoBehaviour
{
    public NPCConversation catConversation;
    public NPCConversation doorConversation;
    public NPCConversation lampConversation;
    private bool isPlayerInRange;
    private bool didDialogueStart;

    [SerializeField] private GameObject dialogueMark;

    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
                playerController.PermitirMovimiento(false);

            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialogueMark.SetActive(false);
        Invoke("StartConversationWithDelay", 0.5f);
    }

    private void StartConversationWithDelay()
    {
        playerController.PermitirMovimiento(false);
        StartConversation();
    }

    public void StartConversation()
    {
        string tag = gameObject.tag;
        if (tag == "Cat")
        {
            ConversationManager.Instance.StartConversation(catConversation);
        }
        else if (tag == "Door")
        {
            ConversationManager.Instance.StartConversation(doorConversation);
        }
        else if (tag == "Lamp")
        {
            ConversationManager.Instance.StartConversation(lampConversation);
        }
    }

    public void EndConversation()
    {
        playerController.PermitirMovimiento(true); // Permitir que el jugador se mueva nuevamente
        didDialogueStart = false; // Restablecer la variable para permitir que se inicie el diálogo nuevamente
        dialogueMark.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            dialogueMark.SetActive(false);
            isPlayerInRange = false;
        }
    }
}
