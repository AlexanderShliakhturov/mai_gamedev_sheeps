using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services._Core.Components.Movement.Base;
using App.Scripts.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services._Core.Components.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class ChaoticMovementComponent : MovementComponent, IDependUpdatable, IResetable
    {
        [SerializeField] private float scanFrequency;
        private GameTimer _timer;

        public override UniTask InitAsync()
        {
            base.InitAsync();
            _timer = new GameTimer();
            _timer.Completed += Move;
            _timer.UseAutoRestart().UseDelay(scanFrequency).Start(scanFrequency);
            return UniTask.CompletedTask;
        }

        public void UpdateDependently(float deltaTime) => _timer.Tick(deltaTime);

        private void Move()
        {
            if (!Activated)
                return;
            
            var  horizontalDirection = new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y);

            Move(horizontalDirection, 1f);
        }

        public void Reset()
        {
            Rigidbody.linearVelocity = Vector3.zero;
            Rigidbody.angularVelocity = Vector3.zero;
            _timer.Stop();
        }
    }
}