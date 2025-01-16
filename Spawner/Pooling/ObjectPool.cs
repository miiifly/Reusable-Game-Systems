using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RECON.Utilites.Spawner.Pool
{
	public class ObjectPool<T> : IObjectPool<T> where T : IBaseSpawnable
	{
		private readonly Dictionary<int, Queue<T>> _pools = new Dictionary<int, Queue<T>>();
		private readonly Dictionary<int, T> _prefabs = new Dictionary<int, T>();

		event Action<T> IObjectPool<T>.OnSpawn
		{
			add { _onSpawn += value; }
			remove { _onSpawn -= value; }
		}
		event Action<T> IObjectPool<T>.OnDespawn
		{
			add { _onDespawn += value; }
			remove { _onDespawn -= value; }
		}

		private Action<T> _onSpawn;
		private Action<T> _onDespawn;

		private readonly Transform _parent;
		private DiContainer _container;
		public ObjectPool(IEnumerable<T> prefabs, Transform parent, DiContainer container, int initialCapacity)
		{
			_parent = parent;
			_container = container;
			Initialize(prefabs, initialCapacity);
		}

		public void Initialize(IEnumerable<T> prefabs, int initialCapacity)
		{
			foreach (var prefab in prefabs)
			{
				var key = prefab.SpawnableTypeID;

				if (!_prefabs.ContainsKey(key))
				{
					_prefabs.Add(key, prefab);

					_pools.Add(key, new Queue<T>());
					for (int i = 0; i < initialCapacity; i++)
					{
						T spawnable = InitializeObject(prefab);
						_pools[key].Enqueue(spawnable);
					}
				}
			}
		}

		T IObjectPool<T>.Get(int typeId)
		{
			if (_pools.ContainsKey(typeId) && _pools[typeId].Count > 0)
			{
				T obj = _pools[typeId].Dequeue();
				obj.GameObject.SetActive(true);
				_onSpawn?.Invoke(obj);
				return obj;
			}

			if (_prefabs.ContainsKey(typeId))
			{
				var newObj = _container.InstantiatePrefab(_prefabs[typeId].GameObject);
				newObj.transform.SetParent(_parent, false);
				T spawnable = newObj.GetComponent<T>();
				_onSpawn?.Invoke(spawnable);
				return spawnable;
			}

			return default;
		}


		void IObjectPool<T>.Release(T obj)
		{
			obj.GameObject.SetActive(false);
			if (_pools.ContainsKey(obj.SpawnableTypeID))
			{
				_pools[obj.SpawnableTypeID].Enqueue(obj);
				_onDespawn?.Invoke(obj);
			}
			else
			{
				Debug.LogWarning($"Pool for type ID {obj.SpawnableTypeID} does not exist.");
			}
		}

		public void Prepare(int typeId, int count)
		{
			if (_prefabs.TryGetValue(typeId, out var prefab))
			{
				for (int i = 0; i < count; i++)
				{
					T spawnable = InitializeObject(prefab);
					_pools[typeId].Enqueue(spawnable);
				}
			}
			else
			{
				Debug.LogWarning($"Pool for type ID {typeId} does not exist.");
			}
		}

		private T InitializeObject(T prefab)
		{
			var obj = _container.InstantiatePrefab(prefab.GameObject);
			obj.transform.SetParent(_parent, false);
			obj.gameObject.SetActive(false);
			return obj.GetComponent<T>();
		}
	}
}

