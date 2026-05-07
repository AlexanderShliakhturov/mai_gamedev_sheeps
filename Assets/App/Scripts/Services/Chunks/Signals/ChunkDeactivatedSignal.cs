using App.Scripts.DI.Utils.Signals;
using App.Scripts.Services.Chunks.Components;

namespace App.Scripts.Services.Chunks.Signals
{
    public class ChunkDeactivatedSignal : Signal
    {
        public ChunkComponent ChunkComponent;
    }
}