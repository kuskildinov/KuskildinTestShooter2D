using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _root;
    [SerializeField] private float _speed;

    private IInput _input;
    private float _horizontal;
    private float _vertical;

    public void Initialize(IInput input)
    {
        _input = input;
    }

    private void Update()
    {
        ReadInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var moveDirection = _root.TransformDirection(new Vector3(_horizontal, _vertical, 0f));
        _rigidbody.MovePosition(_rigidbody.transform.position + moveDirection * _speed * Time.fixedDeltaTime);
    }

    private void ReadInput()
    {
        _horizontal = _input.HorizontalMove();
        _vertical = _input.VerticalMove();
    }
}
