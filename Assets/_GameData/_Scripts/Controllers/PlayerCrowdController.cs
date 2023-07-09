using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrowdController : CrowdBase
{
    public float cubeSize;


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
        obj.transform.parent = this.transform;
        obj.SetActive(true);
        obj.transform.position = transform.position;
        stickman.OnRunStateEnteredHandler();
        stickmanList.Add(stickman);
    }

    private void OnFinishPointReachedHandler()
    {
        CreateDynamicPyramidFormation(stickmanList);
        crowdUIController.DisableObject();
    }

    private void OnFightWonHandler()
    {
        CreateFormation(stickmanList);
    }

    private void CreateDynamicPyramidFormation(List<StickmanController> stickmanList)
    {
        int numRows = CalculateNumRows(stickmanList.Count - 1); // Calculate the number of rows in the pyramid (excluding the topmost cube)
        int[] unitsInRows = CalculateUnitsInRows(numRows, stickmanList.Count - 1); // Calculate the number of cubes in each row
        int currentUnitIndex = 0;
        print("numrows : " + numRows);

        float topX = 0.0f;
        float topY = (numRows - 1) * cubeSize;
        float topZ = 0.0f;
        Vector3 topPosition = new Vector3(topX, topY, topZ);
        print("top Pos : " + topPosition);
        StickmanController topMostUnit = stickmanList[currentUnitIndex];
        topMostUnit.transform.localPosition = topPosition;
        currentUnitIndex++;

        for (int row = numRows - 1; row >= 0; row--)
        {
            int numUnitsInRow = unitsInRows[row];
            float rowOffset = (numUnitsInRow - 1) * cubeSize * 0.5f;

            for (int col = 0; col < numUnitsInRow; col++)
            {
                float x = col * cubeSize - rowOffset;
                float y = row * cubeSize;
                float z = 0.0f;

                Vector3 localPosition = new Vector3(x, y, z);
                StickmanController unit = stickmanList[currentUnitIndex];
                unit.transform.localPosition = localPosition;
                currentUnitIndex++;
            }
        }
    }

    private int CalculateNumRows(int totalCount)
    {
        int numRows = 0;
        int currentUnits = 0;

        while (currentUnits < totalCount)
        {
            numRows++;
            currentUnits += numRows;
        }

        return numRows;
    }

    private int[] CalculateUnitsInRows(int numRows, int totalCount)
    {
        int[] unitsInRows = new int[numRows];
        int remainingUnits = totalCount;
        int currentRow = 0;

        while (remainingUnits > 0)
        {
            if (remainingUnits >= numRows - currentRow)
            {
                unitsInRows[currentRow] = numRows - currentRow;
                remainingUnits -= numRows - currentRow;
            }
            else
            {
                unitsInRows[currentRow] = remainingUnits;
                remainingUnits = 0;
            }

            currentRow++;
        }

        return unitsInRows;
    }
}
