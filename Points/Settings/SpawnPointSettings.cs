using UnityEngine;

namespace RECON.Utilites.Points
{
	[CreateAssetMenu(fileName = "SpawnPointSettings", menuName = "RECON/Points/Settings/SpawnPointSettings")]
	public class SpawnPointSettings : ScriptableObject
	{
		[SerializeField]
		private float _delay = 1f;
		[SerializeField]
		private int _maxSpawn = 100;

		public float Delay => _delay;
		public int MaxSpawn => _maxSpawn;

	}
}

