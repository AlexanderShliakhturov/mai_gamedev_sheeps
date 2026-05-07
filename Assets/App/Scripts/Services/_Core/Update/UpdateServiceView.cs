using System.Collections.Generic;
using App.Scripts.Services._Base;
using UnityEngine;

namespace App.Scripts.Services._Core.Update
{
    
    public class UpdateServiceView : MonoBehaviour
    {
        private List<IUpdateService> _services;

        public void AssignServices(List<IUpdateService> services) => _services = services;

        private void Awake() => DontDestroyOnLoad(this);

        private void Update()
        {
            foreach (var service in _services) 
                service.Update(Time.deltaTime);
        }
    }
}