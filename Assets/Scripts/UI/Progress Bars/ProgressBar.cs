using UnityEngine;

namespace UI.ProgressBars
{
    public interface IProgressBar
    {
        void SetProgress(float progress);

        void SetupProgressBar();

        void ResetProgressBar();

        bool displaying { get; }
    }
}