using UnityEngine;

public class InputRoot : CompositeRoot
{
    [SerializeField] private PlayerMovment _playerMovment;
    [SerializeField] private PlayerVisual _playerVisual;
    [SerializeField] private PlayerGun _playerGun;

    private IInput _input;
    public override void Compose()
    {
        _input = new DesktopInput();
        _playerMovment.Initialize(_input);
        _playerVisual.Initialize(_input);
        _playerGun.Initialize(_input);
    }
}
