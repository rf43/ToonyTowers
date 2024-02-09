using System;
using System.Collections.Generic;
using System.Linq;
using ToonyTowers.StateMachine.Predicates;
using ToonyTowers.StateMachine.States;

namespace ToonyTowers.StateMachine
{
    /// <summary>
    /// Represents a state machine that manages the transitions between different states.
    /// </summary>
    public class Machine
    {
        private IState _currentState;
        private readonly Dictionary<Type, List<Transition>> _transitions = new();
        private List<Transition> _currentTransitions = new();
        private readonly List<Transition> _anyTransitions = new();
        private static readonly List<Transition> EmptyTransitions = new(0);

        /// <summary>
        /// Executes the Tick logic of the state machine.
        /// </summary>
        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                SetState(transition.To);
            }
            
            _currentState?.Tick();
        }

        /// <summary>
        /// Sets the current state of the state machine.
        /// </summary>
        /// <param name="state">The state to set.</param>
        public void SetState(IState state)
        {
            if (state == _currentState) return;
            
            _currentState?.OnExit();
            _currentState = state;

            _transitions.TryGetValue(
                _currentState.GetType(), out _currentTransitions
            );

            _currentTransitions ??= EmptyTransitions;
            
            _currentState.OnEnter();
        }

        /// <summary>
        /// Adds a transition between two states with a specified predicate.
        /// </summary>
        /// <param name="from">The state from which the transition starts.</param>
        /// <param name="to">The state to which the transition goes.</param>
        /// <param name="predicate">The predicate that determines if the transition can occur.</param>
        public void AddTransition(IState from, IState to, IPredicate predicate)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }
            
            transitions.Add(new Transition(to, predicate));
        }

        /// <summary>
        /// Adds a transition between any state and a specified state with a specified predicate.
        /// </summary>
        /// <param name="to">The state to which the transition goes.</param>
        /// <param name="predicate">The predicate that determines if the transition can occur.</param>
        public void AddAnyTransition(IState to, IPredicate predicate)
        {
            _anyTransitions.Add(new Transition(to, predicate));
        }

        /// <summary>
        /// Represents a transition between two states in a state machine.
        /// </summary>
        private class Transition
        {
            public IState To { get; }
            public IPredicate Condition { get; }
            
            public Transition(IState to, IPredicate condition)
            {
                To = to;
                Condition = condition;
            }
        }

        /// <summary>
        /// Retrieves the next transition based on the current state and transition conditions.
        /// </summary>
        /// <returns>The next transition, or null if no transition is available.</returns>
        private Transition GetTransition()
        {
            foreach (var transition in _anyTransitions.Where(transition => transition.Condition.Evaluate()))
            {
                return transition;
            }

            return _currentTransitions.FirstOrDefault(transition => transition.Condition.Evaluate());
        }
    }
}