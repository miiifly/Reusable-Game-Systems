namespace RECON.Utilites.Points
{
	public interface IPointComponent
	{
		void AssignScriptableIfEmpty();
		void UpdatePoint();
		void DeletePoint();
		bool IsAssigne { get; }
	}
}

