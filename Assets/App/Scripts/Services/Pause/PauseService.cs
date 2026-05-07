using App.Scripts.DI.Utils;
using App.Scripts.Services._Base;
using App.Scripts.Services.Pause.Signals;

namespace App.Scripts.Services.Pause
{
    public class PauseService : IGameService
    {
        public void Pause() => SignalManager.Push(new PauseSignal { IsPaused = true });
        public void Resume() => SignalManager.Push(new PauseSignal { IsPaused = false });
    }
}