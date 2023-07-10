using System.Collections;
using UnityEngine;

public class EnemyCrowdController : CrowdBase
{
    [SerializeField] private int enemyCrowdAmount;

    private void Start()
    {
        StartCoroutine(GenerateEnemyGroupWithDelay());
        totalCrowdCount = enemyCrowdAmount;
        crowdUIController.UpdateTotalCrowdText(totalCrowdCount);
    }

    private IEnumerator GenerateEnemyGroupWithDelay()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < enemyCrowdAmount; i++)
        {
            GameObject obj = ObjectPooler.Instance.GetObjectFromPool(StickmanType.EnemyStickman);
            obj.transform.parent = this.transform;
            obj.SetActive(true);
            obj.transform.localPosition = Vector3.zero;
            stickmanList.Add(obj.GetComponent<StickmanController>());
        }
        CreateFormation(stickmanList);
    }
}
