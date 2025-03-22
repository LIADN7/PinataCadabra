using System.Collections;
using UnityEngine;

/// <summary>
/// Plays a random sound from an array every few seconds. 
/// </summary>
public class TrashTalkSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip startSoundTrash;
    [SerializeField] private AudioClip[] soundClips;
    [SerializeField] private float minDelay = 4f;
    [SerializeField] private float maxDelay = 8f;
    private AudioSource audioSource;

    private Coroutine soundLoopCoroutine;

    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();

    }


    // Plays the start sound then starts the random sound loop
    public void StartLoopWithStartSound()
    {
        audioSource.PlayOneShot(startSoundTrash);
        Invoke("StartLoop", startSoundTrash.length);
    }

    public void StartLoop()
    {
        if (soundLoopCoroutine == null)
        {

            soundLoopCoroutine = StartCoroutine(SoundLoop());
        }
    }




    public void StopLoop()
    {
        if (soundLoopCoroutine != null)
        {

            StopCoroutine(soundLoopCoroutine);
            soundLoopCoroutine = null;
        }
    }

    private IEnumerator SoundLoop()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
            if (soundClips.Length > 0)
            {
                int index = Random.Range(0, soundClips.Length);
                audioSource.PlayOneShot(soundClips[index]);
            }
        }
    }
}
