using System.Collections.Generic;
using UnityEngine;

namespace RECON.Utilites.Points
{
    public interface ISpawnPointProvider<T> where T : PointData
    {
        List<T> SpawnPoints { get; }
        Vector3 GetRandomSpawnPosition();
    }
}

