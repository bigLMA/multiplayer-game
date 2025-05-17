using UnityEngine;
using UnityEngine.UI;

namespace UI.ProgressBars
{
    public class SliceBar : MonoBehaviour, IProgressBar
    {
        [SerializeField]    
        private Image progressImage;

        [SerializeField]
        Gradient colorGradient;
        public bool displaying { get; private set; } = false;

        void Start()
        {
            gameObject.SetActive(false);
        }

        public void ResetProgressBar()
        {
            gameObject.SetActive(false);
        }

        public void SetProgress(float progress)
        {
            progressImage.fillAmount = progress;
            progressImage.color = colorGradient.Evaluate(progress);
        }

        public void SetupProgressBar()
        {
            gameObject.SetActive(true);
        }
    }
}

