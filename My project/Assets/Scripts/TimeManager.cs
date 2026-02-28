using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int CurrentHour;
    public int FinalHour;

    //  counts down, when reaches zero, initiate the bear
    public int DaysLeft;
    //  when the bear shows up
    //  slowly approaching bear
    public int TotalDays;

    public void PassTime()
    {
        CurrentHour++;

        if (CurrentHour == FinalHour)
        {
            CurrentHour = 0;
            DaysLeft--;
        }
    }
}
