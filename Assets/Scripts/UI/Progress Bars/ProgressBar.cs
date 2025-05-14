using UnityEngine;

namespace UI.ProgressBars
{
    public interface IProgressBar
    {
        void SetProgress(float progress);

        //void SetupProgressBar(Canvas canvas);
        void SetupProgressBar();

        void ResetProgressBar(Transform transform);

        bool displaying { get; }
    }
}