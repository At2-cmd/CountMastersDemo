using UnityEngine;

public class PlayerCrowdController : CrowdBase
{
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
        for (int i = 0; i < stickmanParent.childCount; i++)
        {
            if (stickmanParent.GetChild(i).TryGetComponent(out StickmanController stickman))
            {
                stickmanList.Add(stickman);
            }
        }
    }

    public void GenerateCrowd(int amount , GateType gateType)
    {
        switch (gateType) 
        {
            case GateType.Enhancer:
                {
                    for (int i = 0; i < amount; i++)
                    {
                        GenerateUnit();
                    }
                    crowdUIController.UpdateGainedCrowdText("+", amount);
                    break;
                }

            case GateType.Multiplier:
                {
                    int loopAmount = totalCrowdCount * (amount - 1);
                    for (int i = 0; i < loopAmount; i++)
                    {
                        GenerateUnit();
                    }
                    crowdUIController.UpdateGainedCrowdText("x", amount);
                    break;
                }
        }

        crowdUIController.UpdateTotalCrowdText(totalCrowdCount);
        CreateFormation(stickmanList);
    }

    private void GenerateUnit()
    {
        totalCrowdCount++;
        GameObject obj = ObjectPooler.Instance.GetObjectFromPool(StickmanType.AllyStickman);
        StickmanController stickman = obj.GetComponent<StickmanController>();
        stickman.Transform.parent = stickmanParent.transform;
        stickman.Transform.position = transform.position;
        stickman.gameObject.SetActive(true);
        stickman.OnRunStateEnteredHandler();
        stickmanList.Add(stickman);
    }

    private void OnFinishPointReachedHandler()
    {
        CreateDynamicPyramidFormation(stickmanList , stairDetector);
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
