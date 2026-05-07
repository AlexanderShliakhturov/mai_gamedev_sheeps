using App.Scripts.Services.Entities.Components.Base;
using App.Scripts.Services.Input;
using App.Scripts.Services.Input.Components;

namespace App.Scripts.Services.Player.Components
{
    public class PlayerComponent : Entity
    {
        public void AssignInput(InputService inputService)
        {
            foreach (var inputObtainable in GetComponents<IInputObtainable>()) 
                inputObtainable.AssignInput(inputService);
        }
    }
}