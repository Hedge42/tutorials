using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName ="Mixer Controller")]
public class MixerController : ScriptableObject
{
    public AudioMixerGroup mixer;

    [Tooltip("Key for PlayerPrefs (float)")]
    public string volumeKey;
    [Tooltip("Key for PlayerPrefs (int)")]
    public string muteKey;
    [Tooltip("Name of the mixer's exposed parameter")]
    public string mixerKey;

    [SerializeField, SyncedProperty(nameof(mute))]
    private bool _mute;
    [SerializeField, SyncedProperty(nameof(volumeInternal))]
    private float _volumeInternal;

    // properties

    private static bool ToBool(int value) => value > 0;
    private static int ToInt(bool value) => value ? 1 : 0;
    public static float LinearToDb(float value) => Mathf.Lerp(-80f, 0f, value);

    public void UpdateMixer()
    {
        mixer.audioMixer.SetFloat(mixerKey, LinearToDb(volume));
    }

    public float volume => mute ? 0f : volumeInternal;
    /// <summary>Updates Mixers</summary>
    public bool mute
    {
        get => ToBool(PlayerPrefs.GetInt(muteKey));
        set
        {
            PlayerPrefs.SetInt(muteKey, ToInt(value));
            UpdateMixer();
        }
    }

    /// <summary>Updates Mixers</summary>
    public float volumeInternal
    {
        get => PlayerPrefs.GetFloat(volumeKey);
        set
        {
            PlayerPrefs.SetFloat(volumeKey, Mathf.Clamp01(value));
            UpdateMixer();
        }
    }
}
