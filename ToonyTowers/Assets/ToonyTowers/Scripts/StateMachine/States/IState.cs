namespace ToonyTowers.StateMachine.States
{
    /// <summary>
    /// Represents a state in a state machine.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Executes the OnEnter logic of the state.
        /// </summary>
        void OnEnter();

        /// <summary>
        /// Executes the Tick logic of the state machine.
        /// </summary>
        void Tick();

        /// <summary>
        /// Executes the OnExit logic of the state.
        /// </summary>
        /// <remarks>
        /// This method is called when the state machine is transitioning from the current state to a new state.
        /// It should contain any cleanup or finalization logic that needs to be performed before leaving the current state.
        /// </remarks>
        void OnExit();
    }
}