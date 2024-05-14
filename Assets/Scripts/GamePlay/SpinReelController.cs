using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace slotgame
{
    public class SpinReelController : MonoBehaviour
    {

        [SerializeField] private GameObject reelItem;
        [SerializeField] private RectTransform myRect;
        [SerializeField] private SlotGameScriptableObject slotData;
        public List<SlotItem> mySlotItems = new List<SlotItem>();

        private float myReelAnimDuration;
        private float myInitYPos;
        private float mySlotItemHt;
        private float reelHt;

        public List<Cell> cells = new List<Cell>();


        [SerializeField] RectTransform part_01;
        [SerializeField] RectTransform part_02;
        [SerializeField] RectTransform initPos;
        [SerializeField] RectTransform finalPos;

        private void Start()
        {
            SpinReel();
        }

        public void InitReel(float reelWidth, float reelHeight, int iconCount, float animDuration)
        {
            myReelAnimDuration = animDuration;
            myInitYPos = reelHeight - reelHeight / iconCount;
            mySlotItemHt = reelHeight / iconCount;
            myRect.sizeDelta = new Vector2(reelWidth, reelHeight);
            reelHt = reelHeight;
            for (int i = 0; i < iconCount; i++)
            {
                GameObject icon = Instantiate(reelItem, transform);
                SlotItem slotItem = icon.GetComponent<SlotItem>();
                slotItem.myRect.sizeDelta = new Vector2(reelWidth, reelHeight / iconCount);
                slotItem.myRect.anchoredPosition = new Vector2(slotItem.myRect.anchoredPosition.x, myInitYPos);
                slotItem.gameObject.SetActive(false);
                mySlotItems.Add(slotItem);

                Cell cell = new Cell();

                cell.yCoordinate = (i + .5f) * mySlotItemHt - reelHt / 2;
                cells.Add(cell);
            }

        }
        float speed = 1;

        public void SpinReel()
        {

            // float midY = 0f;
            // part_01.anchoredPosition = initPos.anchoredPosition;
            // part_02.anchoredPosition = initPos.anchoredPosition;

            //part_01.DOAnchorPosY()
            speed = 2000;
        }

      


        private void Update()
        {

            float yPos2 = part_02.anchoredPosition.y - speed * Time.deltaTime;
            if (yPos2 < finalPos.anchoredPosition.y)
                yPos2 = initPos.anchoredPosition.y;

            part_02.anchoredPosition = new Vector2(part_02.anchoredPosition.x, yPos2);


            float yPos1 = part_01.anchoredPosition.y - speed * Time.deltaTime;
            if (yPos1 < finalPos.anchoredPosition.y)
                yPos1 = initPos.anchoredPosition.y;


            part_01.anchoredPosition = new Vector2(part_01.anchoredPosition.x, yPos1);



        }

    }
}