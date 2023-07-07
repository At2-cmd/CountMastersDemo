using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanController : MonoBehaviour
{
    [SerializeField] private StickmanType type;
    public StickmanType Type => type;

    private PlayerAnimController _animController;

    private void OnEnable()
    {
        if (type != StickmanType.AllyStickman) return; // EnemyStickmans do not have to subscribe and listen these events.

        EventManager.Instance.OnRunStateEntered += OnRunStateEnteredHandler;
    }

    private void OnDisable()
    {
        if (type != StickmanType.AllyStickman) return; // EnemyStickmans should not unsubscribe these events, because they have never been subscribed.
        EventManager.Instance.OnRunStateEntered -= OnRunStateEnteredHandler;
    }

    private void Awake()
    {
        _animController = new PlayerAnimController(GetComponent<Animator>());
    }

    public void OnRunStateEnteredHandler()
    {
        _animController.PlayAnim(PlayerAnimController.Run);
    }
}
