using System;

namespace RECON.Utilites.Spawner
{
	public interface ISpawner<T> where T : IBaseSpawnable
	{
		event Action<T> OnSpawn;
		event Action<T> OnDespawn;

		void ClearSpawnedObjects();
		void Spawn(T spawnPrefab, bool setParent, Action<T> spawnedCallback);
		void Spawn(int spawnId, bool setParent, Action<T> spawnedCallback);
		void Despawn(T despawnPrefab);
	}
}

