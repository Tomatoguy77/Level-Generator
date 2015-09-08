using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameInit : MonoBehaviour
{
    [Header("De scene die geladen zal worden")]
    public string m_sceneToLoad = "Main";
    [Header("Link het game object met het logo image hier")]
    public Image m_logoImage;
    [Header("Hoe lang moeten we wachten tot we gaan faden")]
    public float m_waitTime = 2f;
    [Header("Hoe snel moet het logo uit faden")]
    public float m_fadeSpeed = 0.5f;

    public void Awake()
    {
#if UNITY_EDITOR
        Application.LoadLevel(m_sceneToLoad);
#else
        startFadeIn();
#endif
    }

    private void startFadeIn()
    {
        StartCoroutine(this.Hideintro(() => { Application.LoadLevel(m_sceneToLoad); }));//start hide intro daarna maak callback naar scene laden
    }

    public IEnumerator Hideintro(Action _callback)
    {
        if (_callback != null)//check of we call back hebben
        {
            if (m_logoImage != null)//check of we logo hebben
            {
                yield return new WaitForSeconds(this.m_waitTime);//wacht tot we intro gaan faden
                float i = 1f;//alpha value
                while (m_logoImage.GetComponent<Image>().color.a > 0f)//loop tot we het plaatje hebben uit gefade
                {
                    i -= m_fadeSpeed*Time.deltaTime;
                    m_logoImage.GetComponent<Image>().color = (new Color(1, 1, 1, i));//verander de alpha van het plaatje
                    yield return null;
                }
                m_logoImage.GetComponent<Image>().color = (new Color(1, 1, 1, 0));//Zet de alpha op 0 zo dat het niet een negatief word
                _callback();
            }
            else
            {
                Debug.LogError("logoImage null");
            }
        }
        else
        {
            Debug.LogError("callback null");
        }
        yield return null;
    }

}
