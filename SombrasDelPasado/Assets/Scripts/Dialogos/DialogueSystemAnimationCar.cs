/* using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueSystemAnimationCar : MonoBehaviour
{
    public Sprite[] characterSprites;  // Array de sprites de personajes
    public string[] characterNames;    // Array de nombres de personajes
    public string[] dialogues;         // Array de textos de diálogo

    public Image dialogueBox;
    public Image characterImage;
    public TMP_Text characterNameText;
    public TMP_Text dialogueText;

    public float letterDelay = 0.05f;  // Retardo entre cada letra
    public AudioClip letterSound;      // Sonido de cada letra
    public AudioClip endSound;         // Sonido al final del diálogo

    public string nextSceneName;       // Nombre de la próxima escena

    private int currentDialogueIndex = 0;
    private string currentDialogue = "";
    private int currentLetterIndex = 0;
    private AudioSource audioSource;

    private bool isTyping = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        HideDialogueElements();  // Oculta los elementos de diálogo al inicio
        Invoke("StartDialogue", 2f);  // Espera 2 segundos antes de iniciar el diálogo
    }

    void HideDialogueElements()
    {
        dialogueBox.gameObject.SetActive(false);
        characterImage.gameObject.SetActive(false);
        characterNameText.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
    }

    void ShowDialogueElements()
    {
        dialogueBox.gameObject.SetActive(true);
        characterImage.gameObject.SetActive(true);
        characterNameText.gameObject.SetActive(true);
        dialogueText.gameObject.SetActive(true);
    }

    void StartDialogue()
    {
        ShowDialogueElements();  // Muestra los elementos de diálogo
        ShowDialogue();
    }

    void Update()
    {
        if (!isTyping && currentDialogueIndex < dialogues.Length && Input.GetMouseButtonDown(0))
        {
            ShowDialogue();
        }
    }

    void ShowDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            currentDialogue = dialogues[currentDialogueIndex];
            dialogueBox.sprite = characterSprites[currentDialogueIndex];
            characterImage.sprite = characterSprites[currentDialogueIndex];
            characterNameText.text = characterNames[currentDialogueIndex];
            dialogueText.text = "";

            currentLetterIndex = 0;
            isTyping = true;
            InvokeRepeating("ShowNextLetter", 0, letterDelay);

            currentDialogueIndex++;

            if (currentDialogueIndex == dialogues.Length)
            {
                float delayBeforeSceneChange = currentDialogue.Length * letterDelay + 2; // 2 segundos adicionales después del último diálogo
                Invoke("HideDialogueElementsAndChangeScene", delayBeforeSceneChange);
            }
        }
    }

    void ShowNextLetter()
    {
        if (currentLetterIndex < currentDialogue.Length)
        {
            dialogueText.text += currentDialogue[currentLetterIndex];
            currentLetterIndex++;

            if (letterSound && audioSource)
            {
                audioSource.PlayOneShot(letterSound);
            }
        }
        else
        {
            isTyping = false;
            CancelInvoke("ShowNextLetter");

            if (endSound && audioSource && currentDialogueIndex == dialogues.Length)
            {
                audioSource.PlayOneShot(endSound);
            }
        }
    }

    void HideDialogueElementsAndChangeScene()
    {
        HideDialogueElements();  // Oculta los elementos de diálogo
        Invoke("ChangeScene", 2f);  // Cambia de escena después de 2 segundos
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);  // Cambia a la escena definida en nextSceneName
    }
}
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogueSystemAnimationCar : MonoBehaviour
{
    public Sprite[] characterSprites;
    public string[] characterNames;
    public string[] dialogues;

    public Image dialogueBox;
    public Image characterImage;
    public TMP_Text characterNameText;
    public TMP_Text dialogueText;

    public float initialDelay = 2f;  // Tiempo de espera inicial
    public float letterDelay = 0.05f;
    public AudioClip startSound;  // Audio al inicio
    public AudioClip endSound;  // Audio al final

    public string nextSceneName;

    private int currentDialogueIndex = 0;
    private string currentDialogue = "";
    private int currentLetterIndex = 0;
    private AudioSource audioSource;

    private bool isTyping = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        HideDialogueElements();

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

    void HideDialogueElements()
    {
        dialogueBox.gameObject.SetActive(false);
        characterImage.gameObject.SetActive(false);
        characterNameText.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
    }

    void ShowDialogueElements()
    {
        dialogueBox.gameObject.SetActive(true);
        characterImage.gameObject.SetActive(true);
        characterNameText.gameObject.SetActive(true);
        dialogueText.gameObject.SetActive(true);
    }

    void ShowDialogue()
    {
        ShowDialogueElements();

        if (currentDialogueIndex < dialogues.Length)
        {
            currentDialogue = dialogues[currentDialogueIndex];
            dialogueBox.sprite = characterSprites[currentDialogueIndex];
            characterImage.sprite = characterSprites[currentDialogueIndex];
            characterNameText.text = characterNames[currentDialogueIndex];
            dialogueText.text = "";

            currentLetterIndex = 0;
            isTyping = true;
            InvokeRepeating("ShowNextLetter", 0, letterDelay);
        }
        else
        {
            HideDialogueElements();
            PlayEndSoundAndChangeScene();
        }
    }

    void ShowNextLetter()
    {
        if (currentLetterIndex < currentDialogue.Length)
        {
            dialogueText.text += currentDialogue[currentLetterIndex];
            currentLetterIndex++;
        }
        else
        {
            isTyping = false;
            CancelInvoke("ShowNextLetter");
        }
    }

    void Update()
    {
        if (!isTyping && currentDialogueIndex < dialogues.Length && Input.GetMouseButtonDown(0))
        {
            currentDialogueIndex++;
            dialogueText.text = "";
            ShowDialogue();
        }
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
