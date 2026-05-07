using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Services._Base;

namespace App.Scripts.DI.Utils
{
    public static class ServiceLocator
    {
        public static IReadOnlyList<IGameService> GameServices =>
            _services.Select(a => (IGameService)a.Value).ToList().AsReadOnly();

        public static IReadOnlyList<ServiceConfig> Configs =>
            _services.Select(a => (ServiceConfig)a.Value).ToList().AsReadOnly();

        private static Dictionary<Type, ServiceConfig> _configs = new();

        private static Dictionary<Type, object> _services = new();

        public static void RegisterService<T>(T service) => _services[typeof(T)] = service;

        public static T GetService<T>() => (T)_services[typeof(T)];

        public static List<T> GetServices<T>() => _services.Where(a => a.Value is T).Select(a => (T)a.Value).ToList();

        public static void Clear()
        {
            _services.Clear();
            _configs.Clear();
        }

        public static void RegisterConfig(ServiceConfig config) => _configs[config.GetType()] = config;

        public static T GetConfig<T>() where T : ServiceConfig => (T)_configs[typeof(T)];
    }
}