using UnityEngine;

public class PlayerMovementController : MovementBase
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float swerveSpeed;
    [SerializeField] private float maxSwerveAmount;
    [SerializeField] private float smoothFactor;
    [SerializeField] private float minXPosition;
    [SerializeField] private float maxXPosition;

    private Transform _transform;
    private bool isMousePressed;
    private float targetSwerveAmount;
    private float currentSwerveAmount;
    private bool canMove = false;
    private bool canUserControl = true;

    private void OnEnable()
    {
        EventManager.Instance.OnFightStarted += OnFightStartedHandler;
        EventManager.Instance.OnFightWon += OnFightWonHandler;
        EventManager.Instance.OnGameFailed += OnGameFailedHandler;
        EventManager.Instance.OnFinishPointReached += OnFinishPointReachedHandler;
        EventManager.Instance.OnGameSuccessed += OnGameSuccessedHandler;
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void OnFightStartedHandler(Vector3 targetDirection , EnemyCrowdController _)
    {
        canMove = false;
        moveToTargetRoutine = StartCoroutine(MoveToFightTarget(targetDirection, 3));
        SmoothLookAtTarget(targetDirection);
    }

    private void OnFightWonHandler()
    {
        canMove = true;
        StopCoroutine(moveToTargetRoutine);
        SmoothLookAtForward();
    }

    private void OnGameFailedHandler()
    {
        canMove = false;
        StopCoroutine(moveToTargetRoutine);
    }

    private void OnFinishPointReachedHandler()
    {
        canUserControl = false;
        forwardSpeed *= 2;
    }

    private void OnGameSuccessedHandler()
    {
        canMove = false; 
        canUserControl = false;
    }

    private void Update()
    {
        if (!canMove) return;

        // Move the object forward
        _transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        if (!canUserControl) return;

        // Check if the left mouse button is pressed
        isMousePressed = InputManager.Instance.IsMousePressed();

        if (isMousePressed)
        {
            // Get the horizontal input from the Input Manager
            float horizontalInput = InputManager.Instance.GetHorizontalInput();

            // Calculate the target swerve amount based on the input
            targetSwerveAmount = horizontalInput * maxSwerveAmount;

            // Smoothly interpolate the current swerve amount towards the target swerve amount
            currentSwerveAmount = Mathf.Lerp(currentSwerveAmount, targetSwerveAmount, smoothFactor * Time.deltaTime);
        }
        else
        {
            // Reset the swerve amount if the left mouse button is not pressed
            targetSwerveAmount = 0f;
            currentSwerveAmount = 0f;
        }

        // Apply the swerve movement to the object's position
        Vector3 newPosition = transform.position + new Vector3(currentSwerveAmount * swerveSpeed * Time.deltaTime, 0f, 0f);
        newPosition.x = Mathf.Clamp(newPosition.x, minXPosition, maxXPosition);
        _transform.position = newPosition;
    }

    public void ActivateMovement()
    {
        canMove = true;
    }
}
