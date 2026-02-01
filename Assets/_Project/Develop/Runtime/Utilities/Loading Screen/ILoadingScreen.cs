namespace _Project.Develop.Runtime.Utilities.Loading_Screen
{
	public interface ILoadingScreen
	{
		bool IsShown {get;}

		void Show ();

		void Hide ();
	}
}
