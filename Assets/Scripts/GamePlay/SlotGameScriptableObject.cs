using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlotGameData", menuName = "ScriptableObjects/SlotGameDataScriptableObject", order = 1)]
public class SlotGameScriptableObject : ScriptableObject
{
    public IconData[] iconSprites;
}

[System.Serializable]
public class IconData
{
    public ICON_ID myID;
    public Sprite normalSprite;
    public Sprite highlightSprite;

}

public enum ICON_ID
{
    S1 = 1,
    S2 = 2,
    S3 = 3,
    S4 = 4,
    S5 = 5,
    S6 = 6,
    S7 = 7,
    S8 = 8,
    S9 = 9
}