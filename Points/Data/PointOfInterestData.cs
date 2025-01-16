using RECON.Utilites.RandomSelector;
using UnityEngine;

namespace RECON.Utilites.Points
{
    [CreateAssetMenu(fileName = "PointOfInterestData", menuName = "RECON/Points/Location/PointOfInterestData")]
    public class PointOfInterestData : PointData, IPriorityModel<PointOfInterestData>
    {
        public PointOfInterestSettings Settings { get; private set; }

        PointOfInterestData IPriorityModel<PointOfInterestData>.Mode => this;

        float IPriorityModel<PointOfInterestData>.Priority => Settings.Priority;

        public void UpdateSettings(PointOfInterestSettings settings)
        {
            Settings = settings;
        }
    }
}
