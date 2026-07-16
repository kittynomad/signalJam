using UnityEngine;

public class BackgroundHazard : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        print("stayyyy");
        if (collision.gameObject.TryGetComponent(out PlayerBehaviors pb))
        {
            if(pb.ExposedFunction())
            {
                //play animation or whatever
                pb.OnKill();
            }
        }
    }
}
