using App.Scripts.Utils.EditorUtils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.DI
{
    public static class ProjectStartup
    {
        private static ServiceStartup _serviceStartup;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void OnLoad()
        {
            _serviceStartup?.Dispose();
            _serviceStartup = new ServiceStartup();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Init() => InitAsync().Forget();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void PostInit() => PostInitAsync().Forget();

        private static async UniTask PostInitAsync()
        {
            Log.Success("Project startup post init async started...");
            
            await _serviceStartup.PostInitAsync();
            
            Log.Success("Project startup post init async ended successfully...");
        }

        private static async UniTask InitAsync()
        {
            Log.Success("Project startup init async started...");
            
            await _serviceStartup.InitAsync();
            
            Log.Success("Project startup init async ended successfully...");
        }
    }
}