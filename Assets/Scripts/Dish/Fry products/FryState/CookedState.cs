using Misc;
using Misc.Timer;
using UI.ProgressBars;
using UnityEngine;

namespace Dish.FryProducts
{
    public class CookedState : FryStateBase
    {
        private GameObject cookedGO;

        private FryProductBase parent;

        private float burnTime;

        private float burnWarningProgress;

        private IProgressBar progressBar;

        private ITimer timer;

        public delegate void BurnWarning();
        public event BurnWarning OnBurnWarning;

        public CookedState(GameObject cooked, float cookTime, float warningProgress, IProgressBar bar, ITimer timerRef, FryProductBase product)
        {
            cookedGO = cooked;
            burnTime = cookTime;
            parent = product;
            timer = timerRef;
            burnWarningProgress = warningProgress;
            progressBar = bar;

            if(progressBar!= null)
            {
                if(progressBar is IVisit<CookedState> visit)
                {
                    visit.Visit(this);
                }
            }
        }

        public override void Enter()
        {
            cookedGO.SetActive(true);

            StartCooking();
        }

        public override void Exit()
        {
            if (timer == null) return;

            progressBar.ResetProgressBar();

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

            progressBar.SetupProgressBar();

            timer.Start(burnTime);
            timer.OnTimerFinished += Exit;
        }

        public override void StopCooking()
        {
            progressBar.ResetProgressBar();

            timer.Pause();
        }

        public override void Update(float deltaTime)
        {
            if (timer == null) return;

            timer.Update(deltaTime);

            if(progressBar==null || !progressBar.displaying) return;

            var progress = timer.duration/timer.maxDuration;
            progressBar.SetProgress(1f-progress);

            if(1-progress>=burnWarningProgress)
            {
                OnBurnWarning?.Invoke();
            }
        }
    }
}
