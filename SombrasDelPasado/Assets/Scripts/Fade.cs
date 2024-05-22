using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Animator animator;

    public delegate void FadeComplete();
    public event FadeComplete OnFadeComplete;

    public void FadeOut()
    {
        animator.Play("FadeOut");
    }

    // Este método se llama al final de la animación de FadeOut
    public void FadeOutComplete()
    {
        if (OnFadeComplete != null)
        {
            OnFadeComplete();
        }
    }
}
