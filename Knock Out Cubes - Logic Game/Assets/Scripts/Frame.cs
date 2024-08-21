using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    [SerializeField] private Block _block;
    private Level _level;

    private bool isOpen = false;

    public void InitFrame(Level level)
    {
        _level = level;
        isOpen = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isOpen)
        {
            if (collision.TryGetComponent(out Block block))
            {
                if (block == _block)
                {
                    block.HideBlock();

                    _level.SuccessFrameEvent();

                    isOpen = false;
                }
            }
        }
    }
}
