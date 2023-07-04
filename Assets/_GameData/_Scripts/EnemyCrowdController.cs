using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowdController : CrowdBase
{
    private void Update()
    {
        //FOR TESTING
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject obj = ObjectPooler.Instance.GetObjectFromPool(StickmanType.EnemyStickman);
                obj.transform.parent = this.transform;
                obj.transform.localPosition = Vector3.zero;
                stickmanList.Add(obj.GetComponent<StickmanController>());
            }
            CreateFormation(stickmanList);
        }
    }
}
