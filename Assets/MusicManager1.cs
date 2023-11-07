using UnityEngine;

public class MusicManager1 : MonoBehaviour
{
    public AudioSource bgMusicSource; // Assign in inspector, regular background music
    public AudioSource bossMusicSource; // Assign in inspector, boss music

    public float crossFadeTime = 2.0f; // Time in seconds to crossfade music

    private bool isBossActive = false;

    private void Start()
    {
        // Play background music at the start
        bgMusicSource.Play();
        bossMusicSource.volume = 0; // Start with boss music volume at 0
    }

    private void Update()
    {
        // Detect if the boss has appeared
        // For example purposes, this assumes you trigger the fight somehow,
        // and set isBossActive to true when the boss appears.
        if (isBossActive)
        {
            StartBossMusic();
        }
    }

    public void StartBossMusic()
    {
        StartCoroutine(CrossfadeToBossMusic());
    }

    private System.Collections.IEnumerator CrossfadeToBossMusic()
    {
        float timeElapsed = 0;

        bossMusicSource.Play();

        while (timeElapsed < crossFadeTime)
        {
            bgMusicSource.volume = Mathf.Lerp(1, 0, timeElapsed / crossFadeTime);
            bossMusicSource.volume = Mathf.Lerp(0, 1, timeElapsed / crossFadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure we set the volumes to the final values
        bgMusicSource.volume = 0;
        bossMusicSource.volume = 1;
        bgMusicSource.Stop(); // Stop the background music AudioSource if not needed
    }

    // Call this method when the boss is defeated or when you want to switch back to regular music
    public void StopBossMusic()
    {
        StartCoroutine(CrossfadeToBgMusic());
    }

    private System.Collections.IEnumerator CrossfadeToBgMusic()
    {
        float timeElapsed = 0;

        bgMusicSource.Play();

        while (timeElapsed < crossFadeTime)
        {
            bgMusicSource.volume = Mathf.Lerp(0, 1, timeElapsed / crossFadeTime);
            bossMusicSource.volume = Mathf.Lerp(1, 0, timeElapsed / crossFadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        bossMusicSource.volume = 0;
        bgMusicSource.volume = 1;
        bossMusicSource.Stop(); // Stop the boss music AudioSource if not needed
    }
}
