using System.Collections.Generic;
using TMPro;
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
        [SerializeField] float betAmount;
        [SerializeField] float balance;
        public float winningFactor;



        [Space(20)]
        [SerializeField] GameObject reel;
        [SerializeField] RectTransform rectTransform;
        [SerializeField] TextMeshProUGUI minMatchingTxt;
        [SerializeField] GameObject vSeparator;
        [SerializeField] GameObject hSeparator;
        [SerializeField] Transform hGridPrerent;
        [SerializeField] Transform vGridPrerent;





        public static LevelGenerator Instance;
        public List<ReelController> myReels = new List<ReelController>();


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            minMatchingTxt.text = "Min Matching Fruits: " + maxMatchSymbol;
            for (int i = 0; i<maxCol; i++)
            {
                GameObject reelObject = Instantiate(reel, transform);
                ReelController reelController = reelObject.GetComponent<ReelController>();
                reelController.InitReel(rectTransform.sizeDelta.x/maxCol, rectTransform.sizeDelta.y, maxRow, animDuration);
                myReels.Add(reelController);
            }

            PlayerManager.Instance.SetBalance(balance);
            PlayerManager.Instance.SetBetAmount(betAmount);
            GenerateSepartorGrid();
        }


        private void GenerateSepartorGrid()
        {
            for (int i = 0; i < maxCol; i++)
            {
                GameObject go = Instantiate(vSeparator, hGridPrerent);
            }

            for (int i = 0; i < maxRow; i++)
            {
                GameObject go = Instantiate(hSeparator, vGridPrerent);
            }
        }

    }

}