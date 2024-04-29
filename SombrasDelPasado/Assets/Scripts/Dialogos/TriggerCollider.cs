using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueParkAlbum dialogueParkAlbum = FindObjectOfType<DialogueParkAlbum>();
            if (dialogueParkAlbum != null)
            {
                dialogueParkAlbum.OnTriggerEnterPlayerCollider();
            }
        }
    }
}
