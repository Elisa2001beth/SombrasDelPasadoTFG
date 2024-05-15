
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialoguesVacio : MonoBehaviour
{
    public NPCConversation startConversation;
    public NPCConversation catConversation;
    public NPCConversation doorConversation;
    public NPCConversation lampConversation;
    private bool isPlayerInRange;
    private bool didDialogueStart;

    [SerializeField] private GameObject dialogueMark;

    private PlayerController playerController;

    //nuevo
    [SerializeField] private DoorLockCode doorLockCode; 


    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        // Suscribir al evento de finalización de conversación
        ConversationManager.OnConversationEnded += EndConversation;
        doorLockCode = FindObjectOfType<DoorLockCode>();
        if (doorLockCode == null)
        {
            Debug.LogError("DoorLockCode no se encontró en la escena. Asegúrate de que esté presente.");
        }
        else
        {
            Debug.Log("DoorLockCode encontrado.");
            
        }
        StartInitialConversation();


    }

    void Update()
    {
        Debug.Log(doorLockCode.isCodeCorrect);
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
        if (tag == "Cat")
        {
            ConversationManager.Instance.StartConversation(catConversation);
        }
        else if (tag == "Door")
        {

            // Iniciar conversación principal para la puerta
            ConversationManager.Instance.StartConversation(doorConversation);
            ConversationManager.Instance.SetBool("codeCorrect", doorLockCode.isCodeCorrect);
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
        if (isPlayerInRange) // Mostrar la marca de diálogo solo si el jugador sigue en rango
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
