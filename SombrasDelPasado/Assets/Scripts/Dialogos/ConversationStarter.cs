using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    public NPCConversation conversation; // Referencia a la conversación que deseas iniciar

    public void StartConversation()
    {
        ConversationManager.Instance.StartConversation(conversation);
    }
}
