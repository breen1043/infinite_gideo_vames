using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSelector : MonoBehaviour
{
    public static MissionSelector instance;

    [Header("Camera Movement")]
    private Camera cam;
    [SerializeField] private Vector3 tableAngle;
    [SerializeField] private Vector3 squadAngle;
    [SerializeField] private float camRotationSpeed;

    [Header("Buttons")]
    [SerializeField] private GameObject warTableUI;
    [SerializeField] private GameObject squadSelectUI;

    [Header("Mission Lists")]
    public List<MissionStatus> MissionLog;
    //  for the selected mission
    public MissionNode selectedMission;

    [Space(5)]
    public List<BeeSquad> BeeSquadUnits;
    public List<BeeSquad> DeployedBeeSquads;
    //  not sure if this needs to be a list, but we would have access to names of the dead
    public List<BeeSquad> BeeSquadGraveyard;
    //  for the selected member
    public int beeSquadIndex;

    //  view mode: false = looking at map, true = selecting units
    public bool SquadSelect;

    private void Start()
    {
        cam = Camera.main;
        instance = GetComponent<MissionSelector>();
    }

    //  send you bees out to DIE
    public void Deploy()
    {
        if (!selectedMission || BeeSquadUnits.Count <= DeployedBeeSquads.Count)
        {
            return;
        }

        TimeManager.instance.PassTime();
        MissionStatus newListItem = new MissionStatus();
        newListItem.Mission = selectedMission.mission;
        newListItem.HoursUntilComplete = newListItem.Mission.duration;

        if (!selectedMission.mission.Honey)
        {
            newListItem.assignedBeeSquad = BeeSquadUnits[beeSquadIndex];

            BeeSquadUnits[beeSquadIndex].Available = false;
            //  BeeSquadUnits[beeSquadIndex].HoursUnilArrival = selectedMission.mission.duration;
            BeeSquadUnits[beeSquadIndex].missionStatus = newListItem;
            DeployedBeeSquads.Add(BeeSquadUnits[beeSquadIndex]);

            BeeNextIndex();
        }

        newListItem.status = !selectedMission.mission.Honey ? MissionStatus.Status.inProgress : MissionStatus.Status.complete;
        MissionLog.Add(newListItem);
        if (!selectedMission.mission.Honey)
        {
            selectedMission.gameObject.SetActive(false);
            switch (TimeManager.instance.DaysLeft)
            {
                case 3:
                    MissionSetter.instance.PopMission(MissionSetter.instance.Day1MissionPool);
                    break;
                case 2:
                    MissionSetter.instance.PopMission(MissionSetter.instance.Day2MissionPool);
                    break;
                case 1:
                    MissionSetter.instance.PopMission(MissionSetter.instance.Day3MissionPool);
                    break;
            }
        }

        selectedMission = null;

        

        Debug.Log("deployed");
    }

    //  cycle through bee squad members
    public void BeePrevIndex()
    {
        BeeSquadUnits[beeSquadIndex].gameObject.SetActive(false);

        beeSquadIndex--;

        if (beeSquadIndex < 0)
        {
            beeSquadIndex = BeeSquadUnits.Count - 1;
        }

        if (BeeSquadUnits.Count > DeployedBeeSquads.Count && !BeeSquadUnits[beeSquadIndex].Available)
        {
            BeePrevIndex();
        }

        if (BeeSquadUnits.Count > DeployedBeeSquads.Count)
        {
            BeeSquadUnits[beeSquadIndex].gameObject.SetActive(true);
        }
    }

    public void BeeNextIndex()
    {
        BeeSquadUnits[beeSquadIndex].gameObject.SetActive(false);

        beeSquadIndex++;

        if(beeSquadIndex >= BeeSquadUnits.Count)
        {
            beeSquadIndex = 0;
        }

        if (BeeSquadUnits.Count > DeployedBeeSquads.Count && !BeeSquadUnits[beeSquadIndex].Available)
        {
            BeeNextIndex();
        }

        if (BeeSquadUnits.Count > DeployedBeeSquads.Count)
        {
            BeeSquadUnits[beeSquadIndex].gameObject.SetActive(true);
        }

    }

    //  look at squad menu
    public void SquadAngle()
    {
        //Debug.Log("CAM Up to " + squadAngle);
        SquadSelect = true;
        StartCoroutine(CameraRotate(squadAngle, warTableUI, squadSelectUI));
    }

    //  look at mission menu
    public void TableAngle()
    {
        //Debug.Log("CAM Table to" + tableAngle + " | " + Quaternion.Euler(tableAngle));
        StartCoroutine(CameraRotate(tableAngle, squadSelectUI, warTableUI));
        SquadSelect = false;
    }

    private IEnumerator CameraRotate(Vector3 angle, GameObject buttonClicked, GameObject buttonAppearing)
    {
        buttonClicked.SetActive(false);
        Quaternion qangle = Quaternion.Euler(angle);

        while (Quaternion.Angle(cam.transform.rotation, qangle) > 0.001 )
        {
            //Debug.Log(angle +" "+cam.transform.rotation);
            cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, qangle, Time.deltaTime * camRotationSpeed);
            //Debug.Log("rotating..." + cam.transform.rotation + " | " + qangle + " || " + Quaternion.Angle(cam.transform.rotation, qangle));
            yield return null;
        }
        //Debug.Log("loop end");

        buttonAppearing.SetActive(true);
        StopAllCoroutines();
    }
}
