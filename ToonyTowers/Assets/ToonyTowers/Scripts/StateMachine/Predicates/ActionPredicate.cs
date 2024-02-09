using System;

namespace ToonyTowers.StateMachine.Predicates
{
    /// <summary>
    /// Represents a predicate that performs an action based on a given condition.
    /// </summary>
    public class ActionPredicate : IPredicate
    {
        private readonly Func<bool> _predicate;
        private readonly Action _action;
        
        public ActionPredicate(Func<bool> predicate, Action action)
        {
            _predicate = predicate;
            _action = action;
        }
        
        public bool Evaluate()
        {
            if (!_predicate.Invoke()) return false;
            
            _action.Invoke();
            return true;
        }
    }
}