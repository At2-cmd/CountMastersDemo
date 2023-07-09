using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CrowdUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text totalCrowdAmountText;
    [SerializeField] private TMP_Text gainedCrowdText;

    public void UpdateTotalCrowdText(int totalCrowd)
    {
        totalCrowdAmountText.text = totalCrowd.ToString();
    }

    public void UpdateGainedCrowdText(string prefix , int amount)
    {
        gainedCrowdText.text = prefix + amount.ToString();
        gainedCrowdText.gameObject.SetActive(true);
        DOVirtual.DelayedCall(.35f, () =>
       {
           gainedCrowdText.gameObject.SetActive(false);
       });
    }

    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
