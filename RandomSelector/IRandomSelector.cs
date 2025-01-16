using System.Collections.Generic;

namespace RECON.Utilites.RandomSelector
{
	public interface IRandomSelector<Tmodel, Tpriority> where Tmodel : IPriorityModel<Tpriority>
	{
		Tmodel SelectOption(List<Tmodel> items);
	}
}