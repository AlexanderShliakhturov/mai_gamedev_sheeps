using App.Scripts.Services._Core.Components._Base;
using App.Scripts.Services.Entities.Components.Base;
using UnityEngine;

namespace App.Scripts.Services._Core.Components.Grab
{
    public class GrabbableComponent : ActivatableObject
    {
        public void OnGrabbed(Transform parent)
        {
            var entity = GetComponentInParent<Entity>();
            entity.ToggleMotion(false);
            entity.TogglePhysics(false);
            
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
        }

        public void OnReleased()
        {
            var entity = GetComponentInParent<Entity>();
            entity.ToggleMotion(true);
            entity.TogglePhysics(true);
            transform.SetParent(null);
        }
    }
}