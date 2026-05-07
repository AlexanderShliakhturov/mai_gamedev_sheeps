using App.Scripts.DI.Utils.Signals;
using App.Scripts.Services.Player.Components;

namespace App.Scripts.Services.Player.Signals
{
    public class PlayerSpawnedSignal : Signal
    {
        public PlayerComponent PlayerComponent;
    }
}