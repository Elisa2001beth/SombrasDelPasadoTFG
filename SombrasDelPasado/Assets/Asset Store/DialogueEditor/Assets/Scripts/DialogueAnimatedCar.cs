
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using DialogueEditor;

public class DialogueAnimatedCar : MonoBehaviour
{

    public float initialDelay = 2f;  // Tiempo de espera inicial
    public NPCConversation _conversation; // Referencia al gestor de diálogo
 
    public AudioClip startSound;  // Audio al inicio
    public AudioClip endSound;  // Audio al final

    public string nextSceneName;

  
    private AudioSource audioSource;


    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
       
        StartCoroutine(PlayInitialDelayAndStartSound());
    }

    IEnumerator PlayInitialDelayAndStartSound()
    {
        yield return new WaitForSeconds(initialDelay);

        if (startSound)
        {
            audioSource.clip = startSound;
            audioSource.Play();

            yield return new WaitForSeconds(startSound.length);
        }

        ShowDialogue();
    }

    

    void ShowDialogue()
    {
        ConversationManager.Instance.StartConversation(_conversation); // Método para iniciar los diálogos
    }



    void PlayEndSoundAndChangeScene()
    {
        if (endSound)
        {
            audioSource.clip = endSound;
            audioSource.Play();

            Invoke("ChangeScene", endSound.length + 1f);  // Cambio de escena después de que termine el sonido final
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

