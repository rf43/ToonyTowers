using System;

namespace ToonyTowers.StateMachine.Predicates
{
    /// <summary>
    /// Represents a Function predicate that can be evaluated to determine the occurrence
    /// of a transition in a state machine.
    /// </summary>
    public class FuncPredicate : IPredicate
    {
        private readonly Func<bool> _predicate;

        public FuncPredicate(Func<bool> predicate)
        {
            _predicate = predicate;
        }
        
        public bool Evaluate()
        {
            return _predicate.Invoke();
        }
    }
}