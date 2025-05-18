using Dish.FryProducts;
using UnityEngine;
using Misc;
using UnityEngine.UI;
using System.Collections;

namespace UI.ProgressBars
{
    public class FryBar : MonoBehaviour, IProgressBar, IVisit<CookedState>
    {
        [SerializeField]
        private Image progressBar;

        [SerializeField]
        private Color warningColor;

        [SerializeField]
        private GameObject warningGO;

        [SerializeField]
        [Range(1f, 12f)]
        private float speed = 1f;

        private Color initialColor;

        private CookedState state;

        private bool warning = false;

        public bool displaying { get; private set; } = false;

        private PlaySound playSoundComp;

        void Start()
        {
            initialColor = progressBar.color;
            playSoundComp = GetComponent<PlaySound>();  
        }

        void Update()
        {
            if (warning)
            {
                var val = Mathf.PingPong(Time.time * speed, 1f);
                progressBar.color = Color.Lerp(initialColor, warningColor, val);
                warningGO.GetComponent<CanvasRenderer>().SetAlpha(val);
            }
        }

        public void ResetProgressBar()
        {
            displaying = false;
            gameObject.SetActive(false);
            progressBar.fillAmount = 0f;
            warningGO.SetActive(false); 

            warning = false;
            playSoundComp.Stop();
        }

        public void SetProgress(float progress)
        {
            progressBar.fillAmount = progress;
        }

        public void SetupProgressBar()
        {
            displaying = true;
            gameObject.SetActive(true);
        }

        public void Visit(CookedState value)
        {
            state = value;
            state.OnBurnWarning += Warning;
        }

        private void Warning()
        {
            state.OnBurnWarning -= Warning;
            warningGO.SetActive(true);
            warning = true;
            playSoundComp.Play();
        }
    }
}