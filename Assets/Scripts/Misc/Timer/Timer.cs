using UnityEngine;

namespace Misc.Timer
{
    public enum TimerStatus
    {
        stopped,
        started, 
        paused
    }

    public interface ITimer
    {
        public void Start(float newDuration);

        public void Stop();

        public void Pause();    

        public void Update(float deltaTime);

        public float duration { get; }

        public float maxDuration { get; }

        public TimerStatus status { get; }

        public delegate void TimerCallback();
        public event TimerCallback OnTimerFinished;
    }
}