using System;
using System.Collections;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;


namespace _Project.Develop.Runtime.Meta.Infrastructure
{
	public class MainMenuBootstrap : SceneBootstrap
	{
		private DIContainer              _container;
		private ICoroutinePerformer      _coroutinePerformer;
		private GameState                _gameState;
		private SceneSwitcherService     _sceneSwitcher;
		private GameModeSelectionService _gameModeSelectionService;

		public override void ProcessRegistrations (DIContainer container, IInputSceneArgs sceneArgs = null)
		{
			_container = container;

			MainMenuContextRegistrations.Process(_container);
		}

		public override IEnumerator Initialize ()
		{
			_sceneSwitcher            = _container.Resolve<SceneSwitcherService>();
			_coroutinePerformer       = _container.Resolve<ICoroutinePerformer>();
			_gameState                = _container.Resolve<GameState>();
			_gameModeSelectionService = _container.Resolve<GameModeSelectionService>();

			yield break;
		}

		public override void Run ()
		{
			Debug.Log("Choose game mode. (1-numbers, 2-letters)");

			_gameModeSelectionService.Run();
		}
	}
}
