using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeactivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCrowdController player))
        {
            player.UpdateFormation();

            for (int i = 0; i < transform.parent.childCount; i++)
            {
                transform.parent.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
