using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour {
    [SerializeField] AudioMixer masterMixer;

    public void SetMusicVolume(float soundLevel) {
        masterMixer.SetFloat ("MusicVolume", soundLevel);
    }

    public void SetSFXVolume(float soundLevel) {
        masterMixer.SetFloat ("SFXVolume", soundLevel);
    }
    // min value -80, max value 0
}