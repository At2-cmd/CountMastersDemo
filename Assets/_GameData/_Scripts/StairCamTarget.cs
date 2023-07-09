using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairCamTarget : MonoBehaviour
{
    private Tweener tween;
    private float tempYPos;
    private void OnEnable()
    {
        EventManager.Instance.OnStairLineTouched += OnStairLineTouchedHandler;
    }

    private void OnStairLineTouchedHandler(bool _)
    {
        tempYPos += 1f;
        tween.Kill();
        tween = transform.DOMoveY(tempYPos , .25f);
    }
}
