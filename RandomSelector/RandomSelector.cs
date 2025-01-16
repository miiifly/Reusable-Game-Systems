using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace RECON.Utilites.RandomSelector
{
	public class RandomSelector<Tmodel, Tpriority> : IInitializable, IRandomSelector<Tmodel, Tpriority> where Tmodel : IPriorityModel<Tpriority>
	{
		private Random _random;
		public void Initialize()
		{
			_random = new Random();
		}

		public Tmodel SelectOption(List<Tmodel> items)
		{
			var totalPriority = items.Sum(x => x.Priority);
			var randomNumber = _random.Next(0, (int)totalPriority);
			var index = -1;
			var currentValue = 0;

			do
			{
				index++;
				currentValue += (int)items[index].Priority;
			}
			while (currentValue < randomNumber);

			return items[index];
		}
	}
}