using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialoguesVacio : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;

    public NPCConversation conversation; // Referencia a la conversación que deseas iniciar
    private bool isPlayerInRange;
    private bool didDialogueStart;

    private PlayerController playerController; // Referencia al controlador del jugador

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Encontrar la referencia al controlador del jugador
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

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialogueMark.SetActive(false);
        Invoke("StartConversation", 0.5f);



    }

    public void StartConversation()
    {
        playerController.PermitirMovimiento(false); // Restringir el movimiento del jugador
        ConversationManager.Instance.StartConversation(conversation);
    }

    public void EndConversation()
    {
        playerController.PermitirMovimiento(true); // Permitir que el jugador se mueva nuevamente
        didDialogueStart = false; // Reiniciar la bandera de inicio de diálogo
        dialogueMark.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        
        Debug.Log("chocando");
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
            Debug.Log("Dentro");

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
