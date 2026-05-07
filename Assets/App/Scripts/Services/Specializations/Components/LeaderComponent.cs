using App.Scripts.Services._Core.Components.Attraction;
using App.Scripts.Services.Specializations.Components.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Services.Specializations.Components
{
    [RequireComponent(typeof(AttractionComponent), typeof(AttractableComponent))]
    public class LeaderComponent : SpecializationComponent
    {
        private AttractionComponent _attraction;
        private AttractableComponent _attractable;

        [ContextMenu("Increase")]
        public void IncreaseTest() => IncreaseLevel();

        public override UniTask InitAsync()
        {
            _attraction = GetComponent<AttractionComponent>();
            _attractable = GetComponent<AttractableComponent>();
            return UniTask.CompletedTask;
        }

        public override void IncreaseLevelInternal()
        {
            _attraction.IncreasePriority();
            _attractable.IncreaseMinPriority();
        }
    }
}