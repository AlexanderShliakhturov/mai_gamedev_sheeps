using Cysharp.Threading.Tasks;

namespace App.Scripts.Services.GameState.States.Base
{
    public interface IGameState
    {
        UniTask Enter();
        UniTask Exit();
        UniTask Update();
        UniTask LateUpdate();
    }
}