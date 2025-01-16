using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RECON.Utilites.Points
{
    public class SpawnPointProvider<T> : ISpawnPointProvider<T> where T : PointData
    {
        private List<T> _spawnPoints;

        List<T> ISpawnPointProvider<T>.SpawnPoints => _spawnPoints;

        private PointsPreset<T> _preset;

        public SpawnPointProvider(PointsPreset<T> preset)
        {
            _spawnPoints = preset.Points.ToList();
        }

        Vector3 ISpawnPointProvider<T>.GetRandomSpawnPosition()
        {
            if (_preset.Points.Count() == 0)
            {
                Debug.LogError("No available spawn points.");
                return Vector3.zero;
            }

            int randomIndex = Random.Range(0, _spawnPoints.Count);
            var selectedPoint = _spawnPoints[randomIndex];
            return selectedPoint.Position;
        }
    }
}