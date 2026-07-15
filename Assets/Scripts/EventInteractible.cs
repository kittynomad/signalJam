using UnityEngine;
using UnityEngine.Events;

public class EventInteractible : InteractibleEntity
{
    [SerializeField] private UnityEvent _triggeredActions;
    [SerializeField] private bool _reusable = true;

    private LineRenderer lr;
    private bool connected = false;
    private GameObject player;
    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        lr.enabled = false;
    }

    private void FixedUpdate()
    {
        if(connected)
        {
            lr.SetPositions(new Vector3[] {transform.position, player.transform.position });
        }
    }

    public override bool TrySubscribeInteraction(Collider2D collision)
    {
        bool worked = base.TrySubscribeInteraction(collision);
        

        if (worked)
        {
            player = collision.gameObject;
            connected = worked;
            lr.enabled = connected;
        }

        return worked;
    }

    public override bool TryUnsubscribeInteraction(Collider2D collision)
    {
        bool worked = base.TryUnsubscribeInteraction(collision);
        if (worked) connected = false;
        lr.enabled = connected;

        return worked;
    }

    public override void OnInteract(PlayerBehaviors pb)
    {
        base.OnInteract(pb);
        _triggeredActions.Invoke();
    }


}
