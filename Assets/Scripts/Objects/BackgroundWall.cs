using UnityEngine;

public class BackgroundWall : MonoBehaviour
{
    [SerializeField] private bool isActive;
    private Collider2D _zone;
    private SpriteRenderer sr;

    private void Start()
    {
        _zone = gameObject.GetComponent<Collider2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        UpdateState();
    }

    public void ToggleActive()
    {
        isActive = !isActive;
    }

    private void UpdateState()
    {
        sr.color = isActive ? Color.white : new Color(1, 1, 1, 0.5f);
        _zone.enabled = isActive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerBehaviors pb))
        {
            pb.BehindWall = true;
        }
        else if (collision.gameObject.TryGetComponent(out RoamingEnemy re))
        {
            re.BehindWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerBehaviors pb))
        {
            pb.BehindWall = false;
        }
        else if (collision.gameObject.TryGetComponent(out RoamingEnemy re))
        {
            re.BehindWall = true;
        }
    }
}
