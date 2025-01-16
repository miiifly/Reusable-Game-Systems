using System;

namespace RECON.Utilites.Spawner.Pool
{
	public interface IObjectPool<T> where T : IBaseSpawnable
	{
		event Action<T> OnSpawn;
		event Action<T> OnDespawn;
		T Get(int typeId);
		void Release(T obj);
		void Prepare(int typeId, int count);
	}
}
