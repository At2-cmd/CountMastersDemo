using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class CrowdBase : MonoBehaviour
{
    [SerializeField] protected StickmanType stickmanType;
    [SerializeField] protected CrowdUIController crowdUIController;
    protected List<StickmanController> stickmanList = new List<StickmanController>();
    [Range(0, 1)] [SerializeField] protected float distanceFactor;
    [Range(0, 1)] [SerializeField] protected float radius;
    private Vector3 tempPos;
    private float offset = .75f;
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
        stickman.Transform.parent = stickman.InitialParent;

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

    private int CalculateNumRows(int totalUnits)
    {
        int numRows = 0;
        int currentUnits = 0;

        while (currentUnits < totalUnits)
        {
            numRows++;
            currentUnits += numRows;
        }

        return numRows;
    }

    private int[] CalculateUnitsInRows(int numRows, int totalUnits)
    {
        int[] cubesInRows = new int[numRows];
        int remainingCubes = totalUnits;
        int currentRow = 0;

        while (remainingCubes > 0)
        {
            if (remainingCubes >= numRows - currentRow)
            {
                cubesInRows[currentRow] = numRows - currentRow;
                remainingCubes -= numRows - currentRow;
            }
            else
            {
                cubesInRows[currentRow] = remainingCubes;
                remainingCubes = 0;
            }

            currentRow++;
        }

        return cubesInRows;
    }

    protected void CreateDynamicPyramidFormation(List<StickmanController> stickmanList ,StairDetector stairDetector)
    {
        GameObject stairDetectorObject;
        int numRows = CalculateNumRows(stickmanList.Count); // Calculate the number of rows in the pyramid (excluding the topmost cube)
        int[] unitsInRows = CalculateUnitsInRows(numRows, stickmanList.Count); // Calculate the number of cubes in each row
        int currentUnitIndex = 0;

        for (int row = 0; row < numRows; row++)
        {
            stairDetectorObject = Instantiate(stairDetector.gameObject, transform);
            stairDetectorObject.transform.localPosition = new Vector3(0, row * 1.5f, 0);
            stairDetectorObject.GetComponent<StairDetector>().IsTopDetector = true;
            int numUnitsInRow = unitsInRows[row];

            float rowOffset = (numUnitsInRow - 1) * offset * 0.5f;

            for (int col = 0; col < numUnitsInRow; col++)
            {
                float x = (col * offset) - rowOffset;
                float y = 0.0f;
                float z = 0.0f;

                Vector3 position = new Vector3(x, y, z);
                StickmanController unit = stickmanList[currentUnitIndex];
                unit.transform.parent = stairDetectorObject.transform;
                unit.transform.DOLocalMove(position, .5f);
                currentUnitIndex++;
            }

            if (numUnitsInRow <= 1)
            {
                break;
            }

            stairDetectorObject.GetComponent<StairDetector>().IsTopDetector = false;
        }
    }
}


