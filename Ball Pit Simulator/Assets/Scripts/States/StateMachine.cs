public class StateMachine<T> {
    private T context;
    private State<T> state;

    public StateMachine(T context) {
        this.context = context;
    }

    public State<T> CurrentState {
        get { return state; }
    }

    public void SetState<U>() where U : State<T>, new() {
        if (state != null) state.OnExit(context);
        state = new U {
            stateMachine = this
        };
        state.OnEnter(context);
    }

    public void Update() {
        if(state == null) return;
        state.OnUpdate(context);
    }

    public void FixedUpdate() {
        if (state == null) return;
        state.OnFixedUpdate(context);
    }
}
