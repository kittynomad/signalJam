using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventInteractible : InteractibleEntity
{
    [SerializeField] private UnityEvent _triggeredActions;
    [SerializeField] private bool _reusable = true;
    [SerializeField] private float _cooldownTime = 0.75f;

    [SerializeField] private Animator _anim;
    [SerializeField] private bool _isShockbox;

    //private LineRenderer lr;
    private bool connected = false;
    private GameObject player;
    private bool OnCooldown = false;
    void Start()
    {
        //lr = gameObject.GetComponent<LineRenderer>();
        //lr.enabled = false;
    }

    private void FixedUpdate()
    {
        if(connected)
        {
            //lr.SetPositions(new Vector3[] {transform.position, player.transform.position });
        }
    }

    public override bool TrySubscribeInteraction(Collider2D collision)
    {
        bool worked = base.TrySubscribeInteraction(collision);
        

        if (worked)
        {
            if (!_isShockbox)
            {
                _anim.Play("ButtonTurnOn");
            }
            else
            {
                _anim.Play("ShockboxTurnOn");
            }
            player = collision.gameObject;
            connected = worked;
            //lr.enabled = connected;
        }

        return worked;
    }

    public override bool TryUnsubscribeInteraction(Collider2D collision)
    {
        bool worked = base.TryUnsubscribeInteraction(collision);
        if (worked)
        {
            if (!_isShockbox)
            {
                _anim.Play("ButtonTurnOff");
            }
            else
            {
                _anim.Play("ShockboxTurnOff");
            }
            connected = false;
        }
        //lr.enabled = connected;

        return worked;
    }

    public override void OnInteract(PlayerBehaviors pb)
    {
        base.OnInteract(pb);
        if(!OnCooldown)
        {
            StopAllCoroutines();
            _triggeredActions.Invoke();
            StartCoroutine(CooldownCoroutine());
        }
        
    }

    public void StartCooldown()
    {
        StopAllCoroutines();
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        OnCooldown = true;
        yield return new WaitForSeconds(_cooldownTime);
        OnCooldown = false;
    }


}
