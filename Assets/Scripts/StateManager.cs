using UnityEngine;

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
        CurrentGameState = GameState.StrategyPhase;
    }
}