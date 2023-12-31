using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StickmanController stickman))
        {
            AudioReactor.Play(AudioReactor.lib.fallSound);
            stickman.EnableFall();
        }
    }
}
