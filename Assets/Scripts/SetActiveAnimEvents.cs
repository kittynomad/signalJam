using UnityEngine;

public class SetActiveAnimEvents : MonoBehaviour
{
    [SerializeField] private GameObject _gO;
    
    public void TurnON()
    {
        _gO.SetActive(true);
    }

    public void TurnOFF()
    {
        _gO.SetActive(false);
    }
}
