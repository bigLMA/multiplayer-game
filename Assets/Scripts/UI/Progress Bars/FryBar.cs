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

        public bool displaying { get; private set; }

        public void ResetProgressBar()
        {
            gameObject.SetActive(false);
            progressBar.fillAmount = 0f;
        }

        public void SetProgress(float progress)
        {
            progressBar.fillAmount = progress;
        }

        public void SetupProgressBar()
        {
            gameObject.SetActive(false);
        }

        public void Visit(CookedState value)=>state = value;

        private void Warning()
        {
            warningGO.SetActive(true);
        }
    }
}