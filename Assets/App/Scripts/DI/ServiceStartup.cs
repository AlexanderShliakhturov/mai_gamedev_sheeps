using System;
using App.Scripts.DI.Utils;
using App.Scripts.Services._Core.Config;
using App.Scripts.Services._Core.Update;
using App.Scripts.Services.Camera;
using App.Scripts.Services.Chunks;
using App.Scripts.Services.Creeps.Sheeps;
using App.Scripts.Services.Creeps.Wolves;
using App.Scripts.Services.Entities;
using App.Scripts.Services.GameState;
using App.Scripts.Services.Grass;
using App.Scripts.Services.Input;
using App.Scripts.Services.Interest;
using App.Scripts.Services.Pause;
using App.Scripts.Services.Player;
using App.Scripts.Services.Specializations;
using App.Scripts.Services.Update;
using App.Scripts.Utils.EditorUtils;
using App.Scripts.Utils.Exceptions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.DI
{
    public class ServiceStartup : IDisposable
    {
        private static void RegisterServices()
        {
            ServiceLocator.RegisterService(new ConfigService());
            ServiceLocator.RegisterService(new UpdateService());
            ServiceLocator.RegisterService(new GameStateService());
            ServiceLocator.RegisterService(new SimpleUpdateService());
            ServiceLocator.RegisterService(new PauseService());
            ServiceLocator.RegisterService(new EntityService());
            ServiceLocator.RegisterService(new InputService());
            ServiceLocator.RegisterService(new CameraService());
            ServiceLocator.RegisterService(new PlayerService());
            ServiceLocator.RegisterService(new ChunkService());
            ServiceLocator.RegisterService(new InterestPointsService());
            ServiceLocator.RegisterService(new SheepsService());
            ServiceLocator.RegisterService(new GrassInterestService());
            ServiceLocator.RegisterService(new WolvesService());
            ServiceLocator.RegisterService(new SpecializationService());
        }

        public async UniTask InitAsync()
        {
            Application.targetFrameRate = 60;
            
            RegisterServices();
            
            Log.Info("Services registered");
            
            await InitServicesAsync();
            
            Log.Info("Services inited");
        }

        public async UniTask PostInitAsync()
        {
            await PostInitServicesAsync();
            
            Log.Info("Services post inited");
            
            SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single)!.completed += OnMainSceneLoadCompleted;
        }

        public void Dispose()
        {
            foreach (var service in ServiceLocator.GameServices) 
                service.Dispose();
            
            ServiceLocator.Clear();
            SignalManager.Clear();
        }
        
        private void OnMainSceneLoadCompleted(AsyncOperation obj) => StartServicesAsync().Forget();

        private async UniTask StartServicesAsync()
        {
            foreach (var service in ServiceLocator.GameServices) 
                await service.StartAsync();
        }

        private async UniTask InitServicesAsync()
        {
            try
            {
                foreach (var gameService in ServiceLocator.GameServices)
                    await gameService.InitAsync();
            }
            catch (InitServiceException e)
            {
                Log.Error(e.Message);
            }
        }

        private async UniTask PostInitServicesAsync()
        {
            try
            {
                foreach (var gameService in ServiceLocator.GameServices)
                    await gameService.PostInitAsync();
            }
            catch (InitServiceException e)
            {
                Log.Error(e.Message);
            }
        }
    }
}