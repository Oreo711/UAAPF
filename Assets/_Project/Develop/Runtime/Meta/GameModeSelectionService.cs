using System.Collections;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;


namespace _Project.Develop.Runtime.Meta
{
	public class GameModeSelectionService
	{
		private ICoroutinePerformer  _coroutinePerformer;
		private GameState            _gameState;
		private SceneSwitcherService _sceneSwitcher;

		private bool _inputReceived;

		public GameModeSelectionService (ICoroutinePerformer coroutinePerformer, GameState gameState, SceneSwitcherService sceneSwitcher)
		{
			_coroutinePerformer = coroutinePerformer;
			_gameState          = gameState;
			_sceneSwitcher      = sceneSwitcher;
		}

		public void Run ()
		{
			_coroutinePerformer.StartCoroutine(ProcessInput());
		}

		private IEnumerator ProcessInput()
		{
			while (!_inputReceived)
			{
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					_inputReceived = true;
					_coroutinePerformer.StartCoroutine(ProcessStartGameplay(GameMode.numbers));
				}
				else if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					_inputReceived = true;
					_coroutinePerformer.StartCoroutine(ProcessStartGameplay(GameMode.letters));
				}

				yield return null;
			}
		}

		private IEnumerator ProcessStartGameplay (GameMode gameMode)
		{
			_gameState.SetGameMode(gameMode);

			yield return _sceneSwitcher.SwitchToAsync(Scenes.Gameplay);
		}
	}
}
