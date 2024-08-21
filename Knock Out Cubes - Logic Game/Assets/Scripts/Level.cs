using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Frame[] _frames;

    private int _countFramesOnLevel;

    private void Start()
    {
        _countFramesOnLevel = _frames.Length;

        for (int i = 0; i < _frames.Length; i++)
        {
            _frames[i].InitFrame(this);
        }
    }

    public void SuccessFrameEvent()
    {
        _countFramesOnLevel--;

        if (_countFramesOnLevel <= 0)
        {
            GameMain.OnSuccessLevel?.Invoke();
        }
    }
}
