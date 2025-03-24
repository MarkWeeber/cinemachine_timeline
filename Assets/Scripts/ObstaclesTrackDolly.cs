using UnityEngine;
using Cinemachine;

public class ObstaclesTrackDolly : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook _freeLookCamera;
    [SerializeField] private CinemachineVirtualCamera _trackedDollyCamera;
    [SerializeField] private string _playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _freeLookCamera.m_Priority = 0;
            _trackedDollyCamera.m_Priority = 10;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _freeLookCamera.m_Priority = 10;
            _trackedDollyCamera.m_Priority = 0;
        }
    }
}
