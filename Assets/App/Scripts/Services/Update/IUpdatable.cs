namespace App.Scripts.Services.Update
{
    public interface IUpdatable
    {
        void Tick(float deltaTime);
        bool IsPaused { get; }
    }
}