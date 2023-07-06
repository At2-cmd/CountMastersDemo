using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrowdController : CrowdBase
{
    private int totalCrowdCount;

    private void Awake()
    {
        totalCrowdCount = GetComponentsInChildren<StickmanController>().Length;
    }

    public void GenerateCrowd(int amount , GateType gateType)
    {
        if (gateType == GateType.Enhancer)
        {
            for (int i = 0; i < amount; i++)
            {
                GenerateUnit();
            }
        }
        else if (gateType == GateType.Multiplier)
        {
            int loopAmount = totalCrowdCount * (amount - 1);
            for (int i = 0; i < loopAmount; i++)
            {
                GenerateUnit();
            }
        }
        CreateFormation(stickmanList);
    }

    private void GenerateUnit()
    {
        totalCrowdCount++;
        GameObject obj = ObjectPooler.Instance.GetObjectFromPool(StickmanType.AllyStickman);
        StickmanController stickman = obj.GetComponent<StickmanController>();
        obj.transform.parent = this.transform;
        obj.transform.position = transform.position;
        stickman.OnRunStateEnteredHandler();
        stickmanList.Add(stickman);
    }
}