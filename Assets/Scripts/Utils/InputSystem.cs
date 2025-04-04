public class InputSystem : SingletonInitializer<InputSystem>, IInitializer
{
    public InputActions InputActions { get => _inputActions; }
    private InputActions _inputActions;
    public void Initialize()
    {
        _inputActions = new InputActions();
        Enable();
    }

    public void Enable()
    {
        _inputActions.Enable();
    }

    public void Disable()
    {
        _inputActions.Disable();
    }
}

