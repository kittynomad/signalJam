using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private GameObject _nextLevelStartLocation;

    private GameObject player;
    public void InitiateLevelTransition()
    {
        player = FindAnyObjectByType<PlayerBehaviors>().gameObject;
        player.GetComponent<PlayerInput>().DeactivateInput();
        StartCoroutine(LevelTransitionCoroutine());
        
    }
    private void EndLevelTransition()
    {
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player.GetComponent<PlayerInput>().ActivateInput();
    }

    private IEnumerator LevelTransitionCoroutine()
    {
        while (Vector2.Distance(player.GetComponent<Rigidbody2D>().position, _nextLevelStartLocation.transform.position) >= 0.5f)
        {
            player.GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(player.GetComponent<Rigidbody2D>().position, _nextLevelStartLocation.transform.position, _transitionSpeed));
            yield return new WaitForFixedUpdate();
        }
        EndLevelTransition();
    }
}
