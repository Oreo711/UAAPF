using _Project.Develop.Runtime.Meta;


namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameState
	{
		public GameMode GameMode {get; private set;} = GameMode.numbers;

		public void SetGameMode (GameMode gameMode)
		{
			GameMode = gameMode;
		}
	}
}
