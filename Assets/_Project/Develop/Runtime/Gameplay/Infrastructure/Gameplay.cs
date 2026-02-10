using System;
using System.Collections.Generic;

using System.Linq;
using _Project.Develop.Runtime.Meta;
using _Project.Develop.Runtime.Utilities.Config_Management.Configs.Scripts;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;


namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class Gameplay : MonoBehaviour
	{
		private GameState            _gameState;
		private GameplayConfig       _config;
		private SceneSwitcherService _sceneSwitcher;

		private List<char> _chars;
		private char[]     _sequence;
		private bool       _isSetup;

		private readonly List<char> _input = new();

		public void Initialize (GameState gameState, GameplayConfig config, SceneSwitcherService sceneSwitcher)
		{
			_gameState     = gameState;
			_config        = config;
			_sceneSwitcher = sceneSwitcher;
		}

		public void Setup ()
		{
			_chars = GetChars();

			_sequence = new char[_config.SequenceLength];

			for (int i = 0; i < _config.SequenceLength; i++)
			{
				int charIndex = Random.Range(0, _chars.Count);
				_sequence[i] = _chars[charIndex];
			}

			Debug.Log(new string(_sequence));

			_isSetup = true;
		}

		private List<char> GetChars ()
		{
			char[] chars;

			switch (_gameState.GameMode)
			{
				case GameMode.numbers:
					chars = _config.Numbers;
					break;
				case GameMode.letters:
					chars = _config.Letters;
					break;
				default:
					throw new InvalidOperationException();
			}

			return chars.ToList();
		}

		private void Update ()
		{
			if (!_isSetup)
				return;

			if (!Input.anyKeyDown)
				return;

			string inputString = Input.inputString;

			if (inputString.Length == 1 && _chars.Contains(inputString[0]))
			{
				_input.Add(inputString[0]);

				Debug.Log(inputString[0]);

				if (_sequence.Length == _input.Count)
				{
					ValidateInput();
				}
			}
		}

		private void ValidateInput ()
		{
			bool isValidInput = _sequence.SequenceEqual(_input);

			if (isValidInput)
			{
				_sceneSwitcher.SwitchTo(Scenes.MainMenu);
				return;
			}

			_sceneSwitcher.SwitchTo(Scenes.Gameplay);
		}
	}
}
