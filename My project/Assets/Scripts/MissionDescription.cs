using TMPro;
using UnityEngine;

public class MissionDescription : MonoBehaviour
{
    public static MissionDescription instance;
    public TMP_Text titleText;
    public TMP_Text descriptionText;

    private void Start()
    {
        instance = GetComponent<MissionDescription>();
    }

    public void SetDescription(string title, string desc)
    {
        titleText.text = title;
        descriptionText.text = desc;
    }

    public void ClearDescription()
    {
        titleText.text = "";
        descriptionText.text = "";
    }
}
