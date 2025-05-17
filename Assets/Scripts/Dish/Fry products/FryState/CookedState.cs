using Misc.Timer;
using UnityEngine;

namespace Dish.FryProducts
{
    public class CookedState : FryStateBase
    {
        private GameObject cookedGO;

        private FryProductBase parent;

        private float burnTime;

        private ITimer timer;

        public CookedState(GameObject cooked, float time, ITimer timerRef, FryProductBase product)
        {
            cookedGO = cooked;
            burnTime = time;
            parent = product;
            timer = timerRef;
        }

        public override void Enter()
        {
            cookedGO.SetActive(true);

            StartCooking();
        }

        public override void Exit()
        {
            if (timer == null) return;

            StopCooking();

            timer.Stop();
            timer.OnTimerFinished -= Exit;

            cookedGO.SetActive(false);

            parent.state = parent.burnedState;
            parent.state.Enter();
        }

        public override void StartCooking()
        {
            if (timer == null) return;

            timer.Start(burnTime);
            timer.OnTimerFinished += Exit;
        }

        public override void StopCooking()
        {
            timer.Pause();
        }

        public override void Update(float deltaTime)
        {
            if (timer == null) return;

            timer.Update(deltaTime);
        }
    }
}
