using DishContainer;
using UnityEngine;
using Dish;

namespace Attach
{
    public class CookAttachComponent : AttachComponent
    {
        private IDishContainer container;

        public override void Attach(GameObject target)
        {
            if(container==null)
            {
                base.Attach(target);
            }

            IDishContainer? cont = target.GetComponent<IDishContainer>();

            if (cont != null)
            {
                container = cont;
            }

            if(container!=null)
            {
                var prod = target.GetComponent<IDishProduct>();

                if (prod!=null)
                {
                    container.Add(prod);
                }
            }
        }

        public override void Detach()
        {
            if (container != null)
            {
                container = null;
            }

            base.Detach();
        }

        public override void Swap(IAttach target)
        {
            if(container!= null)
            {
                container = null;
            }

            base.Swap(target);
        }

        public override void DestroyAttached()
        {
            container = null;

            base.DestroyAttached();
        }
    }
}
