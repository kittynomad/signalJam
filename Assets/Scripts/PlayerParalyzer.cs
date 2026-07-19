using UnityEngine;

public class PlayerParalyzer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerBehaviors pb))
        {
            pb.Paralyze();
        }
    }
}
