using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanController : MonoBehaviour
{
    [SerializeField] private StickmanType stickmanType;

    [SerializeField] private GameObject splat;
    private Transform _transform;
    private Transform initialParent;

    private CrowdBase crowdController;

    private PlayerAnimController _animController;
    public StickmanType StickmanType => stickmanType;
    public Transform Transform => _transform;
    public Transform InitialParent => initialParent;

    public GameObject Splat { get => splat; set => splat = value; }

    private void OnEnable()
    {
        crowdController = transform.GetComponentInParent<CrowdBase>(); // Change crowd controller each time it gets picked from the pool.

        EventManager.Instance.OnGameFailed += OnGameFailedHandler;
        //-----------------------//
        if (stickmanType != StickmanType.AllyStickman) return; // EnemyStickmans do not have to subscribe and listen these events.
        EventManager.Instance.OnRunStateEntered += OnRunStateEnteredHandler;
        EventManager.Instance.OnFinishPointReached += OnFinishPointReachedHandler;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnGameFailed -= OnGameFailedHandler;
        //-----------------------//
        if (stickmanType != StickmanType.AllyStickman) return; // EnemyStickmans should not unsubscribe these events, because they have never been subscribed.
        EventManager.Instance.OnRunStateEntered -= OnRunStateEnteredHandler; 
        EventManager.Instance.OnFinishPointReached -= OnFinishPointReachedHandler;
    }


    private void Awake()
    {
        _animController = new PlayerAnimController(GetComponent<Animator>());
        _transform = this.transform;
        initialParent = transform.parent;
    }

    public void OnRunStateEnteredHandler()
    {
        _animController.PlayAnim(PlayerAnimController.Run);
    }

    private void OnGameFailedHandler()
    {
        _animController.PlayAnim(PlayerAnimController.Victory);
    }


    private void OnFinishPointReachedHandler()
    {
        _animController.PlayAnim(PlayerAnimController.Idle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (StickmanType != StickmanType.AllyStickman) return;

        if (other.TryGetComponent(out StickmanController otherStickman))
        {
            if (otherStickman.StickmanType == StickmanType.EnemyStickman)
            {
                crowdController.DestroyStickman(this);
                otherStickman.crowdController.DestroyStickman(otherStickman);
            }
        }
    }
}
