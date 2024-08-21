using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromHandler : MonoBehaviour
{
    [SerializeField] private InputTouchHandler _inputTouchHandler;

    [SerializeField] private Rigidbody2D _rbPlatfrom;

    [SerializeField] private float _speedMove;
    private bool isMove = false;

    private void Start()
    {
        ActivePlatfrom();
    }

    public void ActivePlatfrom()
    {
        _inputTouchHandler.TrakingTouch(true);
        isMove = true;
    }

    public void Update()
    {
        if (isMove)
        {
            float Ox = InputTouchHandler.PositionTouch.x;
            Ox = Mathf.Clamp(Ox, -1.6f, 1.6f);
            _rbPlatfrom.position = new Vector2(Ox, _rbPlatfrom.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out BallScript ball))
        {
            Vector2 directionToMove = (ball.transform.position - transform.position).normalized;
            ball.SetForceDirection(directionToMove);
        }
    }

}
