namespace RECON.Utilites.RandomSelector
{
	public interface IPriorityModel<TPriority>
	{
		TPriority Mode { get; }
		float Priority { get; }
	}
}
