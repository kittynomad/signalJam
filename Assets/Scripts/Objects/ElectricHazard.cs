using UnityEngine;

public class ElectricHazard : MonoBehaviour
{
    [SerializeField] bool isToggledOn = false;

    public void ToggleActive()
    {
        isToggledOn = !isToggledOn;
        UpdateState();
    }

    private void UpdateState()
    {
        //put visual changes here?
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isToggledOn && collision.gameObject.TryGetComponent(out IKillable ik))
        {
            ik.OnDamage(1, gameObject);
        }
    }
}
