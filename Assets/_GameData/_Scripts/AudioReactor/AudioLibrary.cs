using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Game Data/New AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    public AudioGroup buttonPress;
    public AudioGroup stickmanDie;
    public AudioGroup success;
    public AudioGroup fail;
}