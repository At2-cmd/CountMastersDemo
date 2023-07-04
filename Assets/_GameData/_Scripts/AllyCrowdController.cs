using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCrowdController : CrowdBase
{
    private void Update()
    {
        //FOR TESTING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject obj = ObjectPooler.Instance.GetObjectFromPool(StickmanType.AllyStickman);
                obj.transform.parent = this.transform;
                obj.transform.localPosition = Vector3.zero;
                stickmanList.Add(obj.GetComponent<StickmanController>());
            }
            CreateFormation(stickmanList);
        }
    }
}
