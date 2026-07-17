using UnityEngine;

public class ToggleableSolid : MonoBehaviour
{
    private SpriteRenderer sr;
    private Collider2D coll;

    [SerializeField] private bool isToggledOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        coll = gameObject.GetComponent<Collider2D>();
        UpdateState();
    }

    // Update is called once per frame
    void Update()
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
            coll.enabled = true;
            sr.color = Color.white;
        }
        else
        {

            coll.enabled = false;
            sr.color = new Color(1, 1, 1, 0.5f);
        }
    }
}
