using UnityEngine;

namespace LM{
    
public abstract class State
{
    public virtual void EnterState(PlayerLocomotion playerLocomotion) {
        Debug.Log("Entering " + this.GetType().Name);
    }
    public abstract void OnUpdate(PlayerLocomotion playerLocomotion);
    public abstract void ExitState(PlayerLocomotion playerLocomotion, State newState);
}

}
