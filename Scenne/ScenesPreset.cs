using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RECON.Utilites.Scenne
{
	[CreateAssetMenu(fileName = "ScenesPreset", menuName = "RECON/Preset/ScenesPreset")]
	public class ScenesPreset : ScriptableObject
	{
		[SerializeField] private List<SceneInfo> _availableScenes;
		public List<SceneInfo> AvailableScenes => _availableScenes;
	}

	[Serializable]
	public class SceneInfo
	{
		public SceneType SceneType;
		public SceneAsset Scene;
	}

	public enum SceneType
	{
		None = 0,
		Enviro = 1,
		Terrain = 2,
		UI = 3
	}
}