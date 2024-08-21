using UnityEngine;
using UnityEngine.UI;
using UnityEngine.iOS;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private Button _rateGame;
    [SerializeField] private Button _termsOfUse;
    [SerializeField] private Button _privacy;

    [SerializeField] private Button _backButton;

    [Header("text viewe")]
    [SerializeField] private GameObject _textViewer;
    [SerializeField] private Text _title;
    [SerializeField] private GameObject _textPrivacy;
    [SerializeField] private GameObject _textTerms;
    [SerializeField] private Button _closeTextBtn;

    private void Awake()
    {
        _rateGame.onClick.AddListener(() =>
        {
            Device.RequestStoreReview();
        });

        _termsOfUse.onClick.AddListener(() =>
        {
            _textViewer.SetActive(true);
            _title.text = "Terms of Use";

            _textPrivacy.SetActive(false);
            _textTerms.SetActive(true);
        });

        _privacy.onClick.AddListener(() =>
        {
            _textViewer.SetActive(true);
            _title.text = "Privacy Policy";

            _textPrivacy.SetActive(true);
            _textTerms.SetActive(false);
        });

        _backButton.onClick.AddListener(() =>
        {
            Screens.OnScreenOpen(ScreensName.Menu);
        });

        _closeTextBtn.onClick.AddListener(() =>
        {
            _textViewer.SetActive(false);
        });
    }
}
