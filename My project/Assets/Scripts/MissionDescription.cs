using TMPro;
using UnityEngine;

public class MissionDescription : MonoBehaviour
{
    public static MissionDescription instance;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public GameObject[] MissionLog;
    public int logIndex;

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

    public void setLog(int i)
    {
        
    }

    public void clearLog()
    {
        for (int i=0; i<MissionLog.Length; i++)
        {

        }
    }
}
