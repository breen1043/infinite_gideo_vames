using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private int currentHour;
    [SerializeField] private int finalHour;

    [SerializeField] private int currentDay;
    [SerializeField] private int finalDay;

    public void PassTime()
    {
        currentHour++;

        if (currentHour == finalHour)
        {
            currentHour = 0;
            currentDay++;
        }
    }
}
