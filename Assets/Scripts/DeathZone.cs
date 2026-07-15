using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IKillable ik))
        {
            ik.OnDamage(1, gameObject);
        }
    }
}
