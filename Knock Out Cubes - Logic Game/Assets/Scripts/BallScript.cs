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

        SetStartRun();
    }

    private void SetStartRun()
    {
        _rbBall.isKinematic = false;
        _rbBall.position = _pointToSpawn.position;
        StartCoroutine(WaitAddForce());
    }

    private IEnumerator WaitAddForce()
    {
        yield return new WaitForSeconds(3);
        //_rbBall.AddForce(new Vector2(100, 100) * _forceStart);
        _rbBall.AddForce(Vector2.up * _forceStart, ForceMode2D.Impulse);
    }

    public void SetForceDirection(Vector2 direction)
    {
        _rbBall.velocity = Vector2.zero;
        _rbBall.AddForce(direction * _forceStart, ForceMode2D.Impulse);
    }
}
