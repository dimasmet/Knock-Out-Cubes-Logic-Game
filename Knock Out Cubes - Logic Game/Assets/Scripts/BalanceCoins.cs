using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceCoins
{
    private Text _coinTextBalance;

    private int _countCoin = 0;

    public BalanceCoins(Text text)
    {
        _coinTextBalance = text;

        _countCoin = PlayerPrefs.GetInt("BalancePlayer");

#if UNITY_EDITOR
        _countCoin = 300;
#endif

        UpdateText();
    }

    private void UpdateText()
    {
        _coinTextBalance.text = _countCoin.ToString();
        PlayerPrefs.SetInt("BalancePlayer", _countCoin);
    }

    public void AddCoin(int value)
    {
        _countCoin += value;
        UpdateText();
    }

    public void DisCoins(int value)
    {
        _countCoin -= value;
        UpdateText();
    }

    public int GetCurrentBalanceValue()
    {
        return _countCoin;
    }
}
