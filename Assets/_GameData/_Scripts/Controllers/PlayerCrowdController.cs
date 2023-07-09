using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCrowdController : CrowdBase
{
    public float cubeSize;
    [SerializeField] private StairDetector stairDetector;
    [SerializeField] private Transform stickmanParent;


    private void OnEnable()
    {
        EventManager.Instance.OnFinishPointReached += OnFinishPointReachedHandler;
        EventManager.Instance.OnFightWon += OnFightWonHandler;
    }


    private void Awake()
    {
        totalCrowdCount = GetComponentsInChildren<StickmanController>().Length;
        crowdUIController.UpdateTotalCrowdText(totalCrowdCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out StickmanController stickman))
            {
                stickmanList.Add(stickman);
            }
        }
    }

    public void GenerateCrowd(int amount , GateType gateType)
    {
        if (gateType == GateType.Enhancer)
        {
            for (int i = 0; i < amount; i++)
            {
                GenerateUnit();
            }
            crowdUIController.UpdateGainedCrowdText("+", amount);
        }

        else if (gateType == GateType.Multiplier)
        {
            int loopAmount = totalCrowdCount * (amount - 1);
            for (int i = 0; i < loopAmount; i++)
            {
                GenerateUnit();
            }
            crowdUIController.UpdateGainedCrowdText("x", amount);
        }
        crowdUIController.UpdateTotalCrowdText(totalCrowdCount);
        CreateFormation(stickmanList);
    }

    private void GenerateUnit()
    {
        totalCrowdCount++;
        GameObject obj = ObjectPooler.Instance.GetObjectFromPool(StickmanType.AllyStickman);
        StickmanController stickman = obj.GetComponent<StickmanController>();
        obj.transform.parent = stickmanParent.transform;
        obj.SetActive(true);
        obj.transform.position = transform.position;
        stickman.OnRunStateEnteredHandler();
        stickmanList.Add(stickman);
    }

    private void OnFinishPointReachedHandler()
    {
        CreateDynamicPyramidFormation(stickmanList, cubeSize , stairDetector);
        crowdUIController.DisableObject();
    }

    public void UpdateFormation()
    {
        CreateFormation(stickmanList);
    }

    private void OnFightWonHandler()
    {
        CreateFormation(stickmanList);
    }
}
