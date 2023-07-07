using UnityEngine;

[CreateAssetMenu(fileName = "New AudioGroup", menuName = "Game Data/New AudioGroup")]
public class AudioGroup : ScriptableObject
{
    public AudioClip[] audio_clips;

    public float pitch_min;
    public float pitch_max;
    public float vol_min;
    public float vol_max;
    public float cooldown;
    public int incrementalPitchSteps;
    public float incrementalPitchCountdown;

    public AudioClip Get_Clip()
    {
        if (soundCount == audio_clips.Length - 1)
        {
            soundCount = -1;
        }
        soundCount++;
        return audio_clips[soundCount];
    }

    public float Get_Vol()
    {
        return Random.Range(vol_min, vol_max);
    }

    private float _lastPlaytime;
    private int playCount;
    private int soundCount = -1;

    public float Get_Pitch()
    {
        if (incrementalPitchSteps > 1)
        {
            float deltaTime = Time.time - _lastPlaytime;

            if (deltaTime < incrementalPitchCountdown)
            {
                float deltaPitch = (pitch_max - pitch_min) / (float)incrementalPitchSteps;
                _lastPlaytime = Time.time;
                playCount = Mathf.Min(incrementalPitchSteps, playCount + 1);
                return pitch_min + deltaPitch * playCount;
            }
            else
            {
                playCount = 0;
                _lastPlaytime = Time.time;
                return pitch_min;
            }
        }

        return Random.Range(pitch_min, pitch_max);
    }
}
