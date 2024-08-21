using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMarketButton : MonoBehaviour
{
    public enum StatusBuy
    {
        Select,
        NoBuy,
        Buyed
    }

    [SerializeField] private Button _btnItem;
    [SerializeField] private Image _imgSkin;

    [SerializeField] private Text _priceText;
    [SerializeField] private Text _statusText;

    private PlatfromMarket _current;

    private ShopHandler shop;

    private void Awake()
    {
        _btnItem.onClick.AddListener(() =>
        {
            shop.ChoiceItem(_current.numItem);
        });
    }

    public void InitButtonMarket(PlatfromMarket platfromMarket, Sprite skin, ShopHandler shopScreen)
    {
        shop = shopScreen;
        _imgSkin.sprite = skin;

        _current = platfromMarket;

        if (_current.isBuyed) ChangeStatus(StatusBuy.Buyed);
        if (_current.isSelected) ChangeStatus(StatusBuy.Select);

        if (_current.isBuyed == false && _current.isSelected == false)
        {
            ChangeStatus(StatusBuy.NoBuy);
        }
    }

    public void ChangeStatus(StatusBuy statusBuy)
    {
        switch (statusBuy)
        {
            case StatusBuy.Select:
                _statusText.text = "SELECTED";
                _priceText.transform.parent.gameObject.SetActive(false);
                _btnItem.interactable = true;
                break;
            case StatusBuy.NoBuy:
                _btnItem.interactable = true;
                _priceText.text = _current.priceItem.ToString();
                break;
            case StatusBuy.Buyed:
                _btnItem.interactable = true;
                _priceText.transform.parent.gameObject.SetActive(false);
                _statusText.text = "PURCHASED";
                break;
        }
    }
}
