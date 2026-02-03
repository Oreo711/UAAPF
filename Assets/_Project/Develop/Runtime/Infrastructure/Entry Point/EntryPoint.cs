using System;
using System.Collections;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Config_Management;
using _Project.Develop.Runtime.Utilities.Loading_Screen;
using _Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;


namespace _Project.Develop.Runtime.Infrastructure.Entry_Point
{
	public class EntryPoint : MonoBehaviour
	{
		private void Awake ()
		{
			SetupAppSettings();

			DIContainer projectContainer = new DIContainer();

			ProjectContextRegistrations.Process(projectContainer);

			projectContainer.Resolve<ICoroutinePerformer>().StartCoroutine(Initialize(projectContainer));
		}

		private void SetupAppSettings ()
		{
			QualitySettings.vSyncCount  = 0;
			Application.targetFrameRate = 60;
		}

		private IEnumerator Initialize (DIContainer container)
		{
			ILoadingScreen       loadingScreen = container.Resolve<ILoadingScreen>();
			SceneSwitcherService sceneSwitcher = container.Resolve<SceneSwitcherService>();

			loadingScreen.Show();

			yield return container.Resolve<ConfigProviderService>().LoadAsync();

			yield return new WaitForSeconds(1f);

			loadingScreen.Hide();

			yield return sceneSwitcher.SwitchToAsync(Scenes.MainMenu);
		}
	}
}
