using UnityEngine;

public class FadingItem : MonoBehaviour
{
    [SerializeField]
    [Range(0.5f, 99f)]
    private float fadeSpeed = 28f;

    private bool fade = false;

    private CanvasGroup canvasGroup;

    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (fade)
        {
            canvasGroup.alpha -= (fadeSpeed / 100 * Time.fixedDeltaTime);

            if (canvasGroup.alpha <= 0f)
            {
                fade = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void Fade()
    {
        if(canvasGroup ==null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        gameObject.SetActive(true);

        if (gameObject == null) return;
        if (this == null) return;
        if (canvasGroup == null) return;

        canvasGroup.alpha = 1f;
        fade = true;
    }
}
