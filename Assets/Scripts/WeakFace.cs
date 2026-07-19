using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WeakFace : MonoBehaviour
{
    public GameObject targetEnemy;
    [SerializeField] private PlayerBehaviors pb;
    [SerializeField] private Animator _anim;

    [SerializeField] private GameObject _deathAnimation;
    [SerializeField] private float _deathYSpawn;

    [SerializeField] private AudioManager _aM;
    private bool isAttacking;


    public void SetTarget(GameObject g)
    {
        targetEnemy = g;
    }

    private void FixedUpdate()
    {
        if (targetEnemy != null)
        {
            transform.position = new Vector2 (targetEnemy.transform.position.x, targetEnemy.transform.position.y + 0.3f);
            RoamingEnemy re = targetEnemy.GetComponent<RoamingEnemy>();
            if (!re.BehindWall)
            {
                _anim.Play("MonsterWeakFace");
                pb.HorrorRiser = true;
            }
            else if (isAttacking && re.BehindWall)
            {
                pb.HorrorRiser = false;
                _anim.Play("MonsterWeakDisappear");
            }
            else if (!isAttacking && re.BehindWall)
            {
                pb.HorrorRiser = false;
                _anim.Play("nothin");
            }
        }
    }

    public void Attack()
    {
        isAttacking = true;
    }

    public void Stop()
    {
        isAttacking = false;
    }

    public void MonsterDeath()
    {
        if (isAttacking && transform.position.x > 4)
        {
            print("killed monster!");
            pb.HorrorRiser = false;
            pb.ExitHorror();
            _aM.FastCutOff();
            Instantiate(_deathAnimation, new Vector2(transform.position.x, _deathYSpawn), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void KillTarget()
    {
        pb.HorrorRiser = false;
        AudioManager.PlaySound("MonsterKill");
        targetEnemy.GetComponent<RoamingEnemy>().OnKill(gameObject);
    }
}
