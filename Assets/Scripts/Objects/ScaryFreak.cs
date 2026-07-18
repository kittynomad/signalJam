using UnityEngine;

public class ScaryFreak : MonoBehaviour, IKillable
{
    public bool OnDamage(float damageAmount = 1, GameObject damageSource = null)
    {
        OnKill(damageSource);
        return true;
    }

    public void OnKill(GameObject damageSource = null)
    {
        BackgroundHazard[] freakZones = FindObjectsByType<BackgroundHazard>(FindObjectsSortMode.None);
        foreach(BackgroundHazard f in freakZones)
        {
            f.gameObject.SetActive(false);
        }
        Destroy(gameObject);
    }
}
