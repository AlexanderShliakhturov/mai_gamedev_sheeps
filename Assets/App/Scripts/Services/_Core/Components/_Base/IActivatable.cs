namespace App.Scripts.Services._Core.Components._Base
{
    public interface IActivatable
    {
        void Activate();
        void Deactivate();
        bool Activated { get; }
    }
}