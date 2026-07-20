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
    [SerializeField] private GameObject _cloudLayer;
    [SerializeField] private GameObject _scaries;
    private bool isAttacking;

    [SerializeField] private bool FUCKYOU;


    public void SetTarget(GameObject g)
    {
        targetEnemy = g;
    }

    private void FixedUpdate()
    {
        if (targetEnemy != null)
        {
            if (!(FUCKYOU && isAttacking))
            {
                transform.position = new Vector2(targetEnemy.transform.position.x, targetEnemy.transform.position.y + 0.3f);
            }
            
            RoamingEnemy re = targetEnemy.GetComponent<RoamingEnemy>();
            if (!re.BehindWall)
            {
                if (!FUCKYOU)
                    _anim.Play("MonsterWeakFace");
                else if (re.falling == false)
                    _anim.Play("MonsterWeakChomp");
                pb.HorrorRiser = true;
            }
            else if (isAttacking && re.BehindWall && !(FUCKYOU && isAttacking))
            {
                pb.HorrorRiser = false;
                _anim.Play("MonsterWeakDisappear");
            }
            else if (!isAttacking && re.BehindWall && !(FUCKYOU && isAttacking))
            {
                pb.HorrorRiser = false;
                _anim.Play("nothin");
            }
        }
    }

    public void Attack()
    {
        isAttacking = true;
        targetEnemy.GetComponent<RoamingEnemy>().moveLock = true;
    }

    public void Stop()
    {
        isAttacking = false;
        targetEnemy.GetComponent<RoamingEnemy>().moveLock = false;
    }

    public void MonsterDeath()
    {
        print("hello!");
        if (isAttacking && transform.position.x > 1.5f)
        {
            print("killed monster!");
            pb.HorrorRiser = false;
            pb.ExitHorror();
            _aM.FastCutOff();
            Destroy(_cloudLayer);
            Destroy(_scaries);
            Instantiate(_deathAnimation, new Vector2(transform.position.x - 1.5f, _deathYSpawn), Quaternion.identity);
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
