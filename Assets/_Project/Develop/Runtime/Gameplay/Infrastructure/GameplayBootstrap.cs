using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta;
using _Project.Develop.Runtime.Utilities.Config_Management;
using _Project.Develop.Runtime.Utilities.Config_Management.Configs.Scripts;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;


namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameplayBootstrap : SceneBootstrap
	{
		[SerializeField] private Gameplay _context;

		private DIContainer       _container;
		private GameplayInputArgs _inputArgs;

		public override void ProcessRegistrations (DIContainer container, IInputSceneArgs sceneArgs = null)
		{
			_container = container;

			if (sceneArgs is GameplayInputArgs inputArgs)
			{
				_inputArgs = inputArgs;
			}

			GameplayContextRegistrations.Process(_container, _inputArgs);
		}

		public override IEnumerator Initialize ()
		{
			ConfigProviderService configProvider = _container.Resolve<ConfigProviderService>();

			GameplayConfig config = configProvider.GetConfig<GameplayConfig>();

			_context.Initialize(
				_container.Resolve<GameState>(),
				config,
				_container.Resolve<SceneSwitcherService>());
			_context.Setup();

			yield break;
		}

		public override void Run ()
		{

		}
	}
}

