using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private Button _levelsBtn;
    [SerializeField] private Button _marketBtn;
    [SerializeField] private Button _settingsBtn;
    [SerializeField] private Button _exitBtn;

    [Header("Rules game")]
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private Button _nextBtn;
    private GameObject _currPanelActive;
    private int numPage = 0;

    [SerializeField] private Button _openRulesGameBtn;
    private bool isUserRulesOpen = false;

    private void Awake()
    {
        _marketBtn.onClick.AddListener(() =>
        {
            Screens.OnScreenOpen(ScreensName.Market);
        });

        _levelsBtn.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("ShowRulesGame") != 1)
                ShowRulesGame();
            else
                Screens.OnScreenOpen(ScreensName.Level);
        });

        _settingsBtn.onClick.AddListener(() =>
        {
            Screens.OnScreenOpen(ScreensName.Settings);
        });

        _exitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        _nextBtn.onClick.AddListener(() =>
        {
            NextStepRules();
        });

        _openRulesGameBtn.onClick.AddListener(() =>
        {
            isUserRulesOpen = true;
            ShowRulesGame();
        });
    }

    private void ShowRulesGame()
    {
        numPage = 0;
        _mainPanel.SetActive(true);

        if (_currPanelActive != null) _currPanelActive.SetActive(false);
        _currPanelActive = _panels[numPage];
        _currPanelActive.SetActive(true);
    }

    private void NextStepRules()
    {
        numPage++;

        if (numPage < _panels.Length)
        {
            if (_currPanelActive != null) _currPanelActive.SetActive(false);
            _currPanelActive = _panels[numPage];
            _currPanelActive.SetActive(true);
        }
        else
        {
            
            if (isUserRulesOpen == false)
            {
                Screens.OnScreenOpen(ScreensName.Level);
                PlayerPrefs.SetInt("ShowRulesGame", 1);
            }
            else
                isUserRulesOpen = false;
            _mainPanel.SetActive(false);
        }
    }
}
