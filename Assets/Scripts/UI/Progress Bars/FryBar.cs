using Dish.FryProducts;
using UnityEngine;
using Misc;
using UnityEngine.UI;

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

        private Color initialColor;

        private CookedState state;

        public bool displaying { get; private set; } = false;

        void Start()
        {
            initialColor = progressBar.color;
        }

        public void ResetProgressBar()
        {
            displaying = false;
            gameObject.SetActive(false);
            progressBar.fillAmount = 0f;
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
            state.OnBurnWarning-=Warning;
            print("WARNING");
            warningGO.SetActive(true);
        }
    }
}