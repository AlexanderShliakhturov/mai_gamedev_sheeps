using System;
using App.Scripts.Services._Base;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services.Input
{
    public class InputService : IGameService, IUpdateService
    {
        public enum InputSystemType
        {
            None,
            Player,
            UI
        }
        
        public InputSystem_Actions InputActions { get; private set; }

        public event Action<Vector2, float> MoveUpdated;

        public UniTask InitAsync()
        {
            InputActions = new InputSystem_Actions();
            return UniTask.CompletedTask;
        }

        public UniTask StartAsync()
        {
            InputActions.Player.Enable();
            return UniTask.CompletedTask;
        }

        public void Update(float deltaTime)
        {
            if (!InputActions.Player.enabled)
                return;
            
            MoveUpdated?.Invoke(InputActions.Player.Move.ReadValue<Vector2>(), deltaTime);
        }

        public void EnableSystem(InputSystemType systemType)
        {
            switch (systemType)
            {
                case InputSystemType.Player:
                    InputActions.Player.Enable();
                    break;
                case InputSystemType.UI:
                    InputActions.UI.Enable();
                    break;
                case InputSystemType.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(systemType), systemType, null);
            }
        }
        
        public void DisableSystem(InputSystemType systemType)
        {
            switch (systemType)
            {
                case InputSystemType.Player:
                    InputActions.Player.Disable();
                    break;
                case InputSystemType.UI:
                    InputActions.UI.Disable();
                    break;
                case InputSystemType.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(systemType), systemType, null);
            }
        }

        public void Dispose()
        {
            InputActions.Player.Disable();
            InputActions.UI.Disable();
            InputActions.Dispose();
        }
    }
}