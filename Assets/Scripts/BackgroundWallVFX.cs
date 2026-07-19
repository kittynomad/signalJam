using UnityEngine;

public class BackgroundWallVFX : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sR;

    [SerializeField] private bool _enabled = true;
    [SerializeField] private bool isColorShifting = false;

    private void Start()
    {
        if (!_enabled )
        {
            Color col = _sR.color;
            col.a = 0.1f;
            _sR.color = col;
        }
        else
        {
            Color col = _sR.color;
            col.a = 0.9f;
            _sR.color = col;
        }
    }

    public void TurnWall()
    { 
        isColorShifting = true;
        _enabled = !_enabled;
        if ( _enabled )
        {
            AudioManager.PlaySound("WallOn", 1, 2);
        }
        else
        {
            AudioManager.PlaySound("WallOff", 1, 2);
        }
    }

    private void FixedUpdate()
    {
        if (isColorShifting)
        {
            if (_enabled)
            {
                Color col = _sR.color;
                col.a = _sR.color.a + 0.05f;
                _sR.color = col;
                if (col.a >= 0.9)
                {
                    isColorShifting = false;
                }
            }
            else
            {
                Color col = _sR.color;
                col.a = _sR.color.a - 0.05f;
                _sR.color = col;
                if (col.a <= 0.1)
                {
                    isColorShifting = false;
                }
            }
        }
       
    }
}
