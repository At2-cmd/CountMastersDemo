using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public struct PoolDetail 
    {
        public StickmanType stickmanType;
        public GameObject objectToPool;
        public int poolAmount;
    }

    [SerializeField] private List<PoolDetail> pools;
    private List<GameObject> allyStickmanPool = new List<GameObject>();
    private List<GameObject> enemyStickmanPool = new List<GameObject>();
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < pools.Capacity; i++)
        {
            for (int j = 0; j < pools[i].poolAmount; j++)
            {
                GameObject spawnedObject = Instantiate(pools[i].objectToPool , Vector3.zero , pools[i].objectToPool.transform.rotation, this.transform);
                AddStickmanIntoList(spawnedObject , pools[i].stickmanType);
                spawnedObject.SetActive(false);
            }
        }
    }

    private void AddStickmanIntoList(GameObject objectToAdd ,StickmanType type)
    {
        switch (type) 
        {
            case StickmanType.AllyStickman:
                allyStickmanPool.Add(objectToAdd);
                break;
            case StickmanType.EnemyStickman:
                enemyStickmanPool.Add(objectToAdd);
                break;
        }
    }

    public GameObject GetObjectFromPool(StickmanType requiredType)
    {
        List<GameObject> listToCheck;
        GameObject objectToPick = null;
        if (requiredType == StickmanType.AllyStickman)
        {
            listToCheck = allyStickmanPool;
        }
        else
        {
            listToCheck = enemyStickmanPool;
        }

        for (int i = 0; i < listToCheck.Count; i++)
        {
            if (!listToCheck[i].activeSelf)
            {
                objectToPick = listToCheck[i];
                break;
            }
        }
        return objectToPick;
    }
}


public enum StickmanType
{
    EnemyStickman,
    AllyStickman
}
