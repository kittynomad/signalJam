using UnityEngine;

public class VFXSuicide : MonoBehaviour
{
    [SerializeField] private BackgroundWallVFX _bW;
    public void Suicide()
    {
        Destroy(gameObject);
    }

    public void MonsterCutscene()
    {
        _bW.TurnWall();
    }
}
