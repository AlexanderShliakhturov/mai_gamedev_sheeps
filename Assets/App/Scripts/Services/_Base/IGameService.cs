using System;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Services._Base
{
    public interface IGameService : IDisposable
    {
        UniTask InitAsync() => UniTask.CompletedTask;
        UniTask PostInitAsync() => UniTask.CompletedTask;

        void IDisposable.Dispose()
        {
        }

        UniTask StartAsync() => UniTask.CompletedTask;
    }
}