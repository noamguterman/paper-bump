using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private AudioSource sfxSource;

    public AudioClip sfxButton;
    public AudioClip sfxReward;

    public AudioClip sfxPaperGrab;
    public AudioClip sfxObstacle;
    public AudioClip sfxGift;
    public AudioClip sfxAnimal;

    public AudioClip sfxVictory;
    public AudioClip sfxGameOver;

    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;

        sfxSource = GetComponent<AudioSource>();
    }

    public void PlayObstacleSFX()
    {
        sfxSource.PlayOneShot(sfxObstacle);
    }

    public void PlayPaperGrabSFX()
    {
        sfxSource.PlayOneShot(sfxPaperGrab);
    }

    public void PlayGiftSFX()
    {
        sfxSource.PlayOneShot(sfxGift);
    }

    public void PlayButtonSFX()
    {
        sfxSource.PlayOneShot(sfxButton);
    }

    public void PlayGameOverSFX()
    {
        sfxSource.PlayOneShot(sfxGameOver);
    }

    public void PlayVictorySFX()
    {
        sfxSource.PlayOneShot(sfxVictory);
    }

    public void PlayAnimalSFX()
    {
        sfxSource.PlayOneShot(sfxAnimal);
    }

    public void PlayRewardSFX()
    {
        sfxSource.PlayOneShot(sfxReward);
    }

    public void AddPitch()
    {
        sfxSource.pitch += 1.5f / 10;
    }

    public void ResetPitch()
    {
        sfxSource.pitch = 1f;
    }

    public void ActiveSounds(bool enabled)
    {
        sfxSource.volume = enabled ? 1 : 0;
    }
}