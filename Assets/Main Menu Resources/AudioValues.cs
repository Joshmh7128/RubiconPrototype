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

    public void SetMaster(System.Single level)
    {
        MasterMixerVolume.SetFloat("Master", level);
        AudioLevels.masterVol = level;
    }

    public void SetMusic(System.Single level)
    {
        MasterMixerVolume.SetFloat("music", level);
        AudioLevels.masterVol = level;
    }

    public void SetSFX(System.Single level)
    {
        MasterMixerVolume.SetFloat("sfx", level);
        AudioLevels.masterVol = level;
    }

    public void SetMatt(System.Single level)
    {
        MasterMixerVolume.SetFloat("announcer", level);
        AudioLevels.masterVol = level;
    }

}
