using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeText : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(FadeTextToFullAlpha(13f, GetComponent<Text>()));
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        yield return new WaitForSeconds(3f);

        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
    }
}
