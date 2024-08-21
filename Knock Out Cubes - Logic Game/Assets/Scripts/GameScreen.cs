using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private Text _titleLevel;
    [SerializeField] private Button _pauseBtn;

    [SerializeField] private PauseScreen _pauseScreen;

    private void Awake()
    {
        _pauseBtn.onClick.AddListener(() =>
        {
            _pauseScreen.ShowPause();
        });
    }

    public void SetLevelTitle(int numberLevel)
    {
        _titleLevel.text = "LEVEL - " + numberLevel;
    }
}
