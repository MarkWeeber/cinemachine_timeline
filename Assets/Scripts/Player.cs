using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 100f;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    [SerializeField] private float _fasterSpeedRatio = 2f;
    [SerializeField] private float _slowerSpeedRatio = 0.5f;
    [SerializeField] private float _steerSpeed = 50f;
    [SerializeField] private bool _invertX = false;
    [SerializeField] private bool _invertY = false;

    private InputActions _inputActions;
    private float _speedFactor;
    private float _rollInput;
    private Vector2 _steerInput;
    private Vector3 _rotation;

    private void Start()
    {
        _inputActions = InputSystem.Instance.InputActions;
        _inputActions.Player.Accelerate.performed += Accelerated;
        _inputActions.Player.Deccelarate.performed += Deccelarated;
        _inputActions.Player.Accelerate.canceled += Released;
        _inputActions.Player.Deccelarate.canceled += Released;
        _speedFactor = 1f;
    }

    private void Update()
    {
        Move();
        Steer();
    }

    private void Move()
    {
        transform.Translate(transform.forward * _moveSpeed * _speedFactor * Time.deltaTime, Space.World);
    }

    private void Steer()
    {
        _steerInput = _inputActions.Player.Steering.ReadValue<Vector2>();
        _rollInput = _inputActions.Player.Rolling.ReadValue<float>();
        _rotation = new Vector3(
            _steerInput.y * ((_invertY) ? 1f : -1f),
            _steerInput.x * ((_invertX) ? 1f : -1f),
            _rollInput * -1f);
        transform.Rotate(_rotation * _steerSpeed * Time.deltaTime, Space.Self);
    }

    private void Accelerated(InputAction.CallbackContext context)
    {
        _speedFactor = _fasterSpeedRatio;
    }

    private void Deccelarated(InputAction.CallbackContext context)
    {
        _speedFactor = _slowerSpeedRatio;
    }

    private void Released(InputAction.CallbackContext context)
    {
        _speedFactor = 1f;
    }

    public void EnableInputSystem()
    {
        _inputActions.Enable();
    }

    public void DisableInputSystem()
    {
        _inputActions.Disable();
    }
}
