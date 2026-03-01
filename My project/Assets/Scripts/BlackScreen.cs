using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    private Image img;
    [SerializeField] private float fadeTime;
    public TMP_Text daysRemain;

    private void Start()
    {
        img = GetComponent<Image>();
        img.canvasRenderer.SetAlpha(0f);
    }

    public void LevelUpScreen()
    {

    }

    public void NextDay()
    {
        img.canvasRenderer.SetAlpha(1);
        img.CrossFadeAlpha(0, 2, false);
        StartCoroutine(FadeAway());
        transform.GetChild(0).gameObject.SetActive(false);
    }

    WaitForSeconds sec = new WaitForSeconds(2f);
    private IEnumerator FadeAway()
    {
        yield return sec;
        gameObject.SetActive(false);
    }
}
