using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairDetector : MonoBehaviour
{
    private Collider _collider;
    private bool isTopDetector;

    public bool IsTopDetector { get => isTopDetector; set => isTopDetector = value; }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StairLine stairLine))
        {
            if (isTopDetector)
            {
                EventManager.Instance.RaiseGameSuccessed();
                AudioReactor.Play(AudioReactor.lib.winSound);
                return;
            }
            _collider.enabled = false;
            transform.parent = null;
            EventManager.Instance.RaiseStairLineTouched(isTopDetector);
        }
    }
}
