using App.Scripts.Services.Interest.Components;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Services.Interest.Base
{
    public interface IInterestPointService
    {
        UniTask CreateInterestAtPoint(InterestPointComponent component);
        UniTask ActivateInterestAtPoint(InterestPointComponent component);
        UniTask DeactivateInterestAtPoint(InterestPointComponent component);
        InterestType InterestType { get; }
    }

    public enum InterestType
    {
        None,
        Sheeps,
        Wolves,
        Grass
    }
}