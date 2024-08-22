using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rbBall;
    [SerializeField] private float _forceStart;

    [SerializeField] private Transform _pointToSpawn;

    [SerializeField] private TrailRenderer _trail;

    private void Start()
    {
        _rbBall.isKinematic = true;
        GameMain.OnRunLevel += SetStartRun;
        GameMain.OnStopGame += StopBallMove;
        //GameMain.OnEndMoveBall += SetStartRun;
    }

    private void OnDestroy()
    {
        GameMain.OnRunLevel -= SetStartRun;
        GameMain.OnStopGame -= StopBallMove;
        //GameMain.OnEndMoveBall -= SetStartRun;
    }

    private void SetStartRun()
    {
        _trail.emitting = false;
        _rbBall.velocity = Vector2.zero;
        _rbBall.isKinematic = true;
        _rbBall.position = _pointToSpawn.position;
        //_trail.emitting = true;
        StartCoroutine(WaitAddForce());
    }

    private void StopBallMove()
    {
        _rbBall.velocity = Vector2.zero;
        _rbBall.isKinematic = true;
        _trail.emitting = false;
        StopAllCoroutines();
    }

    private IEnumerator WaitAddForce()
    {
        yield return new WaitForSeconds(0.5f);
        _rbBall.isKinematic = false;
        _rbBall.AddForce(Vector2.up * _forceStart, ForceMode2D.Impulse);
        _trail.emitting = true;
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
