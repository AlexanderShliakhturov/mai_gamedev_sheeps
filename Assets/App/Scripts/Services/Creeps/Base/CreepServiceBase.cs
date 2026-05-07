using App.Scripts.Services._Base;
using App.Scripts.Services.Interest.Base;
using App.Scripts.Services.Interest.Components;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Services.Creeps.Base
{
    public abstract class CreepServiceBase : IGameService, IInterestPointService 
    {
        public virtual UniTask InitAsync() => UniTask.CompletedTask;
        public virtual UniTask PostInitAsync() => UniTask.CompletedTask;
        public virtual UniTask StartAsync() => UniTask.CompletedTask;

        public abstract UniTask CreateInterestAtPoint(InterestPointComponent component);
        public abstract UniTask ActivateInterestAtPoint(InterestPointComponent component);
        public abstract UniTask DeactivateInterestAtPoint(InterestPointComponent component);
        public abstract InterestType InterestType { get; }
    }
}