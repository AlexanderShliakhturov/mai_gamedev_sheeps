using System;
using App.Scripts.Services.Update;
using App.Scripts.Utils.EditorUtils;

namespace App.Scripts.Utils
{
    public sealed class GameTimer : IUpdatable
    {
        public bool IsPaused { get; private set; } = true;

        public event Action Started;
        public event Action Paused;
        public event Action Resumed;
        public event Action Stopped;
        public event Action Completed;
        private float _duration;
        private float _remainingTime;
        private bool _autoRestart;
        private float _startDelay;

        public GameTimer UseAutoRestart()
        {
            _autoRestart = true;
            return this;
        }

        public GameTimer UseDelay(float secondsDuration)
        {
            _startDelay = secondsDuration;
            return this;
        }

        public void Start(float secondsDuration)
        {
            if (_remainingTime > 0)
                Log.Warning("GameTimer is already started. You may not want this behaviour");

            IsPaused = false;

            _duration = secondsDuration;
            _remainingTime = _duration;

            Started?.Invoke();
        }

        public void Pause()
        {
            if (IsPaused)
                return;

            Paused?.Invoke();

            IsPaused = true;
        }

        public void Resume()
        {
            Resumed?.Invoke();
            IsPaused = false;
        }

        public void Restart() => Start(_duration);

        public void Stop()
        {
            Stopped?.Invoke();
            Complete();
        }

        public void Tick(float deltaTime)
        {
            if (IsPaused)
                return;

            _startDelay -= deltaTime;

            if (_startDelay > 0)
                return;

            _remainingTime -= deltaTime;

            if (_remainingTime <= 0f)
                Complete();
        }


        public void RemoveListeners()
        {
            Started = null;
            Paused = null;
            Resumed = null;
            Stopped = null;
            Completed = null;
        }

        private void Complete()
        {
            IsPaused = true;
            _remainingTime = 0;
            Completed?.Invoke();

            if (_autoRestart)
                Restart();
        }
    }
}