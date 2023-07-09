using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public abstract class CrowdBase : MonoBehaviour
{
    [SerializeField] protected StickmanType stickmanType;
    [SerializeField] protected CrowdUIController crowdUIController;
    protected List<StickmanController> stickmanList = new List<StickmanController>();
    [Range(0, 1)] [SerializeField] protected float distanceFactor;
    [Range(0, 1)] [SerializeField] protected float radius;
    private Vector3 tempPos;
    protected int totalCrowdCount;

    protected virtual void CreateFormation(List<StickmanController> stickmanList)
    {
        for (int i = 0; i < stickmanList.Count; i++)
        {
            float x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            float z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);
            tempPos = new Vector3(x, 0, z);
            stickmanList[i].Transform.DOLocalMove(tempPos, .2f).SetEase(Ease.Flash);
        }
    }

    public void DestroyStickman(StickmanController stickman)
    {
        totalCrowdCount--;
        crowdUIController.UpdateTotalCrowdText(totalCrowdCount);
        stickman.Splat.transform.parent = null;
        stickman.Splat.SetActive(true);
        stickmanList.Remove(stickman);
        stickman.gameObject.SetActive(false);
        stickman.transform.parent = stickman.InitialParent;

        if (stickmanList.Count <= 0)
        {
            if (stickman.StickmanType == StickmanType.AllyStickman)
            {
                //ENEMY WINS
                crowdUIController.DisableObject();
                EventManager.Instance.RaiseGameFailed();
            }
            else
            {
                //PLAYER WINS
                crowdUIController.DisableObject();
                EventManager.Instance.RaiseFightWon();
            }
        }
    }

    public void DestroyStickmanAtFall(StickmanController stickman, Collider collider)
    {
        totalCrowdCount--;
        stickman.transform.parent = stickman.InitialParent;
        crowdUIController.UpdateTotalCrowdText(totalCrowdCount);
        stickmanList.Remove(stickman);

        DOVirtual.DelayedCall(1, () =>
        {
            collider.enabled = true;
            stickman.gameObject.SetActive(false);
        });

        if (stickmanList.Count <= 0)
        {
            crowdUIController.DisableObject();
            EventManager.Instance.RaiseGameFailed();
        }
    }

    protected int CalculateNumRows(int totalCount)
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

    protected int[] CalculateUnitsInRows(int numRows, int totalCount)
    {
        int[] unitsInRows = new int[numRows];
        int remainingUnits = totalCount;
        int currentRow = 0;

        while (remainingUnits > 0)
        {
            if (currentRow == numRows - 1) // Bottom row
            {
                unitsInRows[currentRow] = Math.Min(remainingUnits, 7); // Set the number of units to 7 or the remaining units, whichever is smaller
                remainingUnits -= unitsInRows[currentRow];
            }
            else if (remainingUnits >= numRows - currentRow)
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

    protected void CreateDynamicPyramidFormation(List<StickmanController> stickmanList ,float offset ,StairDetector stairDetector)
    {
        GameObject stairDetectorObject;
        int numRows = CalculateNumRows(stickmanList.Count - 1); // Calculate the number of rows in the pyramid (excluding the topmost cube)
        int[] unitsInRows = CalculateUnitsInRows(numRows, stickmanList.Count - 1); // Calculate the number of cubes in each row
        int currentUnitIndex = 0;

        float topX = 0.0f;
        float topY = (numRows - 1) * offset;
        float topZ = 0.0f;
        Vector3 topPosition = new Vector3(topX, topY, topZ);
        stairDetectorObject = Instantiate(stairDetector.gameObject, transform);
        stairDetectorObject.transform.localPosition = topPosition;
        stairDetectorObject.GetComponent<StairDetector>().IsTopDetector = true;
        StickmanController topMostUnit = stickmanList[currentUnitIndex];
        topMostUnit.transform.parent = stairDetectorObject.transform;
        topMostUnit.transform.DOLocalMove(Vector3.zero, .5f);
        currentUnitIndex++;

        for (int row = numRows - 1; row >= 0; row--)
        {
            int numUnitsInRow = unitsInRows[row];
            float rowOffset = (numUnitsInRow - 1) * offset * 0.5f;
            stairDetectorObject = Instantiate(stairDetector.gameObject, transform);
            stairDetectorObject.transform.localPosition = new Vector3(0, row * offset, 0);

            for (int col = 0; col < numUnitsInRow; col++)
            {
                float x = col * offset - rowOffset;
                float y = 0.0f;
                float z = 0.0f;

                Vector3 localPosition = new Vector3(x, y, z);
                StickmanController unit = stickmanList[currentUnitIndex];
                unit.transform.parent = stairDetectorObject.transform;
                unit.transform.DOLocalMove(localPosition, .5f);
                currentUnitIndex++;
            }
        }
    }
}
