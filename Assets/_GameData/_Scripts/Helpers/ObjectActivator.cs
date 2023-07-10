using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCrowdController player))
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                transform.parent.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
