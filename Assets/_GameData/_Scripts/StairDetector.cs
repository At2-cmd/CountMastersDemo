using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairDetector : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StairLine stairLine))
        {
            transform.parent = null;
            EventManager.Instance.RaiseStairLineTouched();
        }
    }
}
