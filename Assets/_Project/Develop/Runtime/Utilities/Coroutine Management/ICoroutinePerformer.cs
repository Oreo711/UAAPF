using System.Collections;
using UnityEngine;

public interface ICoroutinePerformer
{
	Coroutine StartPerformance (IEnumerator coroutineFunction);
	void StopPerformance (Coroutine coroutine);
}
