using UnityEngine;

namespace RECON.Utilites.Spawner
{
	public interface IBaseSpawnable
	{
		GameObject GameObject { get; }

		int SpawnableTypeID { get;  }
	}
}

