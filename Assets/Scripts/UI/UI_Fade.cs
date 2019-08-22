using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Fade : MonoBehaviour
{
    public static UI_Fade instance;

    [Header("FadeImage")]
    public Image fadeImage;

	private void Awake()
	{
        instance = this;
	}

    public void Fade(System.Action _onFinished , float _speed, float _delay , System.Action _onMiddleReached = null)
    {
        StartCoroutine(FadeCo(_onFinished, _speed, _delay , _onMiddleReached));
    }

    IEnumerator FadeCo (System.Action _onFinished, float _speed, float _delay, System.Action _onMiddleReached = null)
    {
        float a = 0;

        while(a < 1)
        {
			fadeImage.color = Color.Lerp(Color.clear, Color.black, a);
            a += _speed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(_delay / 2);

        if (_onMiddleReached != null)
            _onMiddleReached.Invoke();

        yield return new WaitForSeconds(_delay / 2);

        a = 0;

        while (a < 1)
        {
			fadeImage.color = Color.Lerp(Color.black, Color.clear, a);
            a += _speed * Time.deltaTime;
            yield return null;
        }

        a = 0;
        _onFinished.Invoke();
        yield return null;
    }
}
