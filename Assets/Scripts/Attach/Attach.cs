using UnityEngine;

namespace Attach
{
    public interface IAttach
    {
        void Attach(GameObject target);

        void Detach();

        void Swap(IAttach target);

        GameObject attachObject { get; }
    }
}
