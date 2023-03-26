namespace LM{
    
public abstract class State
{
    public abstract void EnterState(PlayerLocomotion playerLocomotion);
    public abstract void OnUpdate(PlayerLocomotion playerLocomotion);
    public abstract void ExitState(PlayerLocomotion playerLocomotion, State newState);
}

}
