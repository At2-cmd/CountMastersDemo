using UnityEngine;
using DG.Tweening;

public class StickmanController : MonoBehaviour
{
    [SerializeField] private StickmanType stickmanType;
    [SerializeField] private GameObject splat;

    private Transform _transform;
    private Collider _collider;
    private Transform initialParent;
    private CrowdBase crowdController;

    private PlayerAnimController _animController;
    public Transform Transform => _transform;
    public Transform InitialParent => initialParent;
    public StickmanType StickmanType => stickmanType;

    public GameObject Splat { get => splat; set => splat = value; }

    private void OnEnable()
    {
        crowdController = transform.GetComponentInParent<CrowdBase>(); // Change crowd controller each time it gets picked from the pool.

        EventManager.Instance.OnGameFailed += OnGameFailedHandler;
        EventManager.Instance.OnFightStarted += OnFightStartedHandler;
        //-----------------------//
        if (stickmanType != StickmanType.AllyStickman) return; // EnemyStickmans do not have to subscribe and listen these events.
        EventManager.Instance.OnRunStateEntered += OnRunStateEnteredHandler;
        EventManager.Instance.OnFinishPointReached += OnFinishPointReachedHandler;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnGameFailed -= OnGameFailedHandler;
        EventManager.Instance.OnFightStarted -= OnFightStartedHandler;
        //-----------------------//
        if (stickmanType != StickmanType.AllyStickman) return; // EnemyStickmans should not unsubscribe these events, because they have never been subscribed.
        EventManager.Instance.OnRunStateEntered -= OnRunStateEnteredHandler; 
        EventManager.Instance.OnFinishPointReached -= OnFinishPointReachedHandler;
    }


    private void Awake()
    {
        _animController = new PlayerAnimController(GetComponent<Animator>());
        _collider = GetComponent<Collider>();
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
        _collider.enabled = false;
    }

    private void OnFightStartedHandler(Vector3 _ , EnemyCrowdController crowd)
    {
        if (stickmanType != StickmanType.AllyStickman && crowdController != crowd) return; 
        //If the stickman is enemy and not in the group we will fight, return.

        _animController.PlayAnim(PlayerAnimController.FightWalk);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (StickmanType != StickmanType.AllyStickman) return;

        if (other.TryGetComponent(out StickmanController otherStickman))
        {
            if (otherStickman.StickmanType == StickmanType.EnemyStickman)
            {
                AudioReactor.Play(AudioReactor.lib.destroyStickmanSound);
                crowdController.DestroyStickman(this);
                otherStickman.crowdController.DestroyStickman(otherStickman);
            }
        }
    }

    public void EnableFall()
    {
        _collider.enabled = false;
        _transform.DOMoveY(transform.position.y - 5, 1f);
        _transform.parent = initialParent;
        crowdController.DestroyStickmanAtFall(this, _collider);
    }
}
