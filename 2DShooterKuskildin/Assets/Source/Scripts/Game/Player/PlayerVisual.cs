using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;   
    [SerializeField] private Animator _playerAnimator;

    private IInput _input;
    private float _horizontal;
    private float _vertical;
    private Vector3 _mousePosition;

    public void Initialize(IInput input)
    {
        _input = input;
    }

    private void Update()
    {
        ReadInput();
        CheckLookDirection();
        if(Mathf.Abs(_horizontal) > 0.1 || Mathf.Abs(_vertical) > 0.1)
        {
            PlayWalkAnimation();
        }
    }

    private void CheckLookDirection()
    {       
        if(_mousePosition.x > transform.position.x)
            _playerSpriteRenderer.flipX = true; 
        else if(_mousePosition.x < transform.position.x)
            _playerSpriteRenderer.flipX = false;
    }

    private void PlayWalkAnimation()
    {
        _playerAnimator.SetBool("Walk",true);
    }

    private void ReadInput()
    {
        _horizontal = _input.HorizontalMove();
        _vertical = _input.VerticalMove();
        _mousePosition = _input.MousePosition();
    }
}
