using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnLvl : MonoBehaviour
{
    [SerializeField] private Button _thisLvlButton;
    [SerializeField] private Text _numberLevelText;

    [SerializeField] private RatingLevelView _ratingLevelView;
    private LevelInfo _levelInfo;

    public void SetDataButtonLvl(LevelInfo levelInfo)
    {
        _levelInfo = levelInfo;
    }

    private void UpdateInfo()
    {

    }
}
