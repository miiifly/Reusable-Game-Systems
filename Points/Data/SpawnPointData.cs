using UnityEngine;

namespace RECON.Utilites.Points
{
    [CreateAssetMenu(fileName = "SpawnPointData", menuName = "RECON/Points/Location/SpawnPointData")]
    public class SpawnPointData : PointData
    {
        public SpawnPointSettings Settings { get; private set; }

        public void UpdateSettings(SpawnPointSettings settings)
        {
            Settings = settings;
        }
    }
}
