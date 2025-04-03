using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CamerasControl : MonoBehaviour
{
    [SerializeField] private LayerMask _interiorLook;
    [SerializeField] private LayerMask _exteriorLook;
    [SerializeField] private CinemachineVirtualCamera _interiorCamera;
    [SerializeField] private CinemachineVirtualCamera _exteriorCamera;
    [SerializeField] private float _switchToCinematicViewTime = 5f;
    [SerializeField] private CinemachineBlendListCamera _blendListCamera;
    [SerializeField] private bool _interiorView;

    private InputActions _inputActions;
    private IEnumerator _waitForCinematicRoutine;
    private Vector2 _mouseMovementDelta;
    private Player _player;

    private void Start()
    {
        _player = GameObject.Find("SpaceShip").GetComponent<Player>();
        _inputActions = _player.InputActions;
        _inputActions.Player.Switch_Camera.performed += SwitchView;
        _inputActions.Player.AnyKey.performed += SwitchFromCinematic;
        _inputActions.Menu.Exit.performed += ExitApplication;
        SwitchToControlledView();
        _waitForCinematicRoutine = CinematicSwitchRoutine(_switchToCinematicViewTime);
        StartCoroutine(_waitForCinematicRoutine);
    }

    private void Update()
    {
        _mouseMovementDelta = _inputActions.Player.MouseMovement.ReadValue<Vector2>();
        if (_mouseMovementDelta.x != 0f || _mouseMovementDelta.y != 0f)
        {
            SwitchToControlledView();
            StopCoroutine(_waitForCinematicRoutine);
            _waitForCinematicRoutine = CinematicSwitchRoutine(_switchToCinematicViewTime);
            StartCoroutine(_waitForCinematicRoutine);
        }
    }

    private void OnDestroy()
    {
        _inputActions.Player.Switch_Camera.performed -= SwitchView;
        _inputActions.Player.AnyKey.performed -= SwitchFromCinematic;
        _inputActions.Menu.Exit.performed -= ExitApplication;
    }

    private void SwitchToControlledView()
    {
        if (_interiorView)
        {
            SwitchToInteriorLook();
        }
        else
        {
            SwitchToExteriorLook();
        }
    }

    private void SwitchView(InputAction.CallbackContext context)
    {
        StopCoroutine(_waitForCinematicRoutine);
        if (_interiorView)
        {
            SwitchToExteriorLook();
            _interiorView = false;
        }
        else
        {
            SwitchToInteriorLook();
            _interiorView = true;
        }
        _waitForCinematicRoutine = CinematicSwitchRoutine(_switchToCinematicViewTime);
        StartCoroutine(_waitForCinematicRoutine);
    }

    private void SwitchFromCinematic(InputAction.CallbackContext context)
    {
        StopCoroutine(_waitForCinematicRoutine);
        SwitchToControlledView();
        _waitForCinematicRoutine = CinematicSwitchRoutine(_switchToCinematicViewTime);
        StartCoroutine(_waitForCinematicRoutine);
    }

    private void SwitchToExteriorLook()
    {
        _exteriorCamera.m_Priority = 20;
        _interiorCamera.m_Priority = 1;
        Camera.main.cullingMask = _exteriorLook;
    }

    private void SwitchToInteriorLook()
    {
        _interiorCamera.m_Priority = 20;
        _exteriorCamera.m_Priority = 1;
        Camera.main.cullingMask = _interiorLook;
    }

    private void SwitchToCinematicView()
    {
        _interiorCamera.m_Priority = 1;
        _exteriorCamera.m_Priority = 1;
        _blendListCamera.m_Priority = 15;
        Camera.main.cullingMask = _exteriorLook;
    }

    private void ExitApplication(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    IEnumerator CinematicSwitchRoutine(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        SwitchToCinematicView();
    }
}
