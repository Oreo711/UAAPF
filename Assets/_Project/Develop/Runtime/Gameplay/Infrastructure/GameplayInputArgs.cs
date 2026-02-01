namespace _Project.Develop.Runtime.Utilities.SceneManagement
{
	public class GameplayInputArgs : IInputSceneArgs
	{
		public GameplayInputArgs (int levelNumber)
		{
			LevelNumber = levelNumber;
		}

		public int LevelNumber {get;}
	}
}