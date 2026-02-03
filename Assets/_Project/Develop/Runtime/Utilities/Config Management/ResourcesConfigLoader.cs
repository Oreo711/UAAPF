using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Utilities.Config_Management.Configs.Scripts;
using UnityEngine;


namespace _Project.Develop.Runtime.Utilities.Config_Management
{
	public class ResourcesConfigLoader : IConfigLoader
	{
		private readonly ResourceLoader _resources;

		private readonly Dictionary<Type, string> _paths = new();

		public ResourcesConfigLoader (ResourceLoader resources)
		{
			_resources = resources;

			_paths.Add(typeof(GameplayConfig), "Configs/GameplayConfig");
		}

		public IEnumerator LoadAsync (Action<Dictionary<Type, object>> onLoaded)
		{
			Dictionary<Type, object> loadedConfigs = new();

			foreach (KeyValuePair<Type, string> resourcePath in _paths)
			{
				ScriptableObject config = _resources.Load<ScriptableObject>(resourcePath.Value);
				loadedConfigs.Add(resourcePath.Key, config);

				yield return null;
			}

			onLoaded?.Invoke(loadedConfigs);
		}
	}
}
