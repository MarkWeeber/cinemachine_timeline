using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private string _targetTag = "Player";
    [SerializeField] private bool _runOnce = true;
    [SerializeField] private UnityEvent _triggerEvent;

    private Collider _collider;
    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_targetTag))
        {
            _triggerEvent?.Invoke();
            if (_runOnce)
            {
                _collider.enabled = false;
            }
        }
    }

}
