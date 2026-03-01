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

    public bool BearFight;

    private void Start()
    {
        instance = GetComponent<TimeManager>();
        DaysLeft = TotalDays;
    }

    public void PassTime()
    {
        CurrentHour++;

        if (CurrentHour > FinalHour)
        {
            CurrentHour = 0;
            DaysLeft--;

            //  if squad does not come back before nightfall, flat 40% survival rate
            for (int i = 0; i < MissionSelector.instance.DeployedBeeSquads.Count; i++)
            {
                if (MissionSelector.instance.DeployedBeeSquads[i].missionStatus == null)
                {
                    continue;
                }

                MissionSelector.instance.DeployedBeeSquads[i].missionStatus.HoursUntilComplete = 0;

                Debug.Log(MissionSelector.instance.DeployedBeeSquads[i].missionStatus.Mission.name);

                float rng = Random.Range(0, 10);

                MissionSelector.instance.DeployedBeeSquads[i].missionStatus.status = rng > 5 ? MissionStatus.Status.complete : MissionStatus.Status.failed;

                if (rng > 5)
                {
                    MissionSelector.instance.DeployedBeeSquads[i].missionStatus.status = MissionStatus.Status.complete;
                    MissionSelector.instance.DeployedBeeSquads[i].Available = true;

                }
                else
                {
                    MissionSelector.instance.DeployedBeeSquads[i].missionStatus.status = MissionStatus.Status.failed;
                    MissionSelector.instance.BeeSquadGraveyard.Add(MissionSelector.instance.DeployedBeeSquads[i]);
                    MissionSelector.instance.BeeSquadUnits.Remove(MissionSelector.instance.DeployedBeeSquads[i]);
                    Debug.Log("ded");

                }
                MissionSelector.instance.DeployedBeeSquads[i].missionStatus = null;
                MissionSelector.instance.DeployedBeeSquads.RemoveAt(i);
            }

            if (DaysLeft == 0)
            {
                BearFight = true;
            }

            return;
        }

        for (int i=0; i<MissionSelector.instance.DeployedBeeSquads.Count; i++)
        {
            if(MissionSelector.instance.DeployedBeeSquads[i].missionStatus == null)
            {
                continue;
            }

            MissionSelector.instance.DeployedBeeSquads[i].missionStatus.HoursUntilComplete--;

            if (MissionSelector.instance.DeployedBeeSquads[i].missionStatus.HoursUntilComplete <= 0)
            {
                MissionSelector.instance.DeployedBeeSquads[i].MissionCheck();
                MissionSelector.instance.DeployedBeeSquads.RemoveAt(i);
            }
        }
    }
}
