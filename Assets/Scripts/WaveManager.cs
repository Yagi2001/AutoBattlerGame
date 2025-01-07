using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private GameData _gameData;

    private void LoadNextWave()
    {
        SceneManager.LoadScene( "Level" + _gameData.currentLevel + "Wave" + _gameData.nextWave );
        _gameData.nextWave++;
    }
}
