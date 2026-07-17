using UnityEngine;
using System.Collections;

public class Shockbox : BasicTeleporter
{
    [SerializeField] private float _shockStartDelay;
    [SerializeField] private float _shockEndDelay;
    [SerializeField] private GameObject _shockObject;
    public void ActivateShockbox()
    {
        base.TeleportPlayer();
        base.TeleportLocation.GetComponent<EventInteractible>().StartCooldown();
        base.TeleportLocation.GetComponent<Shockbox>().StartCoroutine(base.TeleportLocation.GetComponent<Shockbox>().ShockboxCoroutine());
    }

    public IEnumerator ShockboxCoroutine()
    {
        yield return new WaitForSeconds(_shockStartDelay);

        _shockObject.SetActive(true);

        yield return new WaitForSeconds(_shockEndDelay);

        _shockObject.SetActive(false);
    }
}
