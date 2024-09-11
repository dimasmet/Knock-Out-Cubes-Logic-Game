using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    public enum Result
    {
        WinLevel,
        LoseLevel
    }

    private RatingLevelView _ratingLevelView;

    [SerializeField] private GameObject[] _stars;

    [SerializeField] private GameObject _resultView;
    [SerializeField] private GameObject _winView;
    [SerializeField] private GameObject _loseView;

    private Animator _currentAnimator;

    [SerializeField] private Button _nextLvlButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;

    [SerializeField] private Text _coinText;
    [SerializeField] private Text _levelNumberText;

    private void Awake()
    {
        _nextLvlButton.onClick.AddListener(() =>
        {
            GameMain.main.NextLevel();

            _resultView.SetActive(false);
        });

        _restartButton.onClick.AddListener(() =>
        {
            GameMain.main.RestartLevel();

            _resultView.SetActive(false);
        });

        _homeButton.onClick.AddListener(() =>
        {
            Screens.OnScreenOpen?.Invoke(ScreensName.Menu);
            _resultView.SetActive(false);
        });
    }

    private void Start()
    {
        _ratingLevelView = new RatingLevelView(_stars);
    }

    public void ShowResult(int numberLvl, Result result, int countStar, int countCoin = 0)
    {
        switch (result)
        {
            case Result.WinLevel:
                _winView.SetActive(true);
                _loseView.SetActive(false);
                _currentAnimator = _winView.GetComponent<Animator>();

                _coinText.text = countCoin.ToString();

                MusicHandler.I.RunSound(MusicHandler.NamSound.SoundWin);
                break;
            case Result.LoseLevel:
                _winView.SetActive(false);
                _loseView.SetActive(true);
                _currentAnimator = _loseView.GetComponent<Animator>();

                MusicHandler.I.RunSound(MusicHandler.NamSound.SoundLose);
                break;
        }

        _levelNumberText.text = "LEVEL - " + numberLvl;
        _ratingLevelView.UpdateRatingLevel(countStar);

        StartCoroutine(WaitToShow());
    }

    private IEnumerator WaitToShow()
    {
        yield return new WaitForSeconds(1f);
        _resultView.SetActive(true);
        _resultView.GetComponent<Animator>().Play("Open");
        _currentAnimator.Play("Open");
    }
}
