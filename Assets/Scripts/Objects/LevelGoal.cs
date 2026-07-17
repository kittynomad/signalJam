using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private float _leeway = 0.5f;
    [SerializeField] private GameObject _nextLevelStartLocation;
    [SerializeField] private float _animationDelay;
    [SerializeField] private Animator _anim;

    [SerializeField] private GameObject _noSignalZapVFX;

    private GameObject player;
    public GameObject playerAnim;
    public void InitiateLevelTransition()
    {
        player = FindAnyObjectByType<PlayerBehaviors>().gameObject;
        playerAnim = GameObject.FindGameObjectWithTag("PlayerSprite");
        player.GetComponent<PlayerInput>().DeactivateInput();
        player.GetComponent<Collider2D>().isTrigger = true;
        Instantiate(_noSignalZapVFX, player.transform.position, Quaternion.identity);
        player.SetActive(false);
        _anim.Play("LevelEndChargeShoot");
        Destroy(gameObject.GetComponent<Collider2D>());
        StartCoroutine(LevelTransitionCoroutine());
        
    }
    private void EndLevelTransition()
    {
        playerAnim.GetComponent<Animator>().SetBool("IsLaunching", false);
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player.GetComponent<PlayerController>().MovementDirection = Vector2.zero; 
        player.GetComponent<PlayerInput>().ActivateInput();
        player.GetComponent<Collider2D>().isTrigger = false;
    }

    private IEnumerator LevelTransitionCoroutine()
    {
        yield return new WaitForSeconds(_animationDelay);
        player.SetActive(true);
        playerAnim.GetComponent<Animator>().SetBool("IsLaunching", true);
        player.transform.position = new Vector2(transform.position.x, transform.position.y + 1.15f);
        while (Vector2.Distance(player.GetComponent<Rigidbody2D>().position, _nextLevelStartLocation.transform.position) >= _leeway)
        {
            player.GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(player.GetComponent<Rigidbody2D>().position, _nextLevelStartLocation.transform.position, _transitionSpeed));
            yield return new WaitForFixedUpdate();
        }
        EndLevelTransition();
    }
}
