using App.Scripts.Services._Base;
using App.Scripts.Utils.EditorUtils;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Services.GameState
{
    public class GameStateService : IGameService
    {
        
        public UniTask InitAsync()
        {
            Log.Success(nameof(GameStateService) + " inited");
            return UniTask.CompletedTask;
        }

        public UniTask PostInitAsync()
        {
            Log.Success(nameof(GameStateService) + " post inited");
            return UniTask.CompletedTask;
        }
    }
}