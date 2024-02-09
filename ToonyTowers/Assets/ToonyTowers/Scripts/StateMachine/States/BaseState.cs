namespace ToonyTowers.StateMachine.States
{
    public abstract class BaseState<T> : IState
    {
        protected readonly T Owner;

        protected BaseState(T owner)
        {
            Owner = owner;
        }

        public abstract void OnEnter();
        public abstract void Tick();
        public abstract void OnExit();
    }
}