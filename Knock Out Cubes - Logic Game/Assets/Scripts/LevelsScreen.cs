using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsScreen : MonoBehaviour
{
    [SerializeField] private BtnLvl[] _buttonsLevels;

    [SerializeField] private Button _backButton;

    private void Awake()
    {
        _backButton.onClick.AddListener(() =>
        {
            Screens.OnScreenOpen(ScreensName.Menu);
        });
    }

    public void SetDataLevels(List<LevelInfo> levelInfos)
    {
        for (int i = 0; i < _buttonsLevels.Length; i++)
        {
            _buttonsLevels[i].SetDataButtonLvl(levelInfos[i]);
        }
    }
}
