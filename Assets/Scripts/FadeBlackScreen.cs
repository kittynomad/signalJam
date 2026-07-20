using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeBlackScreen : MonoBehaviour
{
    [SerializeField] private Image _blackScreen;
    [SerializeField] private float _fadeTime;

    private void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(DoFadeIn(_fadeTime));
    }

    public void FadeOut(float dur)
    {
        StartCoroutine(DoFadeOut(dur));
    }

    public IEnumerator DoFadeIn(float fadeTime)
    {
        _blackScreen.color = new Color(0, 0, 0, 1);
        while(_blackScreen.color.a > 0)
        {
            _blackScreen.color = new Color(0, 0, 0, _blackScreen.color.a - (fadeTime * Time.deltaTime)) ;
            yield return new WaitForFixedUpdate();
        }
        _blackScreen.enabled = false;
    }

    public IEnumerator DoFadeOut(float fadeTime)
    {
        _blackScreen.enabled = true;
        _blackScreen.color = new Color(0, 0, 0, 0);
        while (_blackScreen.color.a < 1)
        {
            _blackScreen.color = new Color(0, 0, 0, _blackScreen.color.a + (fadeTime * Time.deltaTime));
            yield return new WaitForFixedUpdate();
        }
    }
}
