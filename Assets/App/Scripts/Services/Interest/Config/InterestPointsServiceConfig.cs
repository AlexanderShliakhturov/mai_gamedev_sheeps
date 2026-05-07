using App.Scripts.Services._Base;
using UnityEngine;

namespace App.Scripts.Services.Interest.Config
{
    [CreateAssetMenu(fileName = nameof(InterestPointsServiceConfig),
        menuName = "Configs/" + nameof(InterestPointsServiceConfig))]
    public class InterestPointsServiceConfig : ServiceConfig
    {
    }
}