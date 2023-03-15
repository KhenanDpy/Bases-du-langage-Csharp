using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{   
    
    public AudioMixer mixer;

    // Ajustement pour que le réglage du son corresponde à ce que l'on fait (sinon le son baisse trop vite)

    // Pour le master
    public void SetMasterLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
    }

    // Pour la music
    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    // Pour les sounds effects
    public void SetSoundEffectsLevel(float sliderValue)
    {
        mixer.SetFloat("SoundEffectsVol", Mathf.Log10(sliderValue) * 20);
    }

}
