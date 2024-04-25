// Script for having a typewriter effect for UI
// Prepared by Nick Hwang (https://www.youtube.com/nickhwang)
// Want to get creative? Try a Unicode leading character(https://unicode-table.com/en/blocks/block-elements/)
// Copy Paste from page into Inpector

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TypewriterUI : MonoBehaviour
{
    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [SerializeField] string leadingChar = "";
    [SerializeField] bool leadingCharBeforeDelay = false;
    [SerializeField] string[] phrases;
    [SerializeField] string nextSceneName; // Nombre de la siguiente escena

    Text _text;
    TMP_Text _tmpProText;
    int phraseIndex = 0;

    // Use this for initialization
    void Start()
    {
        _text = GetComponent<Text>();
        _tmpProText = GetComponent<TMP_Text>();

        if (_text != null)
        {
            StartCoroutine(TypeWriterText());
        }

        if (_tmpProText != null)
        {
            StartCoroutine(TypeWriterTMP());
        }
    }

    IEnumerator TypeWriterText()
    {
        foreach (string phrase in phrases)
        {
            yield return new WaitForSeconds(delayBeforeStart);

            if (_text != null)
            {
                _text.text = leadingCharBeforeDelay ? leadingChar : "";

                foreach (char c in phrase)
                {
                    if (_text.text.Length > 0)
                    {
                        _text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
                    }
                    _text.text += c;
                    _text.text += leadingChar;
                    yield return new WaitForSeconds(timeBtwChars);
                }

                if (leadingChar != "")
                {
                    _text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
                }

                yield return new WaitForSeconds(1f); // Esperar un segundo antes de borrar la frase
                _text.text = "";
            }
        }

        // Cambiar de escena después de mostrar todas las frases
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator TypeWriterTMP()
    {
        foreach (string phrase in phrases)
        {
            yield return new WaitForSeconds(delayBeforeStart);

            if (_tmpProText != null)
            {
                _tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

                foreach (char c in phrase)
                {
                    if (_tmpProText.text.Length > 0)
                    {
                        _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
                    }
                    _tmpProText.text += c;
                    _tmpProText.text += leadingChar;
                    yield return new WaitForSeconds(timeBtwChars);
                }

                if (leadingChar != "")
                {
                    _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
                }

                yield return new WaitForSeconds(1f); // Esperar un segundo antes de borrar la frase
                _tmpProText.text = "";
            }
        }

        // Cambiar de escena después de mostrar todas las frases
        SceneManager.LoadScene(nextSceneName);
    }
}
