using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] public class PlatfromMarket
{
    public int numItem;
    public int priceItem;
    public bool isBuyed;
    public bool isSelected;
}

[System.Serializable] public class MarketData
{
    public List<PlatfromMarket> platfromMarkets;
}

public class ShopHandler : MonoBehaviour
{
    [SerializeField] private Text _balanceText;
    [SerializeField] private List<Sprite> _skins;

    [SerializeField] private MarketData _marketData;
    [SerializeField] private ItemMarketButton[] _itemMarketButtons;

    [SerializeField] private PlatfromHandler _platfrom;

    private ItemMarketButton _currentActiveItem;
    private int currentNumber;

    private void Start()
    {
        GameMain.BalanceCoins = new BalanceCoins(_balanceText);

        string json = PlayerPrefs.GetString("MarketGameData");
        if (json != "")
            _marketData = JsonUtility.FromJson<MarketData>(json);

        for (int i = 0; i < _itemMarketButtons.Length; i++)
        {
            _itemMarketButtons[i].InitButtonMarket(_marketData.platfromMarkets[i], _skins[i], this);

            if (_marketData.platfromMarkets[i].isSelected)
            {
                _currentActiveItem = _itemMarketButtons[i];
                currentNumber = i;

                SetActiveSkin();
            }
        }
    }

    public void ChoiceItem(int numButton)
    {
        if (_currentActiveItem != _itemMarketButtons[numButton])
        {
            if (_marketData.platfromMarkets[numButton].isBuyed)
            {
                _itemMarketButtons[numButton].ChangeStatus(ItemMarketButton.StatusBuy.Select);

                if (_currentActiveItem != null) _currentActiveItem.ChangeStatus(ItemMarketButton.StatusBuy.Buyed);
                _currentActiveItem = _itemMarketButtons[numButton];

                _marketData.platfromMarkets[currentNumber].isSelected = false;

                currentNumber = numButton;

                _marketData.platfromMarkets[currentNumber].isSelected = true;
                //Save

                SetActiveSkin();

                SaveMarketData();
            }
            else
            {
                if (_marketData.platfromMarkets[numButton].priceItem <= GameMain.BalanceCoins.GetCurrentBalanceValue())
                {
                    GameMain.BalanceCoins.DisCoins(_marketData.platfromMarkets[numButton].priceItem);
                    _marketData.platfromMarkets[numButton].isBuyed = true;
                    _marketData.platfromMarkets[numButton].isSelected = true;

                    //Save

                    _itemMarketButtons[numButton].ChangeStatus(ItemMarketButton.StatusBuy.Select);
                    if (_currentActiveItem != null) _currentActiveItem.ChangeStatus(ItemMarketButton.StatusBuy.Buyed);
                    _currentActiveItem = _itemMarketButtons[numButton];

                    _marketData.platfromMarkets[currentNumber].isSelected = false;

                    currentNumber = numButton;

                    _marketData.platfromMarkets[currentNumber].isSelected = true;

                    SetActiveSkin();

                    SaveMarketData();
                }
            }
        }
    }

    private void SetActiveSkin()
    {
        _platfrom.SetSkinSprite(_skins[currentNumber]);
    }

    private void SaveMarketData()
    {
        string jsonMarket = JsonUtility.ToJson(_marketData);
        PlayerPrefs.SetString("MarketGameData", jsonMarket);
    }
}
