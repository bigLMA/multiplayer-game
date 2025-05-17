using Misc.Timer;
using UnityEngine;

namespace Dish.FryProducts
{
    public class RawState : FryStateBase
    {
        private GameObject rawGO;

        private float cookTime;

        private ITimer timer;

        private FryProductBase parent;

        public RawState(GameObject raw, float time, ITimer timerRef, FryProductBase product)
        {
            rawGO = raw;
            cookTime = time;
            parent = product;
            timer = timerRef;

            Enter();
        }

        public override void Enter()
        {
            rawGO.SetActive(true);
            StartCooking();
        }

        public override void Exit()
        {
            if (timer == null) return;

            timer.Stop();
            timer.OnTimerFinished -= Exit;

            rawGO.SetActive(false); 

            parent.state = parent.cookedState;
            parent.state.Enter();
        }

        public override void StartCooking()
        {
            if (timer == null) return;

            timer.Start(cookTime);
            timer.OnTimerFinished+= Exit;   
        }

        public override void StopCooking()
        {
            if (timer == null) return;

            StopCooking();
            
            timer.Pause();
            timer.OnTimerFinished += Exit;
        }

        public override void Update(float deltaTime)
        {
            if (timer == null) return;

            timer.Update(deltaTime);
        }
    }
}
