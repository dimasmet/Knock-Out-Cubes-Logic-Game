using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenStatisticLevels : MonoBehaviour
{
    private List<LevelInfo> _listLevelInfo;

    [SerializeField] private LvlStatItem[] lvlStatItems;

    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _openBtn;
    [SerializeField] private Button _closeBtn;

    private void Awake()
    {
        _openBtn.onClick.AddListener(() =>
        {
            _panel.SetActive(true);
        });
        _closeBtn.onClick.AddListener(() =>
        {
            _panel.SetActive(false);
        });
    }

    public void SetDataLevels(List<LevelInfo> levelInfos)
    {
        _listLevelInfo = levelInfos;

        for (int i = 0; i < lvlStatItems.Length; i++)
        {
            lvlStatItems[i].SetDataButtonLvl(_listLevelInfo[i]);
        }
    }
}
