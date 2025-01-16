using System.Collections.Generic;
using UnityEngine;

namespace RECON.Utilites.Collision
{
    public class CollisionSystem<T> : ICollisionSystem<T>
    {
        private readonly Dictionary<Collider, T> _collidersDictionary = new Dictionary<Collider, T>();

        void ICollisionSystem<T>.Register(Collider collider, T component)
        {
            if (_collidersDictionary.ContainsKey(collider))
            {
                Debug.LogError($"CollisionSystem can't register {collider.name}. Key already exists.");
                return;
            }
            _collidersDictionary.Add(collider, component);
        }

        void ICollisionSystem<T>.Unregister(Collider collider)
        {
            if (!_collidersDictionary.Remove(collider))
            {
                Debug.LogError($"CollisionSystem can't unregister {collider.name}. Key doesn't exist.");
            }
        }

        bool ICollisionSystem<T>.TryGetValue(Collider collider, out T component)
        {
            return _collidersDictionary.TryGetValue(collider, out component);
        }
    }
}