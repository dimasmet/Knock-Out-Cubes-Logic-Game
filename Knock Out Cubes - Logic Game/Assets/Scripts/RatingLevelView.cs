using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingLevelView 
{
    private GameObject[] _stars;

    public RatingLevelView(GameObject[] stars)
    {
        _stars = stars;

        HideStarsAll();
    }

    private void HideStarsAll()
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].SetActive(false);
        }
    }

    public void UpdateRatingLevel(int countStars)
    {
        HideStarsAll();
        int count = _stars.Length - (_stars.Length - countStars);

        for (int i = 0; i < count; i++)
        {
            _stars[i].SetActive(true);
        }
    }
}
