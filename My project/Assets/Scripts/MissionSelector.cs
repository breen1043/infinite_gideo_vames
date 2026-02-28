using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSelector : MonoBehaviour
{
    private TimeManager timeManager;

    [Header("Camera Movement")]
    private Camera cam;
    [SerializeField] private Vector3 tableAngle;
    [SerializeField] private Vector3 squadAngle;
    [SerializeField] private float camRotationSpeed;

    [Header("Buttons")]
    [SerializeField] private GameObject upButton;
    [SerializeField] private GameObject downButton;

    [Header("Mission Lists")]
    [SerializeField] private List<Mission> missionPool;
    public List<Mission> AvailableMissions;
    //  for the selected mission
    private int missionIndex;

    [Space(5)]
    public List<BeeSquad> BeeSquadUnits;
    //  not sure if this needs to be a list, but we would have access to names of the dead
    public List<BeeSquad> BeeSquadGraveyard;
    //  for the selected member
    private int beeSquadIndex;

    //  view mode: false = looking at map, true = selecting units
    private bool SquadSelect;

    private void Start()
    {
        cam = Camera.main;
        //  assuming we're stuffing all the scripts on one empty object
        timeManager = GetComponent<TimeManager>();
    }

    //  cycle through bee squad members
    public void BeePrevIndex()
    {
        beeSquadIndex--;

        if (!BeeSquadUnits[beeSquadIndex].Available)
        {
            beeSquadIndex--;
        }

        if (beeSquadIndex < 0)
        {
            beeSquadIndex = BeeSquadUnits.Count - 1;
        }
    }

    public void BeeNextIndex()
    {
        beeSquadIndex++;

        if (!BeeSquadUnits[beeSquadIndex].Available)
        {
            beeSquadIndex++;
        }

        if(beeSquadIndex >= BeeSquadUnits.Count)
        {
            beeSquadIndex = 0;
        }
    }

    //  look at squad menu
    public void SquadAngle()
    {
        //Debug.Log("CAM Up to " + squadAngle);
        SquadSelect = true;
        StartCoroutine(CameraRotate(squadAngle, upButton, downButton));
    }

    //  look at mission menu
    public void TableAngle()
    {
        //Debug.Log("CAM Table to" + tableAngle + " | " + Quaternion.Euler(tableAngle));
        SquadSelect = false;
        StartCoroutine(CameraRotate(tableAngle, downButton, upButton));
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
