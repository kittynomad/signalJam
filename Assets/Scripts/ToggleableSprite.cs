using UnityEngine;

public class ToggleableSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private bool isToggledOn;
    private GameObject[] babies;

    private void Start()
    {
        
    }
    public void ToggleState()
    {
        isToggledOn = !isToggledOn;
        UpdateState();
    }

    private void UpdateState()
    {
        if(isToggledOn)
        {
            sr.enabled = true;
        }
        else
        {
            sr.enabled = false;
        }
    }
}
