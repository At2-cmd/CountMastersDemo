using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowdController : CrowdBase
{
    [SerializeField] private int enemyCrowdAmount;

    private void Start()
    {
        StartCoroutine(GenerateEnemyGroupWithDelay());
    }

    private IEnumerator GenerateEnemyGroupWithDelay()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < enemyCrowdAmount; i++)
        {
            GameObject obj = ObjectPooler.Instance.GetObjectFromPool(StickmanType.EnemyStickman);
            obj.transform.parent = this.transform;
            obj.SetActive(true);
            obj.transform.localPosition = Vector3.zero;
            stickmanList.Add(obj.GetComponent<StickmanController>());
        }
        CreateFormation(stickmanList);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCrowdController player))
        {
            foreach (StickmanController stickman in stickmanList)
            {
                stickman.OnRunStateEnteredHandler();
            }
        }
    }
}
