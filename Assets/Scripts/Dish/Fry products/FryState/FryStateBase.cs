namespace Dish.FryProducts
{
    public abstract class FryStateBase
    {
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update(float deltaTime);

        public abstract void StartCooking();
        public abstract void StopCooking();
    }
}
