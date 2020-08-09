using UnityEngine;

[CreateAssetMenu(fileName = "levelDate", menuName = "Scriptable Object/levelDate")]
public class InformationLvl : ScriptableObject
{
    public GameInfoLVL[] level = new GameInfoLVL[3];
}

[System.Serializable]
public class GameInfoLVL
{
    public int informationProgress;
    public int informationAsteroid;
    public bool informationActiveLVL;
}