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

        public FryStateBase rawState { get; private set; }
        public FryStateBase cookedState { get; private set; } 
        public FryStateBase burnedState { get; private set; } 

        public FryStateBase state { get; set; }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            cookedGO.SetActive(false);
            burnedGO.SetActive(false);

            rawState = new RawState(rawGO, cookTime, this);
            cookedState = new CookedState(cookedGO, burnTime, this);
            burnedState = new BurnedState(burnedGO);

            state = rawState;
            state.Enter();
        }

        // Update is called once per frame
        void Update()
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