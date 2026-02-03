using System.Collections;
using UnityEngine;

public interface ICoroutinePerformer
{
	Coroutine StartCoroutine (IEnumerator coroutineFunction);
	void StopCoroutine (Coroutine coroutine);
}
