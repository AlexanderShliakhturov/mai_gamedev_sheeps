using App.Scripts.Services._Core.Components._Base;
using UnityEngine;

namespace App.Scripts.Services._Core.Components.Attraction
{
    public class AttractionComponent : ActivatableObject
    {
        [field: SerializeField] public int Priority { get; private set; }

        public void IncreasePriority() => ++Priority;
    }
}