using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

/// <summary>
/// This class allows to attach some Unity events whenever the current camera goes live or goes standby
/// </summary>
public class CinemachineOnLive : MonoBehaviour
{
    [SerializeField] private UnityEvent _onLiveEvent;
    [SerializeField] private UnityEvent _onStandbyEvent;
    private CinemachineBrain _brain;
    private CinemachineVirtualCamera _camera;

    private bool _wasActive;

    private void Start()
    {
        _brain = Camera.main.GetComponent<CinemachineBrain>();
        _brain.m_CameraActivatedEvent.AddListener(CameraActivatedEvent);
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void CameraActivatedEvent(ICinemachineCamera arg0, ICinemachineCamera arg1)
    {
        if (CinemachineCore.Instance.IsLive(_camera)) // camera went live
        {
            _onLiveEvent?.Invoke();
            _wasActive = true;
        }
        else if (_wasActive) // camera went standby
        {
            _onStandbyEvent?.Invoke();
            _wasActive = false;
        }
    }
}
