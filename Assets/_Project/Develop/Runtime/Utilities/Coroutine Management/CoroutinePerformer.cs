using UnityEngine;


namespace _Project.Develop.Runtime.Utilities.Coroutine_Management
{
    public class CoroutinePerformer : MonoBehaviour, ICoroutinePerformer
    {
        private void Awake ()
        {
            DontDestroyOnLoad(this);
        }
    }
}
