using UnityEngine;

public class WeakFace : MonoBehaviour
{
    public GameObject targetEnemy;
    private PlayerBehaviors pb;

    private void Start()
    {
        pb = FindAnyObjectByType<PlayerBehaviors>().GetComponent<PlayerBehaviors>();
        pb.HorrorRiser = true;
    }

    public void SetTarget(GameObject g)
    {
        targetEnemy = g;
    }

    private void FixedUpdate()
    {
        if (targetEnemy != null)
        {
            transform.position = targetEnemy.transform.position;
        }
    }

    public void KillTarget()
    {
        pb.HorrorRiser = false;
        targetEnemy.GetComponent<RoamingEnemy>().OnKill(gameObject);
    }
}
