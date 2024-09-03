public class MainMenuState : State<GameManager> {
    public override void OnEnter(GameManager gameManager) {
        gameManager.MainMenuController.Initialize();
        gameManager.GameplayHUDController.SetVisibilitiy(false);
    }

    public override void OnExit(GameManager gameManager) {
        gameManager.MainMenuController.DeInitialize();
    }

    public override void OnUpdate(GameManager gameManager) {
    }

    public override void OnFixedUpdate(GameManager gameManager) {
    }
}