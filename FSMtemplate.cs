using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMTemplate : MonoBehaviour
{
    Coroutine stateRoutine;
    State activeState = State.None;
    readonly WaitForSeconds tick = new WaitForSeconds(0.05f);

    void Start()
    {
        SetState(State.Idle);
    }

    public enum State
    {
        None,
        Idle,
        Wander,
        Spin,
        Jump
    }

    public void SetState(State newState)
    {
        if (stateRoutine != null) StopCoroutine(stateRoutine);

        if (activeState != null)
            StartCoroutine(GetState(activeState, false, true));

        activeState = newState;
        stateRoutine = StartCoroutine(GetState(newState, true, false));
    }

    IEnumerator GetState(State id, bool enter, bool exit)
    {
        return id switch
        {
            State.Idle => State_Idle(enter, exit),
            State.Spin => State_Spin(enter, exit),
            State.Wander => State_Wander(enter, exit),
            State.Jump => State_Jump(enter, exit),
            _ => State_None(),
        };
    }

    IEnumerator State_None()
    {
        yield return null;
    }

    IEnumerator State_Idle(bool enter, bool exit)
    {
        if (enter)
        {
            print("entered " + activeState + " state");
            // some enter logic
        }
        if (exit)
        {
            print("exited " + activeState + " state");
            // some exit logic
            yield break;
        }

        print("idling...");
        // state update 
        yield return tick;

        // restart the state, running update only
        yield return null;
        stateRoutine = StartCoroutine(State_Idle(false, false));
    }

    IEnumerator State_Spin(bool enter, bool exit)
    {
        if (enter)
        {
            print("entered " + activeState + " state");

        }
        if (exit)
        {
            print("exited " + activeState + " state");
            yield break;
        }

        print("spinning...");
        yield return tick;

        yield return null;  
        stateRoutine = StartCoroutine(State_Spin(false, false));
    }

    IEnumerator State_Wander(bool enter, bool exit)
    {
        if (enter)
        {
            print("entered " + activeState + " state");

        }
        if (exit)
        {
            print("exited " + activeState + " state");
            yield break;
        }

        print("wandering...");
        yield return tick;

        yield return null;
        stateRoutine = StartCoroutine(State_Wander(false, false));
    }

    IEnumerator State_Jump(bool enter, bool exit)
    {
        if (enter)
        {
            print("entered " + activeState + " state");
        }
        if (exit)
        {
            print("exited " + activeState + " state");
            yield break;
        }

        print("jumping...");
        yield return tick;

        yield return null;
        stateRoutine = StartCoroutine(State_Jump(false, false));
    }
}
