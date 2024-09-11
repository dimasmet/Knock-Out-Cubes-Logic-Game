using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private Button _levelsBtn;
    [SerializeField] private Button _marketBtn;
    [SerializeField] private Button _settingsBtn;

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

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
        });

        _levelsBtn.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("ShowRulesGame") != 1)
                ShowRulesGame();
            else
                Screens.OnScreenOpen(ScreensName.Level);

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
        });

        _settingsBtn.onClick.AddListener(() =>
        {
            Screens.OnScreenOpen(ScreensName.Settings);

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
        });

        _nextBtn.onClick.AddListener(() =>
        {
            NextStepRules();

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
        });

        _openRulesGameBtn.onClick.AddListener(() =>
        {
            isUserRulesOpen = true;
            ShowRulesGame();

            MusicHandler.I.RunSound(MusicHandler.NamSound.Tap);
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
