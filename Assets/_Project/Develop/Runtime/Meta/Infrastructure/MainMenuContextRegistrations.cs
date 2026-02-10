using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEditor;


namespace _Project.Develop.Runtime.Meta.Infrastructure
{
	public class MainMenuContextRegistrations
	{
		public static void Process (DIContainer container)
		{
			container.RegisterAsSingle(CreateGameModeSelectionService);
		}

		private static GameModeSelectionService CreateGameModeSelectionService (DIContainer c)
		{
			return new GameModeSelectionService(c.Resolve<ICoroutinePerformer>(), c.Resolve<GameState>(), c.Resolve<SceneSwitcherService>());
		}
	}
}
