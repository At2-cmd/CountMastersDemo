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

    private int soundCount = -1;
    private float _lastPlaytime;
    private int playCount;

    public AudioClip Get_Clip()
    {
        soundCount = (soundCount + 1) % audio_clips.Length;
        return audio_clips[soundCount];
    }

    public float Get_Vol()
    {
        return Random.Range(vol_min, vol_max);
    }

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
