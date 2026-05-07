using System.Collections.Generic;
using App.Scripts.Services._Base;
using UnityEngine;

namespace App.Scripts.Services._Core.Config
{
    public class ConfigServiceView : MonoBehaviour
    {
        public IReadOnlyList<ServiceConfig> ServiceConfigs => _configs;
        [SerializeField] private List<ServiceConfig> _configs = new();
    }
}