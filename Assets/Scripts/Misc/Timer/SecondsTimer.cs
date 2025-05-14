namespace Misc.Timer
{
    public class SecondsTimer : ITimer
    {
        public float duration { get; private set; } = 0f;

        public TimerStatus status { get; private set; } = TimerStatus.stopped;

        public event ITimer.TimerCallback OnTimerFinished;

        public void Pause()
        {
            status = TimerStatus.paused;
        }

        public void Start(float newDuration)
        {
            duration = newDuration;
            status = TimerStatus.started;
        }

        public void Stop()
        {
            status = TimerStatus.stopped;
        }

        public void Update(float deltaTime)
        {
            if(status== TimerStatus.started)
            {
                duration -= deltaTime;

                if (duration <= 0f && status == TimerStatus.started)
                {
                    OnTimerFinished?.Invoke();
                    Stop();
                }
            }
        }
    }
}
