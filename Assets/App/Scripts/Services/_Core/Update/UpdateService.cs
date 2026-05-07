using System.Collections.Generic;
using System.Linq;
using App.Scripts.DI.Utils;
using App.Scripts.Services._Base;
using App.Scripts.Utils.Exceptions;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services._Core.Update
{
    public class UpdateService : IGameService
    {
        private List<IUpdateService> _services;

        public UniTask PostInitAsync()
        {
            _services = ServiceLocator.GameServices
                .OfType<IUpdateService>()
                .ToList();

            var updateView  = GameObject.FindFirstObjectByType(typeof(UpdateServiceView)) as UpdateServiceView;
            
            if (!updateView)
                throw new InitServiceException(GetType());
            
            updateView.AssignServices(_services);
            
            return UniTask.CompletedTask;
        }

        public void Dispose() => _services.Clear();
    }
}