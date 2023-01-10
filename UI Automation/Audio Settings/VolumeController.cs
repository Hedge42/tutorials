using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    // playerprefs keys
    const string MASTER_MUTE_KEY = "master mute";
    const string MASTER_VOL_KEY = "master volume";
    const string SFX_MUTE_KEY = "sfx mute";
    const string SFX_VOL_KEY = "sfx volume";
    const string MUSIC_MUTE_KEY = "music mute";
    const string MUSIC_VOL_KEY = "music volume";

    // mixer parameters
    const string MASTER_MIXER = "Master";
    const string MUSIC_MIXER = "Music";
    const string SFX_MIXER = "SFX";

    // mixer references
    public AudioMixerGroup masterMixer;
    public AudioMixerGroup sfxMixer;
    public AudioMixerGroup musicMixer;

    // editor controls
    [SerializeField, SyncedProperty(nameof(masterMute))]
    private bool _masterMute;
    [SerializeField, SyncedProperty(nameof(masterVolumeInternal))]
    private float _masterVolumeInternal;
    [SerializeField, SyncedProperty(nameof(sfxMute))]
    private bool _sfxMute;
    [SerializeField, SyncedProperty(nameof(sfxVolumeInternal))]
    private float _sfxVolumeInternal;
    [SerializeField, SyncedProperty(nameof(musicMute))]
    private bool _musicMute;
    [SerializeField, SyncedProperty(nameof(musicVolumeInternal))]
    private float _musicVolumeInternal;

    private void Start()
    {
        // can ONLY initialize mixers in start...
        UpdateMixers();
    }

    [ContextMenu(nameof(LogValues))]
    public void LogValues()
    {
        Debug.Log($"Volume Levels:\n" +
            $"master[{!masterMute}]{masterVolumeInternal}" +
            $"sfx[{!sfxMute}]{sfxVolumeInternal}" +
            $"music[{!musicMute}]{musicVolumeInternal}"
            );
    }

    // helpers
    private static bool ToBool(int value) => value > 0;
    private static int ToInt(bool value) => value ? 1 : 0;
    public static float LinearToDb(float value) => Mathf.Lerp(-80f, 0f, value);

    void UpdateMixers()
    {
        masterMixer.audioMixer.SetFloat(MASTER_MIXER, LinearToDb(masterVolume));
        sfxMixer.audioMixer.SetFloat(SFX_MIXER, LinearToDb(sfxVolume));
        musicMixer.audioMixer.SetFloat(MUSIC_MIXER, LinearToDb(musicVolume));
    }


    // properties
    public float masterVolume => masterMute ? 0f : masterVolumeInternal;

    /// <summary>Updates Mixers</summary>
    public bool masterMute
    {
        get => ToBool(PlayerPrefs.GetInt(MASTER_MUTE_KEY));
        set
        {
            PlayerPrefs.SetInt(MASTER_MUTE_KEY, ToInt(value));
            UpdateMixers();
        }
    }

    /// <summary>Updates Mixers</summary>
    public float masterVolumeInternal
    {
        get => PlayerPrefs.GetFloat(MASTER_VOL_KEY);
        set
        {
            PlayerPrefs.SetFloat(MASTER_VOL_KEY, Mathf.Clamp01(value));
            UpdateMixers();
        }
    }

    public float sfxVolume => sfxMute ? 0f : sfxVolumeInternal;


    /// <summary>Updates Mixers</summary>
    public bool sfxMute
    {
        get => ToBool(PlayerPrefs.GetInt(SFX_MUTE_KEY));
        set
        {
            PlayerPrefs.SetInt(SFX_MUTE_KEY, ToInt(value));
            UpdateMixers();
        }
    }


    /// <summary>Updates Mixers</summary>
    public float sfxVolumeInternal
    {
        get => PlayerPrefs.GetFloat(SFX_VOL_KEY);
        set
        {
            PlayerPrefs.SetFloat(SFX_VOL_KEY, Mathf.Clamp01(value));
            UpdateMixers();
        }
    }

    public float musicVolume => musicMute ? 0f : musicVolumeInternal;

    /// <summary>Updates Mixers</summary>
    public float musicVolumeInternal
    {
        get => PlayerPrefs.GetFloat(MUSIC_VOL_KEY);
        set
        {
            PlayerPrefs.SetFloat(MUSIC_VOL_KEY, Mathf.Clamp01(value));
            UpdateMixers();
        }
    }

    /// <summary>Updates Mixers</summary>
    public bool musicMute
    {
        get => ToBool(PlayerPrefs.GetInt(MUSIC_MUTE_KEY));
        set
        {
            PlayerPrefs.SetInt(MUSIC_MUTE_KEY, ToInt(value));
            UpdateMixers();
        }
    }
}
