using UnityEngine;

public class BackgroundWallVFX : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sR;

    [SerializeField] private bool _enabled = true;
    private bool isColorShifting = false;

    private void Start()
    {
        if (!_enabled )
        {
            Color col = _sR.color;
            col.a = 0.2f;
            _sR.color = col;
        }
        else
        {
            Color col = _sR.color;
            col.a = 0.8f;
            _sR.color = col;
        }
    }

    public void TurnWall()
    { 
        isColorShifting = true;
        _enabled = !_enabled;
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
                if (col.a >= 0.8)
                {
                    isColorShifting = false;
                }
            }
            else
            {
                Color col = _sR.color;
                col.a = _sR.color.a - 0.05f;
                _sR.color = col;
                if (col.a <= 0.2)
                {
                    isColorShifting = false;
                }
            }
        }
       
    }
}
