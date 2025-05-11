using UnityEngine;

namespace Counters
{
    public class ContainerCounterInteractable : CounterVisualInteractable
    {
        [Header("Opener")]
        [SerializeField]
        [Tooltip("Opener")]
        private GameObject counterOpener;

        [SerializeField]
        [Tooltip("Open X angle")]
        [Range(-10f, -20f)]
        private float openXAngle = -20f;

        public override void OnLook()
        {
            base.OnLook();
            counterOpener.transform.eulerAngles += new Vector3(openXAngle, 0f, 0f);

        }

        public override void OnNotLook()
        {
            base.OnNotLook();
            counterOpener.transform.eulerAngles -= new Vector3(openXAngle, 0f, 0f);
        }

        public override void Interact()
        {
            base.Interact();
        }
    }
}