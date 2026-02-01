using System;
using System.Collections;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Loading_Screen;
using Object = UnityEngine.Object;


namespace _Project.Develop.Runtime.Utilities.SceneManagement
{
	public class SceneSwitcherService
	{
		private readonly SceneLoaderService _sceneLoader;
		private readonly ILoadingScreen     _loadingScreen;
		private readonly DIContainer        _projectContainer;

		public SceneSwitcherService (SceneLoaderService sceneLoader, ILoadingScreen loadingScreen, DIContainer projectContainer)
		{
			_sceneLoader   = sceneLoader;
			_loadingScreen = loadingScreen;
			_projectContainer = projectContainer;
		}

		public IEnumerator SwitchTo (string sceneName, IInputSceneArgs sceneArgs = null)
		{
			_loadingScreen.Show();

			yield return _sceneLoader.LoadAsync(Scenes.Empty);
			yield return _sceneLoader.LoadAsync(sceneName);

			SceneBootstrap sceneBootstrap = Object.FindObjectOfType<SceneBootstrap>();

			if (!sceneBootstrap)	
			{
				throw new NullReferenceException($"{nameof(sceneBootstrap)} not found");
			}

			DIContainer sceneContainer = new DIContainer(_projectContainer);

			sceneBootstrap.ProcessRegistrations(sceneContainer, sceneArgs);

			yield return sceneBootstrap.Initialize();

			_loadingScreen.Hide();

			sceneBootstrap.Run();
		}
	}
}
