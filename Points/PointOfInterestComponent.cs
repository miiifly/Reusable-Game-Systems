using UnityEngine;

namespace RECON.Utilites.Points
{
    public class PointOfInterestComponent : PointComponent<PointOfInterestData>
    {
        [SerializeField]
        protected ScriptableObject _settings;
        protected override void UpdateScriptableValues()
        {
            base.UpdateScriptableValues();

            if (_settings != null && _data != null && _settings is PointOfInterestSettings poiSettings)
            {
                _data.UpdateSettings(poiSettings);
            }
        }
    }
}
