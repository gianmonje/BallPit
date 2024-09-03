using UnityEngine;

public class GameplayState : State<GameManager> {
    public override void OnEnter(GameManager gameManager) {
        gameManager.PlayerController.Initialize();
        gameManager.BallSpawner.SpawnObjectsWithinArea(gameManager.MainMenuController.ballsToSpawnCount);
        gameManager.GameplayHUDController.SetVisibilitiy(true);
    }

    public override void OnExit(GameManager gameManager) {
        gameManager.PoolManager.RemoveAllBalls();
        gameManager.PlayerController.DeInitialize();
    }

    public override void OnUpdate(GameManager gameManager) {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.GameManagerStateMachine.SetState<MainMenuState>();
        }

        gameManager.PlayerController.HandlePickupAndThrow();
    }

    public override void OnFixedUpdate(GameManager gameManager) {
        gameManager.PlayerController.HandleMovement();
    }
}