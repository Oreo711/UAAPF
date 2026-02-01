using System;
using System.Collections;
using UnityEngine;

public class CoroutinePerformer : MonoBehaviour, ICoroutinePerformer
{
    private void Awake ()
    {
        DontDestroyOnLoad(this);
    }

    public Coroutine StartPerformance (IEnumerator coroutineFunction)
    {
        return StartCoroutine(coroutineFunction);
    }

    public void StopPerformance (Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }
}
