using System.Collections.Generic;
using UnityEngine;

namespace slotgame
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("GameConfig")]
        public float animDuration;
        public int maxMatchSymbol;

        [SerializeField] int maxRow;
        [SerializeField] int maxCol;

        [Space(20)]
        [SerializeField] GameObject reel;
        [SerializeField] RectTransform rectTransform;
        public static LevelGenerator Instance;
        public List<ReelController> myReels = new List<ReelController>();


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            for(int i = 0; i<maxCol; i++)
            {
                GameObject reelObject = Instantiate(reel, transform);
                ReelController reelController = reelObject.GetComponent<ReelController>();
                reelController.InitReel(rectTransform.sizeDelta.x/maxCol, rectTransform.sizeDelta.y, maxRow, animDuration);
                myReels.Add(reelController);
            }
        }

       

    }

}