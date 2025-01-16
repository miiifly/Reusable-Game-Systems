using System;
using UnityEditor;

namespace RECON.Utilites.Scenne
{
    public interface ISceneProvider
    {
        void LoadSceneByRef(SceneType sceneType, bool additive, Action<SceneAsset> callback);
        void UnloadSceneByRef(SceneAsset sceneRef, Action callback);
    }
}