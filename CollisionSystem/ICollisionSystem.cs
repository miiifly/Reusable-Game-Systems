using UnityEngine;

namespace RECON.Utilites.Collision
{
    public interface ICollisionSystem<T>
    {
        void Register(Collider collider, T component);
        void Unregister(Collider collider);
        bool TryGetValue(Collider collider, out T component);
    }
}

