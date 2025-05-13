using DishContainer;
using UnityEngine;
using Dish;

namespace Attach
{
    public class CookAttachComponent : AttachComponent
    {
        private DishContainerBase container;

        public override void Attach(GameObject target)
        {
            if(container==null)
            {
                base.Attach(target);
            }

            if(target==null)    
            {
                return;
            }

            //DishContainerBase? cont = target.GetComponent<DishContainerBase>();

            if(target.TryGetComponent(out DishContainerBase cont))
            {
                if (cont != null)
                {
                    container = cont;
                    base.Attach(target);
                    return;
                }
            }



            if(container!=null)
            {
                //var prod = target.GetComponent<DishProductBase>();

                if(target.TryGetComponent(out DishProductBase prod))
                {
                    if (prod != null)
                    {
                        container.Add(prod);
                    }
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
                if(target.attachObject!=null)
                {
                    if (target.attachObject.TryGetComponent(out DishProductBase prod))
                    {
                        container.Add(prod);
                        return;
                    }
                }

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
