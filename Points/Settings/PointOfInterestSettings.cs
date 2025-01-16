using UnityEngine;

namespace RECON.Utilites.Points
{
	[CreateAssetMenu(fileName = "PointOfInterestSettings", menuName = "RECON/Points/Settings/PointOfInterestSettings")]
	public class PointOfInterestSettings : ScriptableObject
	{
		[SerializeField]
		private float _priority;

		public float Priority => _priority;
	}
}
