using UnityEngine;

namespace Misc.Timer
{
    public interface ITimer
    {
        public void Start(float newDuration);

        public void Stop();

        public void Pause();    

        public void Update(float deltaTime);

        public float duration { get; }
    }
}