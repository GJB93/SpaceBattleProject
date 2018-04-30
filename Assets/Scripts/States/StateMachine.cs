public class StateMachine {

    public State state;

    public StateMachine()
    {

    }

    public void Update()
    {
        state.Update();
    }

    public void ChangeState(State newState)
    {
        if (state != null)
        {
            state.Exit();
        }
        state = newState;

        if (state != null)
        {
            state.Enter();
        }
    }
}
