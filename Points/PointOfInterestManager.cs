using RECON.Utilites.RandomSelector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace RECON.Utilites.Points
{
    public class PointOfInterestManager : IPointOfInterestManager
    {
        [Inject]
        private IRandomSelector<PointOfInterestData, PointOfInterestData> _randomSelector;

        private List<PointOfInterestData> _spawnPoints;

        public PointOfInterestManager(PointOfInterestPreset preset)
        {
            _spawnPoints = preset.Points.ToList();
        }

        public Vector3 GetRandomPointOfInterest() => _randomSelector.SelectOption(_spawnPoints).Position;
    }
}
