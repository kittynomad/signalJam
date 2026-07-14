using UnityEngine;

public interface IKillable
{
    public bool OnDamage(float damageAmount = 1f, GameObject damageSource = null);

    public void OnKill(GameObject damageSource = null);
}
