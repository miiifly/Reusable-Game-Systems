using System;
using System.Linq;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace RECON.Utilites.Scenne
{
    public class SceneProvider : ISceneProvider
    {
        private readonly ScenesPreset _scenes;

        public SceneProvider(ScenesPreset scenes)
        {
            _scenes = scenes;
        }

        void ISceneProvider.LoadSceneByRef(SceneType sceneType, bool additive, Action<SceneAsset> callback) => LoadScene(GetSceneByType(sceneType), additive, callback);

        void ISceneProvider.UnloadSceneByRef(SceneAsset sceneRef, Action callback) => UnloadSceneByRef(sceneRef, callback);

        private void LoadScene(SceneAsset sceneRef, bool additive, Action<SceneAsset> callback)
        {
            if (additive)
            {
                var asyncOper = SceneManager.LoadSceneAsync(sceneRef.name, LoadSceneMode.Additive);
                asyncOper.completed += oper => { callback?.Invoke(sceneRef); };
            }
            else
            {
                var asyncOper = SceneManager.LoadSceneAsync(sceneRef.name, LoadSceneMode.Single);
                asyncOper.completed += oper => { callback?.Invoke(sceneRef); };
            }
        }

        private void UnloadSceneByRef(SceneAsset sceneRef, Action callback)
        {
            var asyncOper = SceneManager.UnloadSceneAsync(sceneRef.name);
            asyncOper.completed += oper => { callback?.Invoke(); };
        }

        private SceneAsset GetSceneByType(SceneType sceneType)
            => _scenes.AvailableScenes.FirstOrDefault(s => s.SceneType == sceneType).Scene;
    }
}