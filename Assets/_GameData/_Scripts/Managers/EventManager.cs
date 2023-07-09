using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
	public static EventManager Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}
		
	public event Action OnGameLoaded;
	public void LoadGame() => OnGameLoaded?.Invoke();

	public event Action OnGameStarted;
	public void RaiseGameStarted() => OnGameStarted?.Invoke();

	public event Action OnGameSuccessed;
	public void RaiseGameSuccessed() => OnGameSuccessed?.Invoke();

	public event Action OnGameFailed;
	public void RaiseGameFailed() => OnGameFailed?.Invoke();

	public event Action OnRunStateEntered;
    public void RaiseRunStateEntered() => OnRunStateEntered?.Invoke();

	public event Action<Vector3> OnFightStarted;
	public void RaiseFightStarted(Vector3 targetDirection) => OnFightStarted?.Invoke(targetDirection);
	
	public event Action OnFightWon;
	public void RaiseFightWon() => OnFightWon?.Invoke();

	public event Action OnFinishPointReached;
	public void RaiseFinishPointReached() => OnFinishPointReached?.Invoke();

	public event Action<bool> OnStairLineTouched;
	public void RaiseStairLineTouched(bool isTopDetector) => OnStairLineTouched?.Invoke(isTopDetector);

}