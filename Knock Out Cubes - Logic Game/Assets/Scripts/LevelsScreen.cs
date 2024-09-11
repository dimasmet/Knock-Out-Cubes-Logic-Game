using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsScreen : MonoBehaviour
{
    [SerializeField] private Sprite[] _spritesPrevLevels;

    [SerializeField] private Button _backButton;
    [SerializeField] private Button _startButton;

    [SerializeField] private Text _nameLevelText;
    [SerializeField] private Image _imagePrevLevel;
    [SerializeField] private Color _colorClose;
    [SerializeField] private GameObject[] _stars;
    [SerializeField] private Button _leftBtn;
    [SerializeField] private Button _rightBtn;
    [SerializeField] private GameObject _iconClose;

    private List<LevelInfo> _listLevelInfo;
    private int _currentNumberLevel = 0;
    private RatingLevelView _ratingLevelView;

    private void Awake()
    {
        _startButton.onClick.AddListener(() =>
        {
            GameMain.main.LevelOpen(_listLevelInfo[_currentNumberLevel].numberLvl);

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
        });

        _backButton.onClick.AddListener(() =>
        {
            Screens.OnScreenOpen(ScreensName.Menu);

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
        });

        _leftBtn.onClick.AddListener(() =>
        {
            _currentNumberLevel--;

            ShowLevel();

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
        });

        _rightBtn.onClick.AddListener(() =>
        {
            _currentNumberLevel++;

            ShowLevel();

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
        });
    }

    public void SetDataLevels(List<LevelInfo> levelInfos)
    {
        _listLevelInfo = levelInfos;

        _currentNumberLevel = 0;

        _ratingLevelView = new RatingLevelView(_stars);

        ShowLevel();
    }

    private void ShowLevel()
    {
        _imagePrevLevel.sprite = _spritesPrevLevels[_currentNumberLevel];

        if (_currentNumberLevel > _listLevelInfo.Count - 2)
        {
            _rightBtn.gameObject.SetActive(false);
        }
        else
        {
            _rightBtn.gameObject.SetActive(true);
        }

        if (_currentNumberLevel <= 0)
        {
            _leftBtn.gameObject.SetActive(false);
        }
        else
        {
            _leftBtn.gameObject.SetActive(true);
        }

        _nameLevelText.text = "LEVEL - " + (_currentNumberLevel + 1);
        _ratingLevelView.UpdateRatingLevel(_listLevelInfo[_currentNumberLevel].countStar);

        if (_listLevelInfo[_currentNumberLevel].openStatus)
        {
            _startButton.interactable = true;
            _iconClose.SetActive(false);
            _imagePrevLevel.color = Color.white;
        }
        else
        {
            _startButton.interactable = false;
            _iconClose.SetActive(true);
            _imagePrevLevel.color = _colorClose;
        }
    }
}
