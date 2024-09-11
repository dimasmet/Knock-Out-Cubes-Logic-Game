using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public static MusicHandler I;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundsSource;

    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _dropSound;
    [SerializeField] private AudioClip _coinSound;
    [SerializeField] private AudioClip _clickButtonSound;

    private int valueSound = 3;

    public enum NamSound
    {
        SoundWin,
        SoundLose,
        SoundDropBall,
        Coin,
        Tap
    }

    private void Awake()
    {
        if (I == null) I = this;
    }

    public void ActiveSound(bool isOn)
    {
        if (isOn) valueSound = 3;
        else valueSound = 0;

        SetValueVolume();
    }

    public int VolumeSounds(bool isAdd)
    {
        switch (isAdd)
        {
            case true:
                valueSound++;
                break;
            case false:
                valueSound--;
                break;
        }

        valueSound = Mathf.Clamp(valueSound, 0, 3);

        SetValueVolume();

        return valueSound;
    }

    private void SetValueVolume()
    {
        if (valueSound != 0)
        {
            float valueVolume = 1f * ((valueSound) / 3f);

            //Debug.Log("valueVolume = 1f / " + ((valueSound) / 3f) + " = " + valueVolume);

            _musicSource.volume = valueVolume;
            _soundsSource.volume = valueVolume;
        }
        else
        {
            _musicSource.volume = 0;
            _soundsSource.volume = 0;
        }
    }

    public void RunSound(NamSound namSound)
    {
        {
            switch (namSound)
            {
                case NamSound.SoundWin:
                    _soundsSource.PlayOneShot(_winSound);
                    break;
                case NamSound.SoundLose:
                    _soundsSource.PlayOneShot(_loseSound);
                    break;
                case NamSound.SoundDropBall:
                    _soundsSource.PlayOneShot(_dropSound);
                    break;
                case NamSound.Coin:
                    _soundsSource.PlayOneShot(_coinSound);
                    break;
                case NamSound.Tap:
                    _soundsSource.PlayOneShot(_clickButtonSound);
                    break;
            }
        }
    }
}
