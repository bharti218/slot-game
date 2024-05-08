using UnityEngine;
using UnityEngine.UI;

namespace slotgame
{

    public class SlotItem : MonoBehaviour
    {
        [SerializeField] private Image myImg;
        [SerializeField] private GameObject blastAnimation;

        public RectTransform myRect;
        public ICON_ID iconID;
        private Sprite myHighlightedSp;

        public void InitSlotItem(ICON_ID myID, Sprite normalSp, Sprite highlightedSp)
        {
            iconID = myID;
            myImg.enabled = true;
            myImg.sprite = normalSp;
            myHighlightedSp = highlightedSp;
            gameObject.name = myID.ToString();
        }


        public float GetMyHt()
        {
            return myRect.sizeDelta.x;
        }

        public void ResetItem(float finalY)
        {
            myRect.anchoredPosition = new Vector2(myRect.anchoredPosition.x, finalY);
            gameObject.SetActive(false);
        }


        public void PlayBlastAnimation()
        {
            myImg.enabled = false;
            blastAnimation.SetActive(true);
            Invoke(nameof(HideBlastAnimation), .7f);
        }

        private void HideBlastAnimation()
        {
            blastAnimation.SetActive(false);
        }
    }
}