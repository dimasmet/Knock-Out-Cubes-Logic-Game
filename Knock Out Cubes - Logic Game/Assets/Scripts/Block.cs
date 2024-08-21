using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider2D _colider;

    public void HideBlock()
    {
        _colider.enabled = false;
        _animator.Play("Hide");
    }
}
