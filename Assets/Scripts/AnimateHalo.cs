using UnityEngine;

public class AnimateHalo : MonoBehaviour
{
    [SerializeField] private GameObject _halo;

    public void HaloOn()
    {
        _halo.SetActive(true);
    }
    public void HaloOff()
    {
        _halo.SetActive(false);
    }
}
