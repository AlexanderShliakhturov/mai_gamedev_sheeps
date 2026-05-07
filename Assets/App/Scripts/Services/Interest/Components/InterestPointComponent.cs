using App.Scripts.Services.Interest.Base;
using UnityEngine;

namespace App.Scripts.Services.Interest.Components
{
    public class InterestPointComponent : MonoBehaviour
    {
        public InterestType InterestType { get; private set; }
        public InterestComponent InterestComponent { get; private set; }

        public void AssignInterest(InterestComponent component, InterestType type)
        {
            InterestComponent = component;
            
            InterestComponent.transform.SetParent(transform);
            InterestComponent.transform.localPosition = Vector3.zero;
            
            InterestType = type;
        }
    }
}