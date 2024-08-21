using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelsCubes
{
    public List<Level> _levels;
}

[System.Serializable]
public class LevelInfo
{
    public int numberLvl;
    public int countStar;
    public bool openStatus;
}

public class GameMain : MonoBehaviour
{
    public static Action OnSuccessLevel;

    [SerializeField] private Level[] _levelsPrefabsObjects;

    private Level _currentLevel;
    private int _currentNumberLevel;

    private void Start()
    {
        LevelOpen(0);
    }

    public void LevelOpen(int numberLevel)
    {
        if (_currentLevel != null) Destroy(_currentLevel.gameObject);

        _currentNumberLevel = numberLevel;

        _currentLevel = Instantiate(_levelsPrefabsObjects[_currentNumberLevel], Vector2.zero, Quaternion.identity);
    }
}
