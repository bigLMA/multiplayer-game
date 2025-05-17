using Misc.Timer;
using UI.ProgressBars;
using UnityEngine;

namespace Dish.FryProducts
{
    public class RawState : FryStateBase
    {
        private GameObject rawGO;

        private float cookTime;

        private ITimer timer;

        private FryProductBase parent;

        private IProgressBar progressBar;

        public RawState(GameObject raw, float time, IProgressBar bar, ITimer timerRef, FryProductBase product)
        {
            rawGO = raw;
            cookTime = time;
            parent = product;
            timer = timerRef;
            progressBar = bar;

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

            progressBar.ResetProgressBar();

            timer.Stop();
            timer.OnTimerFinished -= Exit;

            rawGO.SetActive(false); 

            parent.state = parent.cookedState;
            parent.state.Enter();
        }

        public override void StartCooking()
        {
            if (timer == null) return;

            progressBar.SetupProgressBar();

            timer.Start(cookTime);
            timer.OnTimerFinished+= Exit;
        }

        public override void StopCooking()
        {
            if (timer == null) return;

            StopCooking();
            
            timer.Pause();
            timer.OnTimerFinished -= Exit;
        }

        public override void Update(float deltaTime)
        {
            if (timer == null) return;

            timer.Update(deltaTime);

            if (progressBar == null || !progressBar.displaying) return;

            progressBar.SetProgress(timer.duration / timer.maxDuration);
        }
    }
}
