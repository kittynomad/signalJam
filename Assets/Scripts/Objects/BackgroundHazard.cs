using UnityEngine;

public class BackgroundHazard : MonoBehaviour
{
    public GameObject _spawnedThingaling;
    private bool playerInZone = false;
    private PlayerBehaviors pb;
    private GameObject currentHound;
    private GameObject target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerBehaviors a))
        {
            playerInZone = true;
            pb = a;
            pb.SCARY();
        }
        if (collision.gameObject.TryGetComponent(out RoamingEnemy re))
        {
            re.Hi(gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerBehaviors a))
        {
            playerInZone = false;
            pb.ExposedTime = 0f;
            pb.safeee();
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out RoamingEnemy re))
        {
            re.Hi(gameObject);
        }
    }

    public void KillDog()
    {
        if (currentHound != null)
        {
            Destroy(currentHound);
        }
    }

    public void SickEm(GameObject g)
    {
        currentHound = Instantiate(_spawnedThingaling, transform.position, Quaternion.identity);
        if (currentHound.GetComponent<WeakFace>() != null)
        {
            currentHound.GetComponent<WeakFace>().SetTarget(g);
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
