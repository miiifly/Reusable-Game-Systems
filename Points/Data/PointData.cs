using UnityEngine;

namespace RECON.Utilites.Points
{
	public class PointData : ScriptableObject
	{
		public Vector3 Position;
		public Quaternion Rotation;

		public void UpdatePositionAndRotation(Vector3 position, Quaternion rotation)
		{
			Position = position;
			Rotation = rotation;
		}
	}
}
