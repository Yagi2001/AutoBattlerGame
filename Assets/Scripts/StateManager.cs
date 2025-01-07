using UnityEngine;
using System.Collections;

public enum GameState
{
    StrategyPhase,
    BattlePhase
}

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }
    public GameState CurrentGameState { get; private set; } = GameState.StrategyPhase;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy( gameObject );
            return;
        }
        Instance = this;
        DontDestroyOnLoad( gameObject );
    }

    public void StartBattlePhase()
    {
        CurrentGameState = GameState.BattlePhase;
    }

    public void StartStrategyPhase()
    {
        Debug.Log( "Strategy Phase Active" );
        CurrentGameState = GameState.StrategyPhase;
    }

    public void CheckForUnits()
    {
        GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag( "EnemyUnit" );
        GameObject[] allyUnits = GameObject.FindGameObjectsWithTag( "AllyUnit" );

        if (enemyUnits.Length <= 1 && allyUnits.Length >= 1) //This needs a polishing but currently works
        {
            StartCoroutine( SwitchToStrategyPhase() );
        }
    }

    private IEnumerator SwitchToStrategyPhase()
    {
        yield return new WaitForSeconds( 3f );
        StartStrategyPhase();
    }
}