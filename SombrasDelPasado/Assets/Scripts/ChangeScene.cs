using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string nextSceneName;
    public Fade _fadeOut;

    private void Start()
    {
        _fadeOut.OnFadeComplete += OnFadeOutComplete;
    }

    private void OnDestroy()
    {
        _fadeOut.OnFadeComplete -= OnFadeOutComplete;
    }

    public void CambiarEscena()
    {
        StartCoroutine(FadeAndChangeScene());
    }

    private IEnumerator FadeAndChangeScene()
    {
        _fadeOut.FadeOut();
        // Espera a que la animación de FadeOut complete
        yield return new WaitUntil(() => fadeOutCompleted);
        SceneManager.LoadScene(nextSceneName);
    }

    private bool fadeOutCompleted = false;

    private void OnFadeOutComplete()
    {
        fadeOutCompleted = true;
    }
}
