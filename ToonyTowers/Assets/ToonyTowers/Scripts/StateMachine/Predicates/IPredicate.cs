namespace ToonyTowers.StateMachine.Predicates
{
    /// <summary>
    /// Represents a predicate that can be evaluated to determine the occurrence of a transition in a state machine.
    /// </summary>
    public interface IPredicate
    {
        /// <summary>
        /// Evaluates the predicate to determine if the transition can occur.
        /// </summary>
        /// <returns>True if the predicate evaluates successfully, otherwise false</returns>
        bool Evaluate();
    }
}