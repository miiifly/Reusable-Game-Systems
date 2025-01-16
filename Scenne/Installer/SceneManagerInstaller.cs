using UnityEngine;
using Zenject;

namespace RECON.Utilites.Scenne
{
    public class SceneManagerInstaller : MonoInstaller
    {
        [SerializeField]
        private ScenesPreset _preset;
        public override void InstallBindings()
        {
            Container.BindInstance(_preset).AsTransient();
            Container.Bind<ISceneProvider>().To<SceneProvider>().AsSingle().WithArguments(_preset).NonLazy();
        }
    }
}
