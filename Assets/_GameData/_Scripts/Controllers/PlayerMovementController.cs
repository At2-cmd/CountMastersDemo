using System;
using System.Collections;
using UnityEngine;

public class PlayerMovementController : MovementBase
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float swerveSpeed;
    [SerializeField] private float maxSwerveAmount;
    [SerializeField] private float smoothFactor;
    [SerializeField] private float minXPosition;
    [SerializeField] private float maxXPosition;

    private bool isMousePressed;
    private float targetSwerveAmount;
    private float currentSwerveAmount;
    private bool canMove = false;

    private void OnEnable()
    {
        EventManager.Instance.OnFightStarted += OnFightStartedHandler;
        EventManager.Instance.OnFightWon += OnFightWonHandler;
        EventManager.Instance.OnGameFailed += OnGameFailedHandler;
    }

    private void OnFightStartedHandler(Vector3 targetDirection)
    {
        canMove = false;
        transform.forward = targetDirection - transform.position;
        moveToTargetRoutine = StartCoroutine(MoveToFightTarget(targetDirection, 3));
    }

    private void OnFightWonHandler()
    {
        canMove = true;
        StopCoroutine(moveToTargetRoutine);
    }

    private void OnGameFailedHandler()
    {
        StopCoroutine(moveToTargetRoutine);
    }

    private void Update()
    {
        if (!canMove) return;

        // Move the object forward
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

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
        transform.position = newPosition;
    }

    public void ActivateMovement()
    {
        canMove = true;
    }
}
