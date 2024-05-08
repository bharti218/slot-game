using System.Collections;
using UnityEngine;

namespace slotgame
{
    public class SpinState : BaseState
    {
        public override void Enter()
        {
            Debug.Log("Entering Spin State:");
            PlayerManager.Instance.OnSpin();
            SpinReels();
            base.Enter();
        }


        public void SpinReels()
        {
            StartCoroutine(SpinReelsCoroutine());
        }

        IEnumerator SpinReelsCoroutine()
        {
            for (int i = 0; i < LevelGenerator.Instance.myReels.Count; i++)
            {
                LevelGenerator.Instance.myReels[i].ResetReel();
            }

            for (int i = 0; i < LevelGenerator.Instance.myReels.Count; i++)
            {
                LevelGenerator.Instance.myReels[i].Spin();
                yield return new WaitForSeconds(LevelGenerator.Instance.animDuration / 2);

            }
            yield return new WaitForSeconds(1f);
            SlotGameController.Instance.SetResultState();
        }

        public override void Exit()
        {
            Debug.Log("Exiting Spin State:");
            base.Exit();
        }
    }
}