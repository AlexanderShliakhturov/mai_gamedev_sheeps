using App.Scripts.Services._Core.Components._Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Services.Specializations
{
    [RequireComponent(typeof(Canvas))]
    public class SpecializationWindow : MonoBehaviour, IInitializable
    {
        public Button leaderButton;
        public Button healerButton;
        public Button shooterButton;

    }
}