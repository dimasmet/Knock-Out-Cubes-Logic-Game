using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject _pauseViewPanel;

    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _menuBtn;

    private void Awake()
    {
        _restartBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            GameMain.main.RestartLevel();

            _pauseViewPanel.SetActive(false);
        });

        _continueBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            _pauseViewPanel.SetActive(false);
        });

        _menuBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            Screens.OnScreenOpen?.Invoke(ScreensName.Menu);
            _pauseViewPanel.SetActive(false);
        });
    }

    public void ShowPause()
    {
        _pauseViewPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
