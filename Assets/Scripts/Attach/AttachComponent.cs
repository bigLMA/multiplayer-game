using UnityEngine;
using UnityEngine.UIElements;

namespace Attach
{
    public class AttachComponent : MonoBehaviour, IAttach
    {
        [Header("Attach info")]
        [SerializeField]
        [Tooltip("Distance from parent to attach object")]
        private Vector3 attachOffset = Vector3.zero;

        public event IAttach.AttachCallback OnAttach;
        public event IAttach.AttachCallback OnDetach;

        public GameObject attachObject { get; private set; } = null;

        public virtual void Attach(GameObject target)
        {
            if (attachObject != null) return;
            if(target == null) return;

            attachObject = target;
            attachObject.transform.parent = transform;
            attachObject.transform.position= transform.position;
            attachObject.transform.localPosition += attachOffset;
            attachObject.transform.rotation = Quaternion.identity;

            OnAttach?.Invoke();
        }

        public virtual void Detach()
        {
            if (attachObject == null) return;

            attachObject.transform.parent = null;
            attachObject=null;

            OnDetach?.Invoke();
        }

        public virtual void Swap(IAttach target)
        {
            var thisAttach = attachObject;
            var targetAttach = target.attachObject;

            Detach();
            target.Detach();

            Attach(targetAttach);
            target.Attach(thisAttach);
        }

        public virtual void DestroyAttached()
        {
            if(attachObject==null) return;

            var target = attachObject;
            Detach();
            Destroy(target);
        }
    }
}
