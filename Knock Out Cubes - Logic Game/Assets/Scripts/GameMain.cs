using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelsCubes
{
    public List<LevelInfo> levels;
}

[System.Serializable]
public class LevelInfo
{
    public int numberLvl;
    public int countStar;
    public bool openStatus;
}

public class GameMain : MonoBehaviour
{
    public static GameMain main;

    public static Action OnRunLevel;
    public static Action OnStopGame;
    public static Action OnSuccessLevel;
    public static Action OnEndMoveBall;
    public static Action OnCoinTake;

    [SerializeField] private LevelsCubes _levelsCubes;
    [SerializeField] private Level[] _levelsPrefabsObjects;
    [SerializeField] private LevelsScreen _levelsScreen;

    [SerializeField] private ResultScreen _resultScreen;
    [SerializeField] private GameScreen _gameScreen;

    public static BalanceCoins BalanceCoins;

    private Level _currentLevel;
    private int _currentNumberLevel;

    private int _NumberAttempts;

    private int _countCoinCollectOnLevel;

    [SerializeField] private Text _countBallsText;

    private void Start()
    {
        OnEndMoveBall += AttemptUsed;
        OnSuccessLevel += SuccessLevel;
        OnCoinTake += CollectCoin;

        _levelsScreen.SetDataLevels(_levelsCubes.levels);
    }

    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }

        string saveLocal = PlayerPrefs.GetString("PlayerResultLevels");
        if (saveLocal != "")
        {
            _levelsCubes = JsonUtility.FromJson<LevelsCubes>(saveLocal);
        }
    }

    private void OnDestroy()
    {
        OnEndMoveBall -= AttemptUsed;
        OnSuccessLevel -= SuccessLevel;
        OnCoinTake -= CollectCoin;
    }

    public void NextLevel()
    {
        OnStopGame?.Invoke();
        if (_currentNumberLevel < _levelsCubes.levels.Count - 1)
        {
            _currentNumberLevel++;
            LevelOpen(_currentNumberLevel);
        }
    }

    public void RestartLevel()
    {
        OnStopGame?.Invoke();
        LevelOpen(_currentNumberLevel);
    }

    private void CollectCoin()
    {
        _countCoinCollectOnLevel++;
    }

    public void LevelOpen(int numberLevel)
    {
        _countCoinCollectOnLevel = 0;

        if (_currentLevel != null) Destroy(_currentLevel.gameObject);

        _currentNumberLevel = numberLevel;

        _currentLevel = Instantiate(_levelsPrefabsObjects[_currentNumberLevel], Vector2.zero, Quaternion.identity);

        OnRunLevel?.Invoke();

        _NumberAttempts = 3;
        _countBallsText.text = _NumberAttempts.ToString();

        _gameScreen.SetLevelTitle(_currentNumberLevel + 1);

        Screens.OnScreenOpen(ScreensName.Game);
    }

    private void AttemptUsed()
    {
        _NumberAttempts--;
        if (_NumberAttempts <= 0)
        {
            _resultScreen.ShowResult(ResultScreen.Result.LoseLevel, _NumberAttempts);
            OnStopGame?.Invoke();
        }
        else
        {
            OnRunLevel?.Invoke();
        }

        _countBallsText.text = _NumberAttempts.ToString();
    }

    private void SuccessLevel()
    {
        _resultScreen.ShowResult(ResultScreen.Result.WinLevel, _NumberAttempts, _countCoinCollectOnLevel);

        BalanceCoins.AddCoin(_countCoinCollectOnLevel);

        OnStopGame?.Invoke();

        if (_levelsCubes.levels[_currentNumberLevel].countStar < _NumberAttempts)
        {
            _levelsCubes.levels[_currentNumberLevel].countStar = _NumberAttempts;
            SaveResultPlayer();
        }

        if ((_currentNumberLevel < _levelsCubes.levels.Count - 1) && _levelsCubes.levels[_currentNumberLevel + 1].openStatus == false)
        {
            _levelsCubes.levels[_currentNumberLevel + 1].openStatus = true;
            SaveResultPlayer();
        }

    }

    private void SaveResultPlayer()
    {
        string jsonSave = JsonUtility.ToJson(_levelsCubes);
        PlayerPrefs.SetString("PlayerResultLevels", jsonSave);
    }
}
