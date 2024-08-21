using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] private Text _balanceText;

    private void Start()
    {
        GameMain.BalanceCoins = new BalanceCoins(_balanceText);
    }
}
