using DishContainer;
using UnityEngine;
using Dish;
using UnityEngine.Audio;
using Misc.Sound;
using NUnit.Framework;
using System.Collections.Generic;

namespace Attach
{
    public class CookAttachComponent : AttachComponent
    {
        [SerializeField]
        private List<AudioResource> attachSounds;

        private DishContainerBase container;

        private IPlaySound attachSound;

        private void Start()
        {
            attachSound = new PlayRandomSound(GetComponent<AudioSource>(), attachSounds);
            OnAttach += attachSound.Play;
        }

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
