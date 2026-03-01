using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private Image blackScreen;
    [SerializeField] private GameObject dayEndUI;

    private void Start()
    {
        instance = GetComponent<TimeManager>();
        DaysLeft = TotalDays + 1;
    }

    public void PassTime()
    {
        CurrentHour++;

        if (CurrentHour > FinalHour)
        {
            CurrentHour = 0;
            DaysLeft--;

            blackScreen.gameObject.SetActive(true);
            blackScreen.canvasRenderer.SetAlpha(0);
            blackScreen.CrossFadeAlpha(1f, 2, false);
            StartCoroutine(DayEndUI());

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

            if (BearFight)
            {
                if (MissionSelector.instance.DeployedBeeSquads[i].missionStatus.Mission.Honey)
                {
                    MissionSelector.instance.DeployedBeeSquads[i].SwarmAttack();
                }
                else
                {
                    MissionSelector.instance.DeployedBeeSquads[i].BearCheck();
                }
                MissionSelector.instance.DeployedBeeSquads.RemoveAt(i);
                return;
            }

            MissionSelector.instance.DeployedBeeSquads[i].missionStatus.HoursUntilComplete--;

            if (MissionSelector.instance.DeployedBeeSquads[i].missionStatus.HoursUntilComplete <= 0)
            {
                MissionSelector.instance.DeployedBeeSquads[i].MissionCheck();
                MissionSelector.instance.DeployedBeeSquads.RemoveAt(i);
            }
        }
    }

    WaitForSeconds sec = new WaitForSeconds(2f);
    private IEnumerator DayEndUI()
    {
        yield return sec;

        MissionSelector.instance.MissionLog.Clear();

        switch (DaysLeft)
        {
            case 3:
                blackScreen.gameObject.GetComponent<BlackScreen>().daysRemain.text = "Three days remain.";
                break;
            case 2:
                blackScreen.gameObject.GetComponent<BlackScreen>().daysRemain.text = "Two days remain.";
                break;
            case 1:
                blackScreen.gameObject.GetComponent<BlackScreen>().daysRemain.text = "One day remains.";
                break;
            case 0:
                blackScreen.gameObject.GetComponent<BlackScreen>().daysRemain.text = "The Bear arrives at dawn.";
                break;
        }
        dayEndUI.SetActive(true);
        MissionSetter.instance.ClearMissions();
    }
}
