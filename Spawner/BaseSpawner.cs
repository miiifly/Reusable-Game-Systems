using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RECON.Utilites.Spawner
{
    public class BaseSpawner<T> : ISpawner<T> where T : IBaseSpawnable
    {
        public event Action<T> OnSpawn
        {
            add { _onSpawn += value; }
            remove { _onSpawn -= value; }
        }
        public event Action<T> OnDespawn
        {
            add { _onDespawn += value; }
            remove { _onDespawn -= value; }
        }

        private Action<T> _onSpawn;
        private Action<T> _onDespawn;

        private List<T> _spawnedObjects = new List<T>();

        private readonly Transform _spawnParent;
        private readonly DiContainer _container;
        private readonly IEnumerable<T> _spawnables;

        public BaseSpawner(Transform spawnParent,
            DiContainer container,
            IEnumerable<T> spawnebles)
        {
            _spawnParent = spawnParent;
            _container = container;
            _spawnables = spawnebles;
        }

        void ISpawner<T>.ClearSpawnedObjects()
        {
            foreach (var despawn in _spawnedObjects)
            {
                _onDespawn.Invoke(despawn);
            }

            _spawnedObjects.Clear();
        }

        void ISpawner<T>.Despawn(T despawnPrefab)
        {
            _onDespawn?.Invoke(despawnPrefab);

            _spawnedObjects.Remove(despawnPrefab);
        }

        void ISpawner<T>.Spawn(T spawnPrefab, bool setParent, Action<T> spawnedCallback) => SpawnInternal(spawnPrefab, setParent, spawnedCallback);

        void ISpawner<T>.Spawn(int spawnId, bool setParent, Action<T> spawnedCallback) => SpawnInternal(FindInPreset(spawnId), setParent, spawnedCallback);


        private T FindInPreset(int spawnTypeID)
        {
            foreach (var spanable in _spawnables)
            {
                if (spanable.SpawnableTypeID == spawnTypeID)
                {
                    return spanable;
                }
            }
            return default;
        }

        protected virtual void SpawnInternal(T spawnPrefab, bool setParent, Action<T> spawnedCallback)
        {
            var gameObject = _container.InstantiatePrefab(spawnPrefab.GameObject);
            var spawnable = gameObject.GetComponent<T>();
            _spawnedObjects.Add(spawnable);

            if (setParent)
            {
                gameObject.transform.SetParent(_spawnParent, false);
            }

            spawnedCallback?.Invoke(spawnable);
            _onSpawn?.Invoke(spawnable);
        }

        private void DestroySpawnedObjects(IEnumerable<T> objTODestroy)
        {
            foreach (var obj in objTODestroy)
            {
                if (obj?.GameObject != null)
                {
                    UnityEngine.Object.Destroy(obj.GameObject);
                }
            }
        }
    }
}

