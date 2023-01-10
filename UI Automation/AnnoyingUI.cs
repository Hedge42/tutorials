using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnnoyingUI : MonoBehaviour
{
    public VolumeController controller;

    public Slider masterVolumeSlider;
    public TextMeshProUGUI masterVolumeLabel;
    public Toggle masterMute;

    public Slider musicVolumeSlider;
    public TextMeshProUGUI musicVolumeLabel;
    public Toggle musicMute;

    public Slider sfxVolumeSlider;
    public TextMeshProUGUI sfxVolumeLabel;
    public Toggle sfxMute;

    private void Awake()
    {
        masterVolumeSlider.onValueChanged.AddListener(MasterVolumeChanged);
        masterMute.onValueChanged.AddListener(b => controller.masterMute = !b);

        sfxVolumeSlider.onValueChanged.AddListener(SFXVolumeChanged);
        sfxMute.onValueChanged.AddListener(b => controller.sfxMute = !b);

        musicVolumeSlider.onValueChanged.AddListener(MusicVolumeChanged);
        musicMute.onValueChanged.AddListener(b => controller.musicMute = !b);
    }

    private void OnEnable()
    {
        masterVolumeSlider.value = controller.masterVolumeInternal;
        sfxVolumeSlider.value = controller.sfxVolumeInternal;
        musicVolumeSlider.value = controller.musicVolumeInternal;

        masterMute.isOn = !controller.masterMute;
        musicMute.isOn = !controller.musicMute;
        sfxMute.isOn = !controller.sfxMute;
    }

    void MasterVolumeChanged(float v)
    {
        controller.masterVolumeInternal = v;

        v = controller.masterVolumeInternal;
        masterVolumeSlider.SetValueWithoutNotify(v);
        masterVolumeLabel.SetText((v * 100f).ToString("f0") + "%") ;
    }
    void SFXVolumeChanged(float v)
    {
        controller.sfxVolumeInternal = v;

        v = controller.sfxVolumeInternal;
        sfxVolumeSlider.SetValueWithoutNotify(v);
        sfxVolumeLabel.SetText((v * 100f).ToString("f0") + "%");
    }
    void MusicVolumeChanged(float v)
    {
        controller.musicVolumeInternal = v;

        v = controller.musicVolumeInternal;
        musicVolumeSlider.SetValueWithoutNotify(v);
        musicVolumeLabel.SetText((v * 100f).ToString("f0") + "%");
    }
}
