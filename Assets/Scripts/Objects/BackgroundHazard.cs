using UnityEngine;

public class BackgroundHazard : MonoBehaviour
{
    private bool playerInZone = false;
    private PlayerBehaviors pb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerBehaviors a))
        {
            playerInZone = true;
            pb = a;
        }
        else if (collision.gameObject.TryGetComponent(out IKillable ik))
        {
            //cool animation here probably
            ik.OnKill();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInZone = false;
        pb.ExposedTime = 0f;
    }

    private void FixedUpdate()
    {
        if (playerInZone && pb.ExposedFunction())
        {
            playerInZone = false;
            //play animation or whatever
            pb.OnKill();
        }
    }
}
