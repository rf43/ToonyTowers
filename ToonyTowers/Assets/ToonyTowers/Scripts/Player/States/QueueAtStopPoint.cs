using ToonyTowers.StateMachine.States;

namespace ToonyTowers.Player.States
{
    public class QueueAtStopPoint : BaseState<TheThing>
    {
        public QueueAtStopPoint(TheThing owner) : base(owner) { }

        public override void OnEnter() { }

        public override void Tick() { }

        public override void OnExit() { }
    }
}