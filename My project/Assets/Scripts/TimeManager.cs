using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public int CurrentHour;
    public int FinalHour;

    //  counts down, when reaches zero, initiate the bear
    public int DaysLeft;
    //  when the bear shows up
    //  slowly approaching bear
    public int TotalDays;

    private void Start()
    {
        instance = GetComponent<TimeManager>();
        DaysLeft = TotalDays;
    }

    public void PassTime()
    {
        CurrentHour++;

        if (CurrentHour == FinalHour)
        {
            CurrentHour = 0;
            DaysLeft--;
        }

        for (int i=0; i<MissionSelector.instance.DeployedBeeSquads.Count; i++)
        {
            if(MissionSelector.instance.DeployedBeeSquads[i].missionStatus == null)
            {
                continue;
            }

            MissionSelector.instance.DeployedBeeSquads[i].missionStatus.HoursUntilComplete--;

            if (MissionSelector.instance.DeployedBeeSquads[i].missionStatus.HoursUntilComplete == 0)
            {
                MissionSelector.instance.DeployedBeeSquads[i].MissionCheck();
                MissionSelector.instance.DeployedBeeSquads.RemoveAt(i);
            }
        }
    }
}
