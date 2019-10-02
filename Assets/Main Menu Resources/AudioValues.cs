using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioValues : MonoBehaviour
{
    public int masterVolume;
    public int sfxVolume;
    public int announcerVolume;
    public int musicVolume;

    public AudioMixer MasterMixerVolume;

    private void Update()
    {
        MasterMixerVolume.SetFloat("Master", 100 / (float)masterVolume * (-0.8f));
        MasterMixerVolume.SetFloat("music", 100 / (float)musicVolume * (-0.8f));
        MasterMixerVolume.SetFloat("sfx", 100 / (float)sfxVolume * (-0.8f));
        MasterMixerVolume.SetFloat("announcer", 100 / (float)announcerVolume * (-0.8f));
    }

}
