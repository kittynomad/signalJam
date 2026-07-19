using UnityEngine;

public class ScaryFace : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    public void FaceOn()
    {
        _anim.SetBool("FaceOn", true);
    }
    public void FaceOff()
    {
        _anim.SetBool("FaceOn", false);
    }

    public void HandOut()
    {
        _anim.SetBool("HandOut", true);
    }

    public void HandAway()
    {
        _anim.SetBool("HandOut", false);
    }

    public void DahBihGah()
    {
        //_anim.SetBool("Safe", false);
    }
    public void FUCKYOUDIE()
    {
        _anim.SetBool("DeathSeq", true);
    }
    public void PizzaFriendlyy()
    {
        _anim.SetBool("DeathSeq", false);
    }
}
