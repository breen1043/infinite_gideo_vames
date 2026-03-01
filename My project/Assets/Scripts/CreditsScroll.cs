using System.Numerics;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] public float scrollSpeed = 40f;

    private RectTransform rectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rectTransform =  GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition += new UnityEngine.Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
