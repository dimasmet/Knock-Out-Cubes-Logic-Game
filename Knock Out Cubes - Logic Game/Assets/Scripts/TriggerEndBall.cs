using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEndBall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BallScript ball))
        {
            GameMain.OnEndMoveBall?.Invoke();
        }
    }
}
