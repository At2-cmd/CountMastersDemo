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

}