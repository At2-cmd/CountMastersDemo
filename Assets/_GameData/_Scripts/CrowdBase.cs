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


    protected virtual void CreateFormation(List<StickmanController> stickmanList)
    {
        for (int i = 0; i < stickmanList.Count; i++)
        {
            float x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            float z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);
            tempPos = new Vector3(x, 0, z);
            stickmanList[i].transform.DOLocalMove(tempPos , .2f).SetEase(Ease.Flash); 
        }
    }


}
