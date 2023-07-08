using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class CrowdBase : MonoBehaviour
{
    [SerializeField] protected StickmanType stickmanType;
    protected List<StickmanController> stickmanList = new List<StickmanController>();
    [Range(0,1)] [SerializeField] protected float distanceFactor;
    [Range(0,1)] [SerializeField] protected float radius;
    private Vector3 tempPos;
    protected int totalCrowdCount;

    protected virtual void CreateFormation(List<StickmanController> stickmanList)
    {
        for (int i = 0; i < stickmanList.Count; i++)
        {
            float x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            float z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);
            tempPos = new Vector3(x, 0, z);
            stickmanList[i].Transform.DOLocalMove(tempPos , .2f).SetEase(Ease.Flash);
        }
    }


    public void DestroyStickman(StickmanController stickman)
    {
        totalCrowdCount--;
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
                print("ENEMY WINS");
                EventManager.Instance.RaiseGameFailed();
            }
            else
            {
                //PLAYER WINS
                print("PLAYER WINS");
                CreateFormation(stickmanList);
                EventManager.Instance.RaiseFightWon();
            }
        }
    }
}
