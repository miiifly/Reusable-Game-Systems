using UnityEditor;
using UnityEngine;

namespace RECON.Utilites.Points
{
    public class PointComponent<T> : MonoBehaviour, IPointComponent where T : PointData
    {
        [SerializeField]
        protected string _name;
        [SerializeField]
        protected T _data;
        [SerializeField]
        protected PointsPreset<T> _preset;

        protected string _currentName;

        bool IPointComponent.IsAssigne => _data == null;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!Application.isPlaying)
            {
                UpdateScriptableValues();
            }
        }
#endif

        void IPointComponent.AssignScriptableIfEmpty()
        {
            if (_data == null)
            {
                _data = ScriptableObject.CreateInstance<T>();

                _data.name = !string.IsNullOrEmpty(_name) ? _name : _preset.Config.DefaultName;
                _currentName = _data.name;
                string folderPath = _preset.Config.Path + "/" + _preset.Config.FolderName;

                if (!AssetDatabase.IsValidFolder(folderPath))
                {
                    AssetDatabase.CreateFolder(_preset.Config.Path, _preset.Config.FolderName);
                }

                string assetPath = $"{folderPath}/{_data.name}.asset";
                AssetDatabase.CreateAsset(_data, assetPath);
                AssetDatabase.SaveAssets();

                if (_preset != null)
                {
                    _preset.TryAdd(_data);
                    EditorUtility.SetDirty(_preset);
                    AssetDatabase.SaveAssets();
                }

                UpdateScriptableValues();
            }
        }

        protected virtual void UpdateScriptableValues()
        {
            if (_data == null)
            {
                return;
            }

            if (_data.name != _currentName)
            {
                _name = _data.name;
            }
            else
            {
                _data.name = _name;
            }

            string assetPath = AssetDatabase.GetAssetPath(_data);
            if (string.IsNullOrEmpty(assetPath))
            {
                Debug.LogError("Asset path is null or empty!");
                return;
            }

            AssetDatabase.RenameAsset(assetPath, _name);
            AssetDatabase.SaveAssets();

            _currentName = _data.name;

            if (transform == null)
            {
                Debug.LogError("Transform is null!");
                return;
            }

            _data.UpdatePositionAndRotation(transform.position, transform.rotation);

            if (_preset != null)
            {
                _preset.TryAdd(_data);
                EditorUtility.SetDirty(_preset);
                AssetDatabase.SaveAssets();
            }

            EditorUtility.SetDirty(_data);
            AssetDatabase.SaveAssets();
        }


        void IPointComponent.UpdatePoint()
        {
            UpdateScriptableValues();
        }

        void IPointComponent.DeletePoint()
        {
            if (_data == null)
            {
                Debug.LogWarning("Data object is null and cannot be deleted.");
                return;
            }

            string assetPath = AssetDatabase.GetAssetPath(_data);

            if (_preset != null)
            {
                _preset.Remove(_data);

                EditorUtility.SetDirty(_preset);

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            DestroyImmediate(_data, true);
            _data = null;

            if (!string.IsNullOrEmpty(assetPath))
            {
                AssetDatabase.DeleteAsset(assetPath);
                AssetDatabase.Refresh();
            }
        }

    }

    [CustomEditor(typeof(PointComponent<>), true)]
    public class PointComponentEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var myScript = target as IPointComponent;

            if (myScript.IsAssigne && GUILayout.Button("Assign Scriptable"))
            {
                myScript.AssignScriptableIfEmpty();
            }

            if (GUILayout.Button("Update Data"))
            {
                myScript.UpdatePoint();
            }

            if (GUILayout.Button("Delete"))
            {
                myScript.DeletePoint();
            }
        }
    }
}
