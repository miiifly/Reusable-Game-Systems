using ModestTree;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RECON.Utilites.Points
{
    public abstract class PointsPreset<T> : ScriptableObject where T : PointData
    {
        [SerializeField]
        private List<T> _points = new List<T>();

        [SerializeField]
        private PointConfig _config;

        public PointConfig Config => _config;
        public IEnumerable<T> Points => _points;

        public void TryAdd(T data)
        {
            if (_points.Contains(data)) return;

            _points.Add(data);
        }

        public void Remove(T data)
        {
            _points.RemoveWithConfirm(data);
        }
    }

    [Serializable]
    public class PointConfig
    {
        [SerializeField] private string _path = "Assets/_Presets";
        [SerializeField] private string _defaultName = "NewPointPreset";
        [SerializeField] private string _folderName = "Points";

        public string Path => _path;
        public string FolderName => _folderName;
        public string DefaultName => _defaultName;
    }
}
