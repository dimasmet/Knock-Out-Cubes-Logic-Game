using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreensName
{
    Menu,
    Game,
    Level
}

public class Screens : MonoBehaviour
{
    public static Action<ScreensName> OnScreenOpen; 

    [SerializeField] private GameObject _screenMenu;
    [SerializeField] private GameObject _screenGame;
    [SerializeField] private GameObject _screenLevel;

    private GameObject _screenActive;

    private void Start()
    {
        OnScreenOpen += ScreenActive;
    }

    private void OnDestroy()
    {
        OnScreenOpen -= ScreenActive;
    }

    private void ScreenActive(ScreensName screen)
    {
        if (_screenActive != null) _screenActive.SetActive(false);

        switch (screen)
        {
            case ScreensName.Menu:
                _screenActive = _screenMenu;
                break;
            case ScreensName.Game:
                _screenActive = _screenGame;
                break;
            case ScreensName.Level:
                _screenActive = _screenLevel;
                break;
        }

        _screenActive.SetActive(true);
    }
}
