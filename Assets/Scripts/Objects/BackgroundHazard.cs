using UnityEngine;

public class BackgroundHazard : MonoBehaviour
{
    [SerializeField] private GameObject _spawnedThingaling;
    private bool playerInZone = false;
    private PlayerBehaviors pb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerBehaviors a))
        {
            playerInZone = true;
            pb = a;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerBehaviors a))
        {
            playerInZone = false;
            pb.ExposedTime = 0f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out RoamingEnemy re))
        {
            if(re.ExposedFunction())
            {
                Instantiate(_spawnedThingaling, re.transform.position, Quaternion.identity);
                //animation here
                re.OnKill();
            }
        }
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
