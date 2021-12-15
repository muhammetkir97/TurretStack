using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateMachineController controller;

    public abstract void OnStateUpdate();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public State(StateMachineController controller)
    {
        this.controller = controller;
    }
}
