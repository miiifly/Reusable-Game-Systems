using UnityEngine;

namespace RECON.Utilites.Points
{
    public class SpawnPointComponent : PointComponent<SpawnPointData>
    {
        [SerializeField]
        protected ScriptableObject _settings;

        protected override void UpdateScriptableValues()
        {
            base.UpdateScriptableValues();

            //if (_settings != null && _settings is SpawnPointSettings spawnSettings)
            //{
            //    _data.UpdateSettings(spawnSettings);
            //}
        }
    }
}