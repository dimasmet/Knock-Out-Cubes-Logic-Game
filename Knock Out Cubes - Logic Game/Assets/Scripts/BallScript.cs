using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rbBall;
    [SerializeField] private float _forceStart;

    [SerializeField] private Transform _pointToSpawn;

    private void Start()
    {
        _rbBall.isKinematic = true;
        GameMain.OnRunLevel += SetStartRun;
        GameMain.OnStopGame += StopBallMove;
        GameMain.OnEndMoveBall += SetStartRun;
    }

    private void OnDestroy()
    {
        GameMain.OnRunLevel -= SetStartRun;
        GameMain.OnStopGame -= StopBallMove;
        GameMain.OnEndMoveBall -= SetStartRun;
    }

    private void SetStartRun()
    {
        _rbBall.velocity = Vector2.zero;
        _rbBall.isKinematic = true;
        _rbBall.position = _pointToSpawn.position;
        StartCoroutine(WaitAddForce());
    }

    private void StopBallMove()
    {
        _rbBall.velocity = Vector2.zero;
        _rbBall.isKinematic = true;
        StopAllCoroutines();
    }

    private IEnumerator WaitAddForce()
    {
        yield return new WaitForSeconds(1.5f);
        _rbBall.isKinematic = false;
        _rbBall.AddForce(Vector2.up * _forceStart, ForceMode2D.Impulse);
    }

    public void SetForceDirection(Vector2 direction)
    {
        _rbBall.velocity = Vector2.zero;
        _rbBall.AddForce(direction * _forceStart, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            coin.HideCoin();
            GameMain.OnCoinTake?.Invoke();
        }
    }
}
