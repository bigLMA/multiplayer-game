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

        public void ResetProgressBar(Transform transform)
        {
            //transform.parent = null;
            //transform.parent = transform;
            //displaying = false;

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
            //GetComponent<Canvas>().worldCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
            //displaying = true;

            //var rectTransform = GetComponent<RectTransform>();
            //rectTransform.localPosition = new Vector3(0f, 1.75f);
            //transform.SetParent(GameObject.Find("Canvas").transform);
            //rectTransform.rotation = Quaternion.identity;
            //rectTransform.localScale = Vector3.one;
            //rectTransform.sizeDelta = new Vector2(2f, 0.35f);
        }

        //public void SetupProgressBar(Canvas canvas)
        //{
        //    transform.SetParent(canvas.transform);
        //}
    }
}

