using UnityEngine;
using UnityEngine.Events;

public class EventInteractible : InteractibleEntity
{
    [SerializeField] private UnityAction _triggeredActions;
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
        connected = worked;
        lr.enabled = connected;

        if (worked)
        {
            player = collision.gameObject;
            _triggeredActions.Invoke();
        }

        return worked;
    }

    public override bool TryUnsubscribeInteraction(Collider2D collision)
    {
        bool worked = base.TryUnsubscribeInteraction(collision);
        connected = !worked;
        lr.enabled = connected;

        return worked;
    }


}
