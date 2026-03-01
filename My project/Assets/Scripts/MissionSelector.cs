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
    [SerializeField] private GameObject newBeeCanvas;

    [Header("Mission Lists")]
    public List<MissionStatus> MissionLog;
    //  for the selected mission
    public MissionNode selectedMission;
    [SerializeField] private TMP_InputField beeNamer;

    [Header("Stat Bars")]

    [SerializeField] private List<Image> statUpgradeBars;
    [SerializeField] private List<Image> statDeployBars;
    [SerializeField] private Image pollen_counter;
    [SerializeField] private GameObject UpgradePanel;
    [SerializeField] private GameObject DeployPanel;
    [SerializeField] private List<GameObject> UpgradeButtons;
    [SerializeField] private GameObject DisplayCamera;
    [SerializeField] private float orbitSpeed;
    [SerializeField] private float bobAmplitude;
    [SerializeField] private float bobFrequency;
    
    


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
    public bool Rotating;

    private void Start()
    {
        cam = Camera.main;
        instance = GetComponent<MissionSelector>();
        lvl_points = 0;
        SetStatBars();
        //beeNamer.onValueChanged.AddListener(NameUpdate);
        //StartCoroutine(CameraOrbitForever());
        beeNamer.characterLimit = 12;
    
    }

    //  send you bees out to DIE
    public void Deploy()
    {
        if (TimeManager.instance.BearFight)
        {
            BearFightDeploy();
            return;
        }

        if (!(beeSquadIndex < BeeSquadUnits.Count && beeSquadIndex >= 0))
        {
            return;
        }

        if (!selectedMission || (BeeSquadUnits.Count <= DeployedBeeSquads.Count && !selectedMission.mission.Honey))
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

    public void BearFightDeploy()
    {

    }

    //  cycle through bee squad members
    public void BeePrevIndex()
    {
        if (beeSquadIndex < BeeSquadUnits.Count && beeSquadIndex >= 0)
        {
            BeeSquadUnits[beeSquadIndex].gameObject.SetActive(false);
        }

        beeSquadIndex--;
        
        if (beeSquadIndex < 0)
        {
            beeSquadIndex = BeeSquadUnits.Count - 1;
        }

        if (beeSquadIndex < 0)
        {
            beeSquadIndex = BeeSquadUnits.Count - 1;
        }

        if (BeeSquadUnits.Count > DeployedBeeSquads.Count && !BeeSquadUnits[beeSquadIndex].Available)
        {
            for (int i=0; i <= BeeSquadUnits.Count; i++)
            {
                BeePrevIndex();
            }
            
        }

        if (BeeSquadUnits.Count > DeployedBeeSquads.Count)
        {
            BeeSquadUnits[beeSquadIndex].gameObject.SetActive(true);
        }
        SetStatBars();
    }

    public void BeeNextIndex()
    {
        if (beeSquadIndex < BeeSquadUnits.Count && beeSquadIndex >= 0)
        {
            BeeSquadUnits[beeSquadIndex].gameObject.SetActive(false);
        }

        beeSquadIndex++;
        
        if(beeSquadIndex >= BeeSquadUnits.Count)
        {
            beeSquadIndex = 0;
        }

        if(beeSquadIndex >= BeeSquadUnits.Count)
        {
            beeSquadIndex = 0;
        }

        if (BeeSquadUnits.Count > DeployedBeeSquads.Count && !BeeSquadUnits[beeSquadIndex].Available)
        {
            for (int i = 0; i <= BeeSquadUnits.Count; i++)
            {
                BeeNextIndex();
            }
        }

        if (BeeSquadUnits.Count > DeployedBeeSquads.Count)
        {
            BeeSquadUnits[beeSquadIndex].gameObject.SetActive(true);
        }
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

        if (statUpgradeBars.Count < 1)
        {
            return;
        }

        statUpgradeBars[0].fillAmount = Mathf.Clamp01((BeeSquadUnits[beeSquadIndex].SquadStats.FlightSpeed-1)/7.0f);
        statUpgradeBars[1].fillAmount = Mathf.Clamp01((BeeSquadUnits[beeSquadIndex].SquadStats.DANCE-1)/7.0f);
        statUpgradeBars[2].fillAmount = Mathf.Clamp01((BeeSquadUnits[beeSquadIndex].SquadStats.Sharpness-1)/7.0f);
        statUpgradeBars[3].fillAmount = Mathf.Clamp01((BeeSquadUnits[beeSquadIndex].SquadStats.Hivemind-1)/7.0f);
        statDeployBars[0].fillAmount = Mathf.Clamp01((BeeSquadUnits[beeSquadIndex].SquadStats.FlightSpeed-1)/7.0f);
        statDeployBars[1].fillAmount = Mathf.Clamp01((BeeSquadUnits[beeSquadIndex].SquadStats.DANCE-1)/7.0f);
        statDeployBars[2].fillAmount = Mathf.Clamp01((BeeSquadUnits[beeSquadIndex].SquadStats.Sharpness-1)/7.0f);
        statDeployBars[3].fillAmount = Mathf.Clamp01((BeeSquadUnits[beeSquadIndex].SquadStats.Hivemind-1)/7.0f);
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
        if (SquadSelect) {
            DeployPanel.SetActive(true);
        } else {
            DeployPanel.SetActive(false);
        }
        squadSelectUI.GetComponentInChildren<TextMeshProUGUI>().text = BeeSquadUnits[beeSquadIndex].squadname;

        for (int i=0; i<4; i++){
            if ((int)getBeeStat(i+1) < 8) {
                UpgradeButtons[i].GetComponent<Image>().color = new Color(1.0f, 0.788f, 0.0f);
            } else {
                UpgradeButtons[i].GetComponent<Image>().color = new Color(0.4f, 0.2f, 0.5f);
            }
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
        temp = (int)getBeeStat(i);
        if (temp < 8) {
            temp += 1;
            BeeSquadUnits[beeSquadIndex].points -= 1;
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

    public int getBeeStat(int i) {
        switch (i) {
            case 1:
                return (int)BeeSquadUnits[beeSquadIndex].SquadStats.FlightSpeed;
            case 2:
                return (int)BeeSquadUnits[beeSquadIndex].SquadStats.DANCE;
            case 3:
                return (int)BeeSquadUnits[beeSquadIndex].SquadStats.Sharpness;
            case 4:
                return (int)BeeSquadUnits[beeSquadIndex].SquadStats.Hivemind;
            default:
                return -1;
        }
    }

    public void CreateNewSquad() {
        GameObject obj = new GameObject("Bee Squad");
        BeeSquad newBeeSquad = obj.AddComponent<BeeSquad>();

        newBeeSquad.SquadStats.FlightSpeed = 0;
        newBeeSquad.SquadStats.DANCE = 0;
        newBeeSquad.SquadStats.Sharpness = 0;
        newBeeSquad.SquadStats.Hivemind = 0;
        newBeeSquad.Level=0;
        newBeeSquad.squadname = beeNamer.text;
        BeeSquadUnits.Add(newBeeSquad);

        newBeeCanvas.SetActive(false);
    }

    //  look at squad menu
    public void SquadAngle()
    {
        //Debug.Log("CAM Up to " + squadAngle);
        if (!Rotating)
        {
            Rotating = true;
            SquadSelect = true;
            StartCoroutine(CameraRotate(squadAngle, warTableUI, squadSelectUI));
        }
    }

    //  look at mission menu
    public void TableAngle()
    {
        //Debug.Log("CAM Table to" + tableAngle + " | " + Quaternion.Euler(tableAngle));
        if (!Rotating)
        {
            Rotating = true;
            SquadSelect = false;
            StartCoroutine(CameraRotate(tableAngle, squadSelectUI, warTableUI));
            
        }
    }

    private IEnumerator CameraRotate(Vector3 angle, GameObject buttonClicked, GameObject buttonAppearing)
    {
        buttonClicked.SetActive(false);
        Quaternion qangle = Quaternion.Euler(angle);

        if (!SquadSelect) {
            SetStatBars();
        }

        while (Quaternion.Angle(cam.transform.rotation, qangle) > 0.001 )
        {
            //Debug.Log(angle +" "+cam.transform.rotation);
            cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, qangle, Time.deltaTime * camRotationSpeed);
            //Debug.Log("rotating..." + cam.transform.rotation + " | " + qangle + " || " + Quaternion.Angle(cam.transform.rotation, qangle));
            yield return null;
        }
        //Debug.Log("loop end");
        if (SquadSelect) {
            SetStatBars();
        }

        buttonAppearing.SetActive(true);
        Rotating = false;
        StopAllCoroutines();
    }

    private IEnumerator CameraOrbitForever() { 
        Vector3 offset = new Vector3(0.0f,2.33f,-5.0f);
        while (true) { 
            offset = Quaternion.AngleAxis(orbitSpeed * Time.deltaTime, Vector3.up) * offset;
            DisplayCamera.transform.position = BeeSquadUnits[beeSquadIndex].gameObject.transform.position + offset;
            Vector3 direction = (BeeSquadUnits[beeSquadIndex].gameObject.transform.position - DisplayCamera.transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(direction);
            DisplayCamera.transform.rotation = Quaternion.Euler( 25f, lookRot.eulerAngles.y, 0f );

            float bob = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
            Vector3 pos = DisplayCamera.transform.position;
            pos.y += bob;
            DisplayCamera.transform.position = pos;
            
            yield return null;
        } 
    }

}
