using Misc.Timer;
using System.Timers;
using UI.ProgressBars;
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

        [SerializeField]
        private GameObject progressBarGO;

        [field: SerializeField]
        public DishProductData cookedData { get; private set; }

        [field: SerializeField]
        public DishProductData burnedData { get; private set; }

        public FryStateBase rawState { get; private set; }
        public FryStateBase cookedState { get; private set; } 
        public FryStateBase burnedState { get; private set; } 

        public FryStateBase state { get; set; }

        ITimer timer = new SecondsTimer();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            cookedGO.SetActive(false);
            burnedGO.SetActive(false);

            var progressBar = progressBarGO.GetComponent<IProgressBar>();

            rawState = new RawState(rawGO, cookTime, progressBar, timer, this);
            cookedState = new CookedState(cookedGO, burnTime, burnProgressNotification, progressBar, timer, this);
            burnedState = new BurnedState(burnedGO);

            state = rawState;
            state.Enter();
        }

        public void UpdateRoutine()
        {
            if (state == null) return;

            state.Update(Time.deltaTime);
        }

        public void StartFrying()
        {
            if (state == null) return;

            state.StartCooking();
        }

        public void StopFrying()
        {
            if (state == null) return;

            state.StopCooking();
        }
    }

}