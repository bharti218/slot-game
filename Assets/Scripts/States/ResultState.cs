using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slotgame
{
    public class ResultState : BaseState
    {
        private Dictionary<ICON_ID, int> occuranceDictionary = new Dictionary<ICON_ID, int>();
        public override void Enter()
        {
            Debug.Log("Entering Result State:");
            base.Enter();

            occuranceDictionary.Clear();
            // Step - 1: Generate Occurance Dictionary.
            for (int i = 0; i < LevelGenerator.Instance.myReels.Count; i++)
            {
                ReelController reelController = LevelGenerator.Instance.myReels[i];
                for (int j= 0; j < reelController.mySlotItems.Count; j++)
                {
                    if (occuranceDictionary.ContainsKey(reelController.mySlotItems[j].iconID))
                        occuranceDictionary[reelController.mySlotItems[j].iconID]++;
                    else
                        occuranceDictionary.Add(reelController.mySlotItems[j].iconID, 1);
                }
            }

            // Step - 2: Figure out max frequent element in the level
            ICON_ID maxfrequentIcon = 0 ;
            int maxCount = -1000;

            foreach (KeyValuePair<ICON_ID, int> keyValuePair in occuranceDictionary)
            {
                if (keyValuePair.Value > maxCount)
                {
                    maxfrequentIcon = keyValuePair.Key;
                    maxCount = keyValuePair.Value;
                }
            }

            Debug.Log("Max Frequent Element is " + maxfrequentIcon + "having frequency : " + maxCount);

            // If the max frequent element is occuring as many times as required to win the level the destroy it.
            // else Set Idle State

            if (maxCount >= LevelGenerator.Instance.maxMatchSymbol)
            {
                for (int i = 0; i < LevelGenerator.Instance.myReels.Count; i++)
                {
                    LevelGenerator.Instance.myReels[i].HighLightMaxOccuringSymobol(maxfrequentIcon);
                    LevelGenerator.Instance.myReels[i].DestroyMaxOccuringSymobol(maxfrequentIcon);
                }

                Invoke(nameof(SetRespawnStateState), 2f);
            }

            else
                SlotGameController.Instance.SetIdleState();
            
        }

        private void SetRespawnStateState()
        {
            SlotGameController.Instance.ChangeState<RespawnState>();
        }

        public override void Exit()
        {
            Debug.Log("Exiting Result State:");
            base.Exit();
        }
    }
}