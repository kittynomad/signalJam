using UnityEngine;

public class AnimationEventSound : MonoBehaviour
{
    public string s;
    public string s2;
    
    public void PlaySoundA()
    {
        AudioManager.PlaySound(s);
    }
    public void PlaySoundB()
    {
        AudioManager.PlaySound(s2);
    }

    public void PlaySoundAProxy()
    {
        AudioManager.PlaySound(s, 1f, 1);
    }
}
