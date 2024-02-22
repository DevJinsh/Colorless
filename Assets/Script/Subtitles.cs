using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Subtitles : MonoBehaviour
{
    private static Subtitles subtitle;
    private IEnumerator coroutine;
    static IEnumerator a = null;
    private void Awake() => subtitle = this;

    static Text Subtext;
    Text SubT;
    void Start()
    {
        subtitle.GetComponent<Text>().enabled = false;
        Subtext = subtitle.GetComponent<Text>();
    }

    public static void Load_Subtitles(string sub)
    {
        if (a != null)
        {            
            subtitle.StopCoroutine(a);
        }
        Subtext.text = sub;
        Subtext.color = Color.white;
        Subtext.enabled = true;
        a = FadeOut();
        subtitle.StartCoroutine(a);
    }
    static IEnumerator FadeOut()
    {
        float duration = 1.5f; // 1.5secs
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            Subtext.color = new Color(Subtext.color.r, Subtext.color.g, Subtext.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        Subtext.enabled = false;
        yield break;
    }
}
