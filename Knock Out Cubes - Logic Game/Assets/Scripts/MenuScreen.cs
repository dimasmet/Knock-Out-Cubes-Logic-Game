using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _levelsBtn;
    [SerializeField] private Button _settingsBtn;
    [SerializeField] private Button _exitBtn;

    private void Awake()
    {
        _playBtn.onClick.AddListener(() =>
        {
            GameMain.main.LevelOpen(0);
        });

        _levelsBtn.onClick.AddListener(() =>
        {
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
    }
}
