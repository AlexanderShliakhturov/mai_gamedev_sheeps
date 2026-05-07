using App.Scripts.DI.Utils;
using App.Scripts.Services._Base;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services._Core.Config
{
    public class ConfigService : IGameService
    {
        public UniTask InitAsync()
        {
            var updateView  = Object.FindFirstObjectByType(typeof(ConfigServiceView)) as ConfigServiceView;

            foreach (var config in updateView!.ServiceConfigs) 
                ServiceLocator.RegisterConfig(config);
            
            return UniTask.CompletedTask;
        }
    }
}