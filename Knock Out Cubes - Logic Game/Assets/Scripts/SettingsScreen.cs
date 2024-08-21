using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private Button _rateGame;
    [SerializeField] private Button _termsOfUse;
    [SerializeField] private Button _privacy;

    [SerializeField] private Button _backButton;

    private void Awake()
    {
        _rateGame.onClick.AddListener(() =>
        {

        });

        _termsOfUse.onClick.AddListener(() =>
        {

        });

        _privacy.onClick.AddListener(() =>
        {

        });

        _backButton.onClick.AddListener(() =>
        {
            Screens.OnScreenOpen(ScreensName.Menu);
        });
    }
}
