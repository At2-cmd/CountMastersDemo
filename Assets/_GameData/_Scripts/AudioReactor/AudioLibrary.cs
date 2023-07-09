using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Game Data/New AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    public AudioGroup destroyStickmanSound;
    public AudioGroup winSound;
    public AudioGroup failSound;
    public AudioGroup highScoreSound;
    public AudioGroup generateCrowdSound;
}