using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private bool _hurtsPlayer = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.TryGetComponent(out Collider2D c2d);
        if (!c2d.isTrigger && collision.gameObject.TryGetComponent(out IKillable ik))
        {
            if(_hurtsPlayer || !collision.gameObject.TryGetComponent(out PlayerBehaviors pb))
                ik.OnDamage(1, gameObject);
        }
    }

}
