using UnityEngine;

namespace Dish.FryProducts
{
    public class BurnedState : FryStateBase
    {
        private GameObject burnedGO;

        public BurnedState(GameObject burned) => burnedGO = burned;

        public override void Enter() => burnedGO.SetActive(true);

        public override void Exit() { }
        public override void StartCooking() { }
        public override void StopCooking() { }
        public override void Update(float deltaTime) { }
    }
}
