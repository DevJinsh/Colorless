using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    private static Title title;

    private void Awake() => title = this;

    static Text titletext;
    public static void Load_Title()
    {
        titletext = title.GetComponent<Text>();
        titletext.text = Scene.Active_Scene;
        FadeOut_Text();
    }        
    public static void FadeOut_Text()
    {
        title.StartCoroutine(FadeOut());
    }

    static IEnumerator FadeOut()
    {
        float duration = 2f; //0.5 secs
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            titletext.color = new Color(titletext.color.r, titletext.color.g, titletext.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
}
