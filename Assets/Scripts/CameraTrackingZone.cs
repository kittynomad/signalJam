using UnityEngine;

public class CameraTrackingZone : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] private float _playerFollowStrengthX = 0f;
    [Range(0f, 1f)] [SerializeField] private float _playerFollowStrengthY = 0f;
    [Range(0f, 1f)] [SerializeField] private float _velocityInfluence = 0f;
    [SerializeField] private float _cameraDistance;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerBehaviors>(out PlayerBehaviors pb))
        {
            Vector2 pointToFollow = collision.gameObject.GetComponent<Rigidbody2D>().position;
            Vector2 playerVel = pb.gameObject.GetComponent<Rigidbody2D>().linearVelocity;
            Vector3 startingPos = Camera.main.transform.localPosition;

            pointToFollow.x = (gameObject.transform.position.x * (1f - _playerFollowStrengthX)) + ((pointToFollow.x + (playerVel.x * _velocityInfluence)) * _playerFollowStrengthX);//Camera.main.transform.position.x;
            pointToFollow.y = (gameObject.transform.position.y * (1f - _playerFollowStrengthY)) + ((pointToFollow.y + (playerVel.y * _velocityInfluence)) * _playerFollowStrengthY); //Camera.main.transform.position.y;
            //move towards the target point in a fairly smooth way
            Camera.main.transform.localPosition = Vector3.MoveTowards(startingPos, new Vector3(pointToFollow.x, pointToFollow.y, _cameraDistance),
                ((Mathf.Abs(pointToFollow.x - startingPos.x) / 10) + (Mathf.Abs(pointToFollow.y - startingPos.y) / 10)));
        }
    }
}
