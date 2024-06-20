using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
    private IState<Bot> currentState;

    private void Start()
    {
        ChangeState(new IdleState());
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

}
