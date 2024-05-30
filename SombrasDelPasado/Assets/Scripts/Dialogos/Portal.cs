using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;


public class Portal : MonoBehaviour
{
    public NPCConversation portalConversation;
    [SerializeField] private GameObject dialogueMark;
    private bool isPlayerInRange;
    private bool didDialogueStart;

    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        ConversationManager.OnConversationEnded += EndConversation;

    }

    void Update()
    {

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
        }
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

    private void OnDestroy()
    {
        // Desuscribir del evento cuando el objeto se destruya
        ConversationManager.OnConversationEnded -= EndConversation;
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialogueMark.SetActive(false);
        Invoke("StartConversationWithDelay", 0.15f);
    }

    private void StartConversationWithDelay()
    {
        playerController.PermitirMovimiento(false);
        StartConversation();

    }

    public void EndConversation()
    {
        playerController.PermitirMovimiento(true); // Permitir que el jugador se mueva nuevamente
        didDialogueStart = false; // Restablecer la variable para permitir que se inicie el diálogo nuevamente
        if (isPlayerInRange) // Mostrar la marca de diálogo solo si el jugador sigue en rango
        {
            dialogueMark.SetActive(true);
        }
    }

    public void StartConversation()
    {
        ConversationManager.Instance.StartConversation(portalConversation);
    }


}
