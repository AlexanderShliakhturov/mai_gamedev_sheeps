using Cysharp.Threading.Tasks;

namespace App.Scripts.Services._Core.Components._Base
{
    public interface IInitializable
    {
        UniTask InitAsync()
        {
            return UniTask.CompletedTask;
        }
    }
}