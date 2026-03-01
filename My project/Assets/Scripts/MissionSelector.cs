using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] private GameObject lvlUpButton;

    [Header("Mission Lists")]
    public List<MissionStatus> MissionLog;
    //  for the selected mission
    public MissionNode selectedMission;

    [Header("Stat Bars")]

    [SerializeField] private List<Image> statBars;
    [SerializeField] private Image pollen_counter;
    [SerializeField] private GameObject UpgradePanel;
    [SerializeField] private List<GameObject> UpgradeButtons;

    [Space(5)]
    public List<BeeSquad> BeeSquadUnits;
    public List<BeeSquad> DeployedBeeSquads;
    //  not sure if this needs to be a list, but we would have access to names of the dead
    public List<BeeSquad> BeeSquadGraveyard;
    //  for the selected member
    public int beeSquadIndex;
    public int pollen;
    public int lvl_points;

    //  view mode: false = looking at map, true = selecting units
    public bool SquadSelect;

    private void Start()
    {
        cam = Camera.main;
        instance = GetComponent<MissionSelector>();
        lvl_points = 0;
        SetStatBars();
    }

    //  send you bees out to DIE
    public void Deploy()
    {
        if (!selectedMission)
        {
            return;
        }

        TimeManager.instance.PassTime();
        MissionStatus newListItem = new MissionStatus();
        newListItem.Mission = selectedMission.mission;
        newListItem.assignedBeeSquad = BeeSquadUnits[beeSquadIndex];

        BeeSquadUnits[beeSquadIndex].Available = false;
        BeeSquadUnits[beeSquadIndex].HoursUnilArrival = selectedMission.mission.duration;
        DeployedBeeSquads.Add(BeeSquadUnits[beeSquadIndex]);

        newListItem.status = MissionStatus.Status.inProgress;
        MissionLog.Add(newListItem);
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

        if (!BeeSquadUnits[beeSquadIndex].Available)
        {
            beeSquadIndex--;
        }

        

        BeeSquadUnits[beeSquadIndex].gameObject.SetActive(true);
        SetStatBars();
    }

    public void BeeNextIndex()
    {
        BeeSquadUnits[beeSquadIndex].gameObject.SetActive(false);

        beeSquadIndex++;
        
        if(beeSquadIndex >= BeeSquadUnits.Count)
        {
            beeSquadIndex = 0;
        }

        if (!BeeSquadUnits[beeSquadIndex].Available)
        {
            beeSquadIndex++;
        }

        

        BeeSquadUnits[beeSquadIndex].gameObject.SetActive(true);
        SetStatBars();
    }

    /*public int[] getStats(int i) {
        return new int[] {
        BeeSquadUnits[i].SquadStats.FlightSpeed,
        BeeSquadUnits[i].SquadStats.DANCE,
        BeeSquadUnits[i].SquadStats.Sharpness,
        BeeSquadUnits[i].SquadStats.Hivemind};
    }*/

    public void SetStatBars() {
        /*stat1.fillAmount = Mathf.Clamp01(1);
        stat2.fillAmount = Mathf.Clamp01(0);
        stat3.fillAmount = Mathf.Clamp01(2);
        stat4.fillAmount = Mathf.Clamp01(0.7f);
        */

        statBars[0].fillAmount = Mathf.Clamp01((BeeSquadUnits[beeSquadIndex].SquadStats.FlightSpeed-1)/7.0f);
        statBars[1].fillAmount = Mathf.Clamp01((BeeSquadUnits[beeSquadIndex].SquadStats.DANCE-1)/7.0f);
        statBars[2].fillAmount = Mathf.Clamp01((BeeSquadUnits[beeSquadIndex].SquadStats.Sharpness-1)/7.0f);
        statBars[3].fillAmount = Mathf.Clamp01((BeeSquadUnits[beeSquadIndex].SquadStats.Hivemind-1)/7.0f);
        if (BeeSquadUnits[beeSquadIndex].Level < 8) {
            lvlUpButton.GetComponentInChildren<TextMeshProUGUI>().text = "Level Up?\n("+BeeSquadUnits[beeSquadIndex].Level*50+" pollen) ↑"+BeeSquadUnits[beeSquadIndex].Level;
        } else {
            lvlUpButton.GetComponentInChildren<TextMeshProUGUI>().text = "Level Maxed";
        }
        pollen_counter.GetComponentInChildren<TextMeshProUGUI>().text = "Pollen: "+pollen;
        //Debug.Log(UpgradePanel.GetComponentInChildren<TextMeshProUGUI>());
        UpgradePanel.GetComponentInChildren<TextMeshProUGUI>().text = "ALLOCATE POINTS (+"+BeeSquadUnits[beeSquadIndex].points+")";

        if (BeeSquadUnits[beeSquadIndex].points <= 0) {
            UpgradePanel.SetActive(false);
        } else {
            UpgradePanel.SetActive(true);
        }
    }

    public void lvlUp() {
        if (pollen >= BeeSquadUnits[beeSquadIndex].Level*50 && BeeSquadUnits[beeSquadIndex].Level < 8) {
            pollen -= BeeSquadUnits[beeSquadIndex].Level*50;
            //Debug.Log("Leveling Up Squad:" + beeSquadIndex +", -"+BeeSquadUnits[beeSquadIndex].Level*50+" pollen ("+pollen+"), +"+BeeSquadUnits[beeSquadIndex].Level+" points");
            
            
            BeeSquadUnits[beeSquadIndex].points+=BeeSquadUnits[beeSquadIndex].Level;
            BeeSquadUnits[beeSquadIndex].Level += 1;
            

            UpgradePanel.SetActive(true);
        } else {
            Debug.Log("Lvlup registered and failed");
        }
        SetStatBars();
    }

    public void statLvlUp(int i) {
        int temp = 0;
        switch (i) {
            case 1:
                temp = BeeSquadUnits[beeSquadIndex].SquadStats.FlightSpeed;
                break;
            case 2:
                temp = BeeSquadUnits[beeSquadIndex].SquadStats.DANCE;
                break;
            case 3:
                temp = BeeSquadUnits[beeSquadIndex].SquadStats.Sharpness;
                break;
            case 4:
                temp = BeeSquadUnits[beeSquadIndex].SquadStats.Hivemind;
                break;
        }
        if (temp < 8) {
            temp += 1;
            BeeSquadUnits[beeSquadIndex].points -= 1;
            if (temp >= 8) {
                UpgradeButtons[i-1].GetComponent<Image>().color = new Color(0.4f, 0.2f, 0.5f);
            }
        } else {
            Debug.Log("Overleveled, lvlup failed");
        }
        switch (i) {
            case 1:
                BeeSquadUnits[beeSquadIndex].SquadStats.FlightSpeed = temp;
                break;
            case 2:
                BeeSquadUnits[beeSquadIndex].SquadStats.DANCE = temp;
                break;
            case 3:
                BeeSquadUnits[beeSquadIndex].SquadStats.Sharpness = temp;
                break;
            case 4:
                BeeSquadUnits[beeSquadIndex].SquadStats.Hivemind = temp;
                break;
        }
        
        
        SetStatBars();
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
