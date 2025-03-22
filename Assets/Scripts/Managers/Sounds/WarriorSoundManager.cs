using UnityEngine;

/// <summary>
/// Manages the warrior's sound effects by playing a random spell sound and a random cast effect sound simultaneously.
/// </summary>
public class WarriorSoundManager : MonoBehaviour
{
    [Header("Spell Sounds")]
    [SerializeField] private AudioClip[] spellSounds;

    [Header("Cast Effect Sounds")]
    [SerializeField] private AudioClip[] castEffectSounds;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // if (audioSource == null)
        //     audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySpellAndCastEffect()
    {
        if (spellSounds.Length == 0 || castEffectSounds.Length == 0)
            return;

        int randomSpellIndex = Random.Range(0, spellSounds.Length);
        int randomCastIndex = Random.Range(0, castEffectSounds.Length);

        audioSource.PlayOneShot(spellSounds[randomSpellIndex]);
        audioSource.PlayOneShot(castEffectSounds[randomCastIndex], 0.2f);
    }
}
