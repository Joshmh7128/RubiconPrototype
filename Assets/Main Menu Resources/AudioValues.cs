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
        level -= 80;
        MasterMixerVolume.SetFloat("Master", level);
        AudioLevels.masterVol = level;
    }

    public void SetMusic(System.Single level)
    {
        level -= 80;
        MasterMixerVolume.SetFloat("music", level);
        AudioLevels.musicVol = level;
    }

    public void SetSFX(System.Single level)
    {
        level -= 80;
        MasterMixerVolume.SetFloat("sfx", level);
        AudioLevels.sfxVol = level;
    }

    public void SetMatt(System.Single level)
    {
        level -= 80;
        MasterMixerVolume.SetFloat("announcer", level);
        AudioLevels.announcerVol = level;
    }

}
