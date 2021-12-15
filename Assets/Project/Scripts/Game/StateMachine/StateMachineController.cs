using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineController
{
    private GameObject currentObject;
    private State currentState;
    private bool isEnterCompleted = false;

    public void InitStateMachine(GameObject obj)
    {
        currentObject = obj;
    }

    public GameObject GetCurrentObject()
    {
        return currentObject;
    }

    public void SetState(State state)
    {
        isEnterCompleted = false;
        if(currentState != null) currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
        isEnterCompleted = true;
    }

    public State GetState()
    {
        return currentState;
    }

    public void UpdateState()
    {
        if(currentState != null || isEnterCompleted) currentState.OnStateUpdate();
    }


}
