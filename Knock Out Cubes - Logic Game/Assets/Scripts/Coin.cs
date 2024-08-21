using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Animator _animCoin;
    [SerializeField] private CircleCollider2D _colider;

    public void HideCoin()
    {
        _colider.enabled = false;
        _animCoin.Play("Hide");
    }
}
