using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private int currentHour;
    [SerializeField] private int finalHour;

    //  counts down, when reaches zero, initiate the bear
    [SerializeField] private int daysLeft;
    //  when the bear shows up
    //  slowly approaching bear
    [SerializeField] private int totalDays;

    public void PassTime()
    {
        currentHour++;

        if (currentHour == finalHour)
        {
            currentHour = 0;
            daysLeft--;
        }
    }
}
