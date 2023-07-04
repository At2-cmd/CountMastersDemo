using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanController : MonoBehaviour
{
    [SerializeField] private StickmanType type;
    public StickmanType Type => type;
}
