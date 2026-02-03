using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Config_Management;
using _Project.Develop.Runtime.Utilities.Coroutine_Management;
using _Project.Develop.Runtime.Utilities.Loading_Screen;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace _Project.Develop.Runtime.Infrastructure.Entry_Point
{
	public class ProjectContextRegistrations
	{
		public static void Process (DIContainer container)
		{
			container.RegisterAsSingle(CreateConfigProviderService);

			container.RegisterAsSingle(CreateResourceLoader);

			container.RegisterAsSingle<ICoroutinePerformer>(CreateCoroutinePerformer);

			container.RegisterAsSingle(CreateSceneLoaderService);

			container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreen);

			container.RegisterAsSingle(CreateSceneSwitcherService);

			container.RegisterAsSingle(CreateGameState);
		}

		private static GameState CreateGameState (DIContainer c) => new GameState();

		private static SceneSwitcherService CreateSceneSwitcherService (DIContainer c)
		{
			return new SceneSwitcherService(
				c.Resolve<SceneLoaderService>(),
				c.Resolve<ILoadingScreen>(),
										c);
		}

		private static SceneLoaderService CreateSceneLoaderService (DIContainer c)
		{
			return new SceneLoaderService();
		}

		private static ConfigProviderService CreateConfigProviderService (DIContainer c)
		{
			ResourceLoader resourceLoader = c.Resolve<ResourceLoader>();

			ResourcesConfigLoader resourcesConfigLoader = new ResourcesConfigLoader(resourceLoader);

			return new ConfigProviderService(resourcesConfigLoader);
		}

		private static ResourceLoader CreateResourceLoader (DIContainer c)
		{
			return new ResourceLoader();
		}

		private static CoroutinePerformer CreateCoroutinePerformer (DIContainer c)
		{
			ResourceLoader resourceLoader = c.Resolve<ResourceLoader>();

			CoroutinePerformer coroutinePerformerPrefab = resourceLoader.Load<CoroutinePerformer>("Utilities/Coroutine Performer");

			return Object.Instantiate(coroutinePerformerPrefab);
		}

		private static StandardLoadingScreen CreateLoadingScreen (DIContainer c)
		{
			ResourceLoader resourceLoader = c.Resolve<ResourceLoader>();

			StandardLoadingScreen standardLoadingScreen = resourceLoader.Load<StandardLoadingScreen>("Utilities/Standard Loading Screen");

			return Object.Instantiate(standardLoadingScreen);
		}
	}
}
