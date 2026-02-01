using UnityEngine;


namespace _Project.Develop.Runtime.Utilities.Loading_Screen
{
	public class StandardLoadingScreen : MonoBehaviour, ILoadingScreen
	{
		public bool IsShown => gameObject.activeSelf;

		public void Awake ()
		{
			Hide();
			DontDestroyOnLoad(this);
		}

		public void Show ()
		{
			gameObject.SetActive(true);
		}

		public void Hide ()
		{
			gameObject.SetActive(false);
		}
	}
}
