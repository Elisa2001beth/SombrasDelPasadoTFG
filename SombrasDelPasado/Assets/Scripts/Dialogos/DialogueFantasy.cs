
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueFantasy : MonoBehaviour
{
    public NPCConversation startConversation;
    public NPCConversation portalConversation;
    
    private bool isPlayerInRange;
    private bool didDialogueStart;

    [SerializeField] private GameObject dialogueMark;

    private PlayerController playerController;

   


    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        // Suscribir al evento de finalizaci�n de conversaci�n
        ConversationManager.OnConversationEnded += EndConversation;
        StartInitialConversation();


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

    private void StartInitialConversation()
    {
        Invoke("StartInitialConversationWithDelay", 0.25f);
    }

    private void StartInitialConversationWithDelay()
    {
        playerController.PermitirMovimiento(false);
        ConversationManager.Instance.StartConversation(startConversation);
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

    public void StartConversation()
    {


        string tag = gameObject.tag;
        if (tag == "Portal")
        {
            ConversationManager.Instance.StartConversation(portalConversation);
        }
    }

    public void EndConversation()
    {
        playerController.PermitirMovimiento(true); // Permitir que el jugador se mueva nuevamente
        didDialogueStart = false; // Restablecer la variable para permitir que se inicie el di�logo nuevamente
        if (isPlayerInRange) // Mostrar la marca de di�logo solo si el jugador sigue en rango
        {
            dialogueMark.SetActive(true);
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



}
