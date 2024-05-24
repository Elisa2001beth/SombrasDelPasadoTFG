
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialoguesVacio2 : MonoBehaviour
{
    public NPCConversation startConversation;
    public NPCConversation catConversation;
    public NPCConversation emilyConversation;
    public NPCConversation lampConversation;
    public NPCConversation endConversation;
    private bool isPlayerInRange;
    private bool didDialogueStart;

    [SerializeField] private GameObject dialogueMark;

    private PlayerController playerController;

    private BattleManager battleManager;



    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        battleManager = FindObjectOfType<BattleManager>();
        // Suscribir al evento de finalización de conversación
        ConversationManager.OnConversationEnded += EndConversation;
        BattleManager.OnPlayerVictory += PlayerWonBattle;

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

    private void PlayerWonBattle()
    {
        // Iniciar la nueva conversación aquí
        StartEndConversation();
    }

    private void StartEndConversation()
    {
        // Código para iniciar la nueva conversación después de que el jugador gane la batalla
        Invoke("StartEndConversationWithDelay", 0.25f);
    }

    private void StartEndConversationWithDelay()
    {
        playerController.PermitirMovimiento(false);
        ConversationManager.Instance.StartConversation(endConversation);
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
        else if (tag == "Enemy")
        {

            ConversationManager.Instance.StartConversation(emilyConversation);
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

    public void StartBattle()
    {
        battleManager.StartBattle();
    }



}
