using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace _Project.Develop.Runtime.Utilities.SceneManagement
{
	public class SceneLoaderService
	{
		public IEnumerator LoadAsync (string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
		{
			AsyncOperation wait = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

			yield return new WaitUntil(() => wait.isDone);
		}

		public IEnumerator UnloadAsync (string sceneName)
		{
			AsyncOperation wait = SceneManager.UnloadSceneAsync(sceneName);

			yield return new WaitUntil(() => wait.isDone);
		}
	}
}
