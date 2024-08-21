using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RatingLevelView 
{
    [SerializeField] private GameObject[] _stars;

    public RatingLevelView(int countStar)
    {
        int count = _stars.Length - (_stars.Length - countStar);

        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].SetActive(true);
        }
    }
}
