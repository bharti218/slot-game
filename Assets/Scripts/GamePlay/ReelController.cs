using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace slotgame
{
    public class ReelController : MonoBehaviour
    {
        [SerializeField] private GameObject reelItem;
        [SerializeField] private RectTransform myRect;
        [SerializeField] private SlotGameScriptableObject slotData;

        public List<SlotItem> mySlotItems = new List<SlotItem>();
        public List<SlotItem> hiddenSlotItems = new List<SlotItem>();
        public List<Cell> cells = new List<Cell>();

        private float myReelAnimDuration;
        private float myInitYPos;
        private float mySlotItemHt;
        private float reelHt;


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

                cell.yCoordinate = (i+.5f)* mySlotItemHt - reelHt / 2;
                cells.Add(cell);
            }

        }

        public void Spin()
        {
            for (int i = 0; i < mySlotItems.Count; i++)
            {
                mySlotItems[i].gameObject.SetActive(true);
                IconData iconData = slotData.iconSprites[Random.Range(0, slotData.iconSprites.Length - 1)];
                mySlotItems[i].InitSlotItem(iconData.myID, iconData.normalSprite, iconData.highlightSprite);
                SlideIcon(mySlotItems[i].myRect, cells[i].yCoordinate, myReelAnimDuration);
                SetCellOccupied(mySlotItems[i], cells[i].yCoordinate);
                cells[i].SetOccupied(mySlotItems[i]);
            }
        }

        public void ResetReel()
        {
            for (int i = 0; i < mySlotItems.Count; i++)
            {
                mySlotItems[i].ResetItem(myInitYPos);
            }

            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].SetEmpty();
            }
        }

        internal void HighLightMaxOccuringSymobol(ICON_ID maxfrequentIconID)
        {
            hiddenSlotItems.Clear();
            for (int i = 0; i < mySlotItems.Count; i++)
            {
                //Hide and reset slot item.
                if (mySlotItems[i].gameObject.activeSelf && mySlotItems[i].iconID == maxfrequentIconID)
                {
                    mySlotItems[i].PlayBlastAnimation();
                    hiddenSlotItems.Add(mySlotItems[i]);
                    SetCellEmpty(mySlotItems[i]);
                }
            }
        }

        public int GetHighLightedSymbolCount()
        {
            return hiddenSlotItems.Count;
        }

        public void DestroyMaxOccuringSymobol(ICON_ID maxfrequentIconID)
        {
            StartCoroutine(DestroyMaxOccuringSymobolCoroutine(maxfrequentIconID));
        }

        public IEnumerator DestroyMaxOccuringSymobolCoroutine(ICON_ID maxfrequentIconID)
        {
            yield return new WaitForSeconds(1f);


            if (hiddenSlotItems.Count > 0)
            {
                for (int i = 0; i < hiddenSlotItems.Count; i++)
                {
                    hiddenSlotItems[i].myRect.anchoredPosition = new Vector2(hiddenSlotItems[i].myRect.anchoredPosition.x, myInitYPos);
                    hiddenSlotItems[i].gameObject.SetActive(false);

                }
                MoveSlotItems();

            }

        }


        private void MoveSlotItems()
        {
            int writeIndex = 0;
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].isOccupied)
                {
                    cells[writeIndex].isOccupied = cells[i].isOccupied;
                    cells[writeIndex].myItem = cells[i].myItem;
                    if (writeIndex != i)
                    {
                        cells[i].SetEmpty();
                    }
                    writeIndex++;
                }
            }

            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].isOccupied)
                    SlideIcon(cells[i].myItem.myRect, cells[i].yCoordinate, 1f);
            }
        }

        public void RespwanHiddenReels()
        {

            int activeSymbolCount = mySlotItems.Count - hiddenSlotItems.Count;
            for (int i = 0; i < hiddenSlotItems.Count; i++)
            {
                hiddenSlotItems[i].gameObject.SetActive(true);
                IconData iconData = slotData.iconSprites[Random.Range(0, slotData.iconSprites.Length - 1)];
                hiddenSlotItems[i].InitSlotItem(iconData.myID, iconData.normalSprite, iconData.highlightSprite);


                float finalPosY = activeSymbolCount * mySlotItemHt + (i+.5f)* mySlotItemHt - reelHt/2;
                SlideIcon(hiddenSlotItems[i].myRect, finalPosY, myReelAnimDuration);
                SetCellOccupied(hiddenSlotItems[i], finalPosY);
            }

        }

        private void SetCellOccupied(SlotItem item, float finalPos)
        {

            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].yCoordinate == finalPos)
                {
                    cells[i].myItem = item;
                    cells[i].isOccupied = true;
                    break;
                }

            }
        }

        private void SetCellEmpty(SlotItem item)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].myItem == item)
                {
                    cells[i].isOccupied = false;
                    cells[i].myItem = null;
                    break;
                }
            }
        }

        private void SlideIcon(RectTransform rectTransform, float finalPos, float duration)
        {
            Debug.Log("FinalPos:" + finalPos);
            rectTransform.DOAnchorPosY(finalPos, duration).SetEase(Ease.OutExpo).OnComplete(() => { AudioManager.Instance.PlayReelStopSound(); });
        }
    }
}