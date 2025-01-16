using System.Collections.Generic;
using UnityEngine;

namespace RECON.Utilites.Spawner
{
    public class SpawnPreset<T> : ScriptableObject where T : IBaseSpawnable
    {
        [SerializeField]
        private List<T> _components;
        public IEnumerable<T> Components => _components;
    }
}