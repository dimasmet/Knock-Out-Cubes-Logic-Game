using UnityEngine;
using UnityEngine.UI;

public class MarketScreen : MonoBehaviour
{
    [SerializeField] private Button _backButton;

    private void Awake()
    {
        _backButton.onClick.AddListener(() =>
        {
            Screens.OnScreenOpen(ScreensName.Menu);
        });
    }
}
