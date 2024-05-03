using UnityEngine;

namespace slotgame
{
    public class IdleState : BaseState
    {
        public override void Enter()
        {
            Debug.Log("Entering Idle State:");
            base.Enter();
        }

        public override void Exit()
        {
            Debug.Log("Exiting Idle State:");
            base.Exit();
        }
    }
}