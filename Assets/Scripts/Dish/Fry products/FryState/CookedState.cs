using Misc.Timer;
using UnityEngine;

namespace Dish.FryProducts
{
    public class CookedState : FryStateBase
    {
        private GameObject cookedGO;

        private FryProductBase parent;

        private float burnTime;

        private ITimer timer = new SecondsTimer();

        public CookedState(GameObject cooked, float time, FryProductBase product)
        {
            cookedGO = cooked;
            burnTime = time;
            parent = product;
        }

        public override void Enter()
        {
            cookedGO.SetActive(true);
        }

        public override void Exit()
        {
            if (timer == null) return;

            timer.Stop();
            timer.OnTimerFinished -= StopCooking;

            cookedGO.SetActive(false);
            parent.state = parent.burnedState;
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
