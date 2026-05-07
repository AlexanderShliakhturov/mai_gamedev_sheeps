using App.Scripts.DI.Utils.Signals;
using App.Scripts.Services.Entities.Components.Base;

namespace App.Scripts.Services.Entities.Signals
{
    public class EntitySpawnedSignal : Signal
    {
        public Entity Entity;
    }
}