namespace slotgame
{
    [System.Serializable]
    public class Cell
    {
        public float yCoordinate;
        public SlotItem myItem;
        public bool isOccupied;

        public void SetEmpty()
        {
            isOccupied = false;
            myItem = null;
        }

        public void SetOccupied(SlotItem item)
        {
            isOccupied = true;
            myItem = item;
        }
    }
}