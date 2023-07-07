using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBase : MonoBehaviour
{

    protected Coroutine moveToTargetRoutine;

    protected IEnumerator MoveToFightTarget(Vector3 target, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;

        while (elapsedTime <= duration)
        {
            transform.position = Vector3.Lerp(startingPosition, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Ensure reaching the exact target position
        transform.position = target;
    }
}
