using UnityEngine;

public abstract class InteractibleEntity : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        TrySubscribeInteraction(collision);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        TryUnsubscribeInteraction(collision);
    }

    public virtual bool TrySubscribeInteraction(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerBehaviors pb))
        {
            pb.interactAction += OnInteract;
            return true;
        }
        return false;
    }

    public virtual bool TryUnsubscribeInteraction(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerBehaviors pb))
        {
            pb.interactAction -= OnInteract;
            return true;
        }
        return false;
    }

    public virtual void OnInteract(PlayerBehaviors pb)
    {
        Debug.Log("object " + gameObject.name + " interacted smile");
    }
}
