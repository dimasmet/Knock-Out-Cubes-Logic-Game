using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromHandler : MonoBehaviour
{
    [SerializeField] private InputTouchHandler _inputTouchHandler;

    [SerializeField] private SpriteRenderer _spritePlatform;
    [SerializeField] private Rigidbody2D _rbPlatfrom;

    [SerializeField] private float _speedMove;
    private bool isMove = false;

    private void Start()
    {
        GameMain.OnRunLevel += ActivePlatfrom;
        GameMain.OnStopGame += StopPlatform;
    }

    private void OnDestroy()
    {
        GameMain.OnRunLevel -= ActivePlatfrom;
        GameMain.OnStopGame -= StopPlatform;
    }

    private void StopPlatform()
    {
        _inputTouchHandler.TrakingTouch(false);
        isMove = false;
    }

    private void ActivePlatfrom()
    {
        Vector2 pos = _rbPlatfrom.position;
        pos.x = 0;
        _rbPlatfrom.position = pos;
        StartCoroutine(WaitToTraking());
    }

    private IEnumerator WaitToTraking()
    {
        yield return new WaitForSeconds(0.5f);
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

    public void SetSkinSprite(Sprite sprite)
    {
        _spritePlatform.sprite = sprite;
    }
}
