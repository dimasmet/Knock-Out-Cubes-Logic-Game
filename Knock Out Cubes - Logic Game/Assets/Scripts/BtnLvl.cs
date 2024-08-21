using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnLvl : MonoBehaviour
{
    [SerializeField] private Button _thisLvlButton;
    [SerializeField] private Text _numberLevelText;

    [SerializeField] private GameObject[] _stars;
    private RatingLevelView _ratingLevelView;
    private LevelInfo _levelInfo;

    private bool isActiveButton = false;

    private void Awake()
    {
        _thisLvlButton.onClick.AddListener(() =>
        {
            if (isActiveButton)
            {
                GameMain.main.LevelOpen(_levelInfo.numberLvl);
            }
        });
    }

    public void SetDataButtonLvl(LevelInfo levelInfo)
    {
        _levelInfo = levelInfo;

        _numberLevelText.text = (_levelInfo.numberLvl + 1).ToString();

        _ratingLevelView = new RatingLevelView(_stars);

        UpdateInfo();
    }

    private void OnEnable()
    {
        if (_levelInfo != null)
            UpdateInfo();
    }

    private void UpdateInfo()
    {
        if (_levelInfo.openStatus)
        {
            isActiveButton = true;
            _ratingLevelView.UpdateRatingLevel(_levelInfo.countStar);
            _thisLvlButton.interactable = true;
        }
        else
        {
            _thisLvlButton.interactable = false;
        }
    }
}
