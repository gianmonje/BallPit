using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    private StateMachine<GameManager> stateMachine;
    static public StateMachine<GameManager> GameManagerStateMachine {
        get {
            return Instance.stateMachine;
        }
    }

    public PlayerController PlayerController { get => playerController; }
    public BallSpawner BallSpawner { get => ballSpawner; }
    public PoolManager PoolManager { get => poolManager; }
    public MainMenuController MainMenuController { get => mainMenuController; }
    public GameplayHUDController GameplayHUDController { get => gameplayHUDController; }

    [SerializeField] private PlayerController playerController;
    [SerializeField] private  BallSpawner ballSpawner;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private MainMenuController mainMenuController;
    [SerializeField] private GameplayHUDController gameplayHUDController;

    public override void Initialize() {
        stateMachine = new StateMachine<GameManager>(this);
    }

    //Initialization
    private void Start() {
        stateMachine.SetState<MainMenuState>();
    }

    private void Update() {
        if (stateMachine != null) stateMachine.Update();
    }

    private void FixedUpdate() {
        if (stateMachine != null) stateMachine.FixedUpdate();
    }
}
