using UnityEngine;
using UnityEngine.UI;

public class SoundScreen : MonoBehaviour
{
    [SerializeField] private GameObject _panelSoundsSettings;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    [SerializeField] private Button OnSoundsBtn;
    [SerializeField] private Button OffSoundBtn;

    [SerializeField] private Button _addVolumeBtn;
    [SerializeField] private Button _disVolumeBtn;

    [SerializeField] private GameObject[] _blocksValue;

    private int countActiveBlock;

    private void Awake()
    {
        _openButton.onClick.AddListener(() =>
        {
            _panelSoundsSettings.SetActive(true);

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
        });

        _closeButton.onClick.AddListener(() =>
        {
            _panelSoundsSettings.SetActive(false);

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
        });

        _addVolumeBtn.onClick.AddListener(() =>
        {
            countActiveBlock = MusicHandler.I.VolumeSounds(true);
            UpdateViewValue();

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
        });

        _disVolumeBtn.onClick.AddListener(() =>
        {
            countActiveBlock = MusicHandler.I.VolumeSounds(false);
            UpdateViewValue();

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
        });

        OnSoundsBtn.onClick.AddListener(() =>
        {
            MusicHandler.I.ActiveSound(true);

            for (int i = 0; i < _blocksValue.Length; i++)
            {
                _blocksValue[i].SetActive(true);
            }

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
        });

        OffSoundBtn.onClick.AddListener(() =>
        {
            MusicHandler.I.ActiveSound(false);

            HideBlocks();

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
        });
    }

    private void UpdateViewValue()
    {
//        Debug.Log(countActiveBlock);
        HideBlocks();

        for (int i = 0; i < countActiveBlock; i++)
        {
            _blocksValue[i].SetActive(true);
        }
    }

    private void HideBlocks()
    {
        for (int i = 0; i < _blocksValue.Length; i++)
        {
            _blocksValue[i].SetActive(false);
        }
    }
}
