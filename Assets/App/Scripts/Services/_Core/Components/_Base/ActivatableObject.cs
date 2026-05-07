using UnityEngine;

namespace App.Scripts.Services._Core.Components._Base
{
    public abstract class ActivatableObject : MonoBehaviour, IActivatable
    {
        public bool Activated { get; private set; }

        public void Activate()
        {
            Activated = true;
            ActivateInternal();
        }

        public void Deactivate()
        {
            Activated = false;
            DeactivateInternal();
        }

        protected virtual void DeactivateInternal()
        {
        }

        protected virtual void ActivateInternal()
        {
        }
    }
}