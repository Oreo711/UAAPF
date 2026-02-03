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
		private DIContainer          _container;
		private ICoroutinePerformer  _coroutinePerformer;
		private GameState            _gameState;
		private SceneSwitcherService _sceneSwitcher;

		public override void ProcessRegistrations (DIContainer container, IInputSceneArgs sceneArgs = null)
		{
			_container = container;

			MainMenuContextRegistrations.Process(_container);
		}

		public override IEnumerator Initialize ()
		{
			_sceneSwitcher      = _container.Resolve<SceneSwitcherService>();
			_coroutinePerformer = _container.Resolve<ICoroutinePerformer>();
			_gameState          = _container.Resolve<GameState>();

			yield break;
		}

		public override void Run ()
		{
			Debug.Log("Choose game mode. (1-numbers, 2-letters)");
		}

		private void Update ()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				_coroutinePerformer.StartCoroutine(ProcessStartGameplay(GameMode.numbers));
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				_coroutinePerformer.StartCoroutine(ProcessStartGameplay(GameMode.letters));
			}
		}

		private IEnumerator ProcessStartGameplay (GameMode gameMode)
		{
			_gameState.SetGameMode(gameMode);

			yield return _sceneSwitcher.SwitchToAsync(Scenes.Gameplay);
		}
	}
}
