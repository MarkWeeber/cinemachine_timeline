using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 100f;
    [SerializeField] private float _steerSpeed = 50f;
    [SerializeField] private bool _invertX = false;
    [SerializeField] private bool _invertY = false;

    private InputActions _inputActions;
    public InputActions InputActions  { get => _inputActions;}

    private Vector2 _steerInput;
    private Vector3 _rotation;

    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Enable();
    }

    private void OnDestroy()
    {
        _inputActions.Dispose();
    }

    private void Update()
    {
        Move();
        Steer();
    }

    private void Move()
    {
        transform.Translate(transform.forward * _moveSpeed * Time.deltaTime, Space.World);
    }

    private void Steer()
    {
        _steerInput = _inputActions.Player.Steering.ReadValue<Vector2>();
        _rotation = new Vector3(_steerInput.y * ((_invertY) ? 1f : -1f), 0f, _steerInput.x * ((_invertX) ? 1f : -1f));
        transform.Rotate(_rotation * _steerSpeed * Time.deltaTime, Space.Self);
    }
}
