using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.TryGetComponent(out Collider2D c2d);
        if (!c2d.isTrigger && collision.gameObject.TryGetComponent(out IKillable ik))
        {
            ik.OnDamage(1, gameObject);
        }
    }
}
