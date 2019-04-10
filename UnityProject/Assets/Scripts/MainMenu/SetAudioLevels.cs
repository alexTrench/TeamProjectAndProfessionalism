using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioLevels : MonoBehaviour {

	public AudioMixer mainMixer;                    //Used to hold a reference to the AudioMixer mainMixer


    //Call this function and pass in the float parameter masterLevel to set the volume of the AudioMixerGroup Master Volume in mainMixer
    public void SetMasterLevel(float masterLevel)
    {
        mainMixer.SetFloat("masterVol", masterLevel);
    }

    //Call this function and pass in the float parameter musicLvl to set the volume of the AudioMixerGroup Music in mainMixer
    public void SetMusicLevel(float musicLevel)
	{
		mainMixer.SetFloat("musicVol", musicLevel);
	}

	//Call this function and pass in the float parameter sfxLevel to set the volume of the AudioMixerGroup SoundFx in mainMixer
	public void SetSfxLevel(float sfxLevel)
	{
		mainMixer.SetFloat("sfxVol", sfxLevel);
	}
}
