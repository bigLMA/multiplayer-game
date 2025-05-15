using Misc.Timer;
using UnityEngine;

namespace Dish.FryProducts
{
    public class FryProductBase : MonoBehaviour
    {
        [SerializeField]
        private GameObject rawGO;

        [SerializeField]
        [Range(1f, 10f)]
        private float cookTime = 4f;

        [SerializeField]
        private GameObject cookedGO;

        [SerializeField]
        [Range(1f, 10f)]
        private float burnTime = 5.5f;

        [SerializeField]
        private GameObject burnedGO;

        [SerializeField]
        [Range(0.25f, 0.65f)]
        private float burnProgressNotification = 0.25f;

        private ITimer timer;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            timer = new SecondsTimer();

            cookedGO.SetActive(false);
            burnedGO.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            timer.Update(Time.deltaTime);
        }

        public void StartFrying()
        {
            if (cookedGO.activeInHierarchy)
            {
                timer.Start(burnTime);
                timer.OnTimerFinished += OnBurned;
            }
            else
            {
                timer.Start(cookTime);
                timer.OnTimerFinished += OnCooked;
            }
        }

        public void StopFrying()
        {
            if (cookedGO.activeInHierarchy)
            {
                timer.Pause();
                timer.OnTimerFinished -= OnBurned;
            }
            else
            {
                timer.Pause();
                timer.OnTimerFinished -= OnCooked;
            }
        }

        private void OnCooked()
        {
            timer.OnTimerFinished -= OnCooked;

            rawGO.SetActive(false);
            cookedGO.SetActive(true);
        }

        private void OnBurned()
        {
            timer.OnTimerFinished -= OnBurned;

            cookedGO.SetActive(false);
            burnedGO.SetActive(true);
        }
    }

}