public abstract class State<T> {
    public StateMachine<T> stateMachine;

    public abstract void OnEnter(T context);

    public abstract void OnUpdate(T context);

    public abstract void OnFixedUpdate(T context);

    public abstract void OnExit(T context);
}
