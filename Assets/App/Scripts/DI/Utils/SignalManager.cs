using System;
using System.Collections.Generic;
using App.Scripts.DI.Utils.Signals;
using App.Scripts.Utils.EditorUtils;
using Cysharp.Threading.Tasks;

namespace App.Scripts.DI.Utils
{
    public static class SignalManager
    {
        private static Dictionary<Type, List<Func<Signal, UniTask>>> _subscribers = new();

        public static void Subscribe<T>(Func<Signal, UniTask> signalAction) where T : Signal
        {
            if (!_subscribers.ContainsKey(typeof(T)))
                _subscribers[typeof(T)] = new List<Func<Signal, UniTask>>();

            _subscribers[typeof(T)].Add(signalAction);
        }

        public static void Push<T>(T signal) where T : Signal
        {
            if (!_subscribers.ContainsKey(typeof(T)))
            {
                Log.Warning("No subscriber registered for signal " + signal.GetType().Name);
                return;
            }

            foreach (var func in _subscribers[typeof(T)])
                func?.Invoke(signal).Forget();
        }

        public static void Clear() => _subscribers.Clear();
    }
}