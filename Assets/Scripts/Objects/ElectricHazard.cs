using UnityEngine;

public class ElectricHazard : MonoBehaviour
{
    [SerializeField] bool isToggledOn = false;

    private void Start()
    {
        UpdateState();
    }

    public void ToggleActive()
    {
        isToggledOn = !isToggledOn;
        UpdateState();
    }

    private void UpdateState()
    {
        //put visual changes here?
        //im putting placeholder SLOPPP here for now
        if(isToggledOn)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isToggledOn && collision.gameObject.TryGetComponent(out IKillable ik))
        {
            ik.OnDamage(1, gameObject);
        }
    }
}
