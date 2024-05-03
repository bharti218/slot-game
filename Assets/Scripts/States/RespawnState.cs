using System.Collections;
using UnityEngine;

namespace slotgame
{
    public class RespawnState : BaseState
    {
        public override void Enter()
        {
            Debug.Log("Entering Respawn State:");
            base.Enter();
            StartCoroutine(RespwanReels());
            
        }
        public override void Exit()
        {
            Debug.Log("Exiting Respawn State:");
            base.Exit();
        }

        private IEnumerator RespwanReels()
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < LevelGenerator.Instance.myReels.Count; i++)
            {
                LevelGenerator.Instance.myReels[i].RespwanHiddenReels();
            }

            yield return new WaitForSeconds(1f);
            SlotGameController.Instance.ChangeState<ResultState>();
        }
    }
}