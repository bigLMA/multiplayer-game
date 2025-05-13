using UnityEngine;
using Interaction;
using Attach;

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

        [Header("Container")]
        [SerializeField]
        [Tooltip("Product cook gets from this container")]
        private GameObject productPrefab;

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

        public override void Interact(Interactor interactor)
        {
            var attachComponent = interactor.GetComponent<IAttach>();

            if(attachComponent!=null)
            {
                if(attachComponent.attachObject == null)
                {
                    var go =Instantiate(productPrefab);
                    attachComponent.Attach(go);
                }
            }
        }
    }
}