using TMPro;
using UnityEngine;

public class PopUpUIHandler : MonoBehaviour
{
    public static PopUpUIHandler instance;
    [TextArea(0, 30)]
    [SerializeField] private string perishMessage;
    [TextArea(0, 30)]
    [SerializeField] private string returnMessage;

    [SerializeField] private TMP_Text text;

    private void Start()
    {
        instance = GetComponent<PopUpUIHandler>();
    }

    public void PerishedMessage(string beeName)
    {
        text.text = beeName+" "+perishMessage;
    }

    public void ReturnedMessage(string beeName)
    {
        text.text = beeName+" "+returnMessage;
    }

    public void ClosePopUp()
    {
        Destroy(gameObject);
    }
}
