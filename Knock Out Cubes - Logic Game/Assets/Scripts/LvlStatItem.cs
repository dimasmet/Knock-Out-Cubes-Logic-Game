using UnityEngine;
using UnityEngine.UI;

public class LvlStatItem : MonoBehaviour
{
    [SerializeField] private Text _numberLevelText;

    [SerializeField] private GameObject[] _stars;
    [SerializeField] private Image _imageLvl;
    [SerializeField] private Sprite _grayImage;
    [SerializeField] private Sprite _originImage;
    private RatingLevelView _ratingLevelView;
    private LevelInfo _levelInfo;

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
        if (_levelInfo.countStar > 0)
        {
            _imageLvl.sprite = _originImage;
        }
        else
        {
            _imageLvl.sprite = _grayImage;
        }
        _ratingLevelView.UpdateRatingLevel(_levelInfo.countStar);
    }
}
