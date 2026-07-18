using UnityEngine;
using System;

public class MusicTrigger : MonoBehaviour
{
    [SerializeField] private AudioManager _aM;
    [SerializeField] private float _musicBehavior;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_musicBehavior == 1)
        {
            _aM.NormalMusicStart();
            Destroy(gameObject);
        }

        if (_musicBehavior == 2)
        {
            _aM.ScaryMusicStart();
            Destroy(gameObject);
        }

        if (_musicBehavior == 3)
        {
            _aM.FinalMusicStart();
            Destroy(gameObject);
        }
    }
}
