using UnityEngine;
using System.Collections;

public class Shockbox : BasicTeleporter
{
    [SerializeField] private float _shockStartDelay;
    [SerializeField] private float _shockEndDelay;
    [SerializeField] private GameObject _shockObject;

    [SerializeField] private GameObject _explosionVFX;
    [SerializeField] private GameObject _noSignalZapVFX;
    public void ActivateShockbox()
    {
        GameObject player = FindAnyObjectByType<PlayerBehaviors>().gameObject;
        Instantiate(_noSignalZapVFX, player.transform.position, Quaternion.identity);
        base.TeleportPlayer();
        base.TeleportLocation.GetComponent<EventInteractible>().StartCooldown();
        base.TeleportLocation.GetComponent<Shockbox>().StartCoroutine(base.TeleportLocation.GetComponent<Shockbox>().ShockboxCoroutine());
    }

    public IEnumerator ShockboxCoroutine()
    {
        yield return new WaitForSeconds(_shockStartDelay);

        Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        _shockObject.SetActive(true);

        yield return new WaitForSeconds(_shockEndDelay);

        _shockObject.SetActive(false);
    }
}
