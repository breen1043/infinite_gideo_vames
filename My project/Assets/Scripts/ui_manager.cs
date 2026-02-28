using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject panel;

    public void ClosePanel()
    {
        Debug.Log("closed quest");
        panel.SetActive(false);
    }
}
