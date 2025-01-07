using UnityEngine;

[CreateAssetMenu( fileName = "GameData", menuName = "Game/GameData" )]
public class GameData : ScriptableObject
{
    public int currentLevel;
    public int nextWave;
}