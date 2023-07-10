using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PositionAlternator : MonoBehaviour
{
    public float moveDuration = 2f;    // Duration of each movement cycle
    public float moveDistance = 5f;    // Distance to move the object
    public float startingDelay;    

    private Vector3 startPosition;     // Starting position of the object
    private Vector3 targetPosition;    // Target position for each movement cycle

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        startPosition = transform.position;
        targetPosition = startPosition - new Vector3(moveDistance, 0f, 0f);

        // Start the coroutine to move the object
        StartCoroutine(MoveObject());
    }

    private IEnumerator MoveObject()
    {
        yield return new WaitForSeconds(startingDelay);
        while (true)
        {
            // Move the object towards the target position
            float elapsedTime = 0f;
            while (elapsedTime < moveDuration)
            {
                float t = elapsedTime / moveDuration;
                _transform.position = Vector3.Lerp(startPosition, targetPosition, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Swap the start and target positions
            Vector3 temp = startPosition;
            startPosition = targetPosition;
            targetPosition = temp;
        }
    }
}
