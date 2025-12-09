using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour {

    [SerializeField] AudioMixer masterMixer;

    const float MIN_DB = -80f;

    public void SetMusicVolume(float value) {
        masterMixer.SetFloat("MusicVolume", LinearToDecibel(value));
    }

    public void SetSFXVolume(float value) {
        masterMixer.SetFloat("SFXVolume", LinearToDecibel(value));
    }

    float LinearToDecibel(float value) {
        if (value <= 0f) return MIN_DB;
        return Mathf.Log10(value) * 20f;
    }
}
