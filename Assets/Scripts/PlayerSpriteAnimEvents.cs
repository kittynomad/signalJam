using UnityEngine;

public class PlayerSpriteAnimEvents : MonoBehaviour
{
    public void WalkSound()
    {
        AudioManager.PlaySound("Step");
    }
}
