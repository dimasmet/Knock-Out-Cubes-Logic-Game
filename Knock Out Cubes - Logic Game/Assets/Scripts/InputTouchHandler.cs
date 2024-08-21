using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouchHandler : MonoBehaviour
{
    private bool isTrakingTouch = false;

    public static Vector2 PositionTouch;

    public void TrakingTouch(bool isActive)
    {
        isTrakingTouch = isActive;
    }

    private void Update()
    {
        if (isTrakingTouch)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                PositionTouch = Camera.main.ScreenToWorldPoint(touch.position);
            }
        }
    }
}
