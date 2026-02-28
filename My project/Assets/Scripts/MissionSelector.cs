using System.Collections;
using UnityEngine;

public class MissionSelector : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Vector3 tableAngle;
    [SerializeField] private Vector3 squadAngle;
    [SerializeField] private float camRotationSpeed;

    [SerializeField] private bool SquadSelect;

    //  probably will replace this with a mission component or scriptable object
    private GameObject mission;

    private void Start()
    {
        cam = Camera.main;
    }

    public void SquadAngle()
    {
        //Debug.Log("CAM Up to " + squadAngle);
        SquadSelect = true;
        StartCoroutine(CameraRotate(squadAngle));
    }

    public void TableAngle()
    {
        //Debug.Log("CAM Table to" + tableAngle + " | " + Quaternion.Euler(tableAngle));
        SquadSelect = false;
        StartCoroutine(CameraRotate(tableAngle));
    }

    private IEnumerator CameraRotate(Vector3 angle)
    {
        Quaternion qangle = Quaternion.Euler(angle);
        while (Quaternion.Angle(cam.transform.rotation, qangle) > 0.001 )
        {
            //Debug.Log(angle +" "+cam.transform.rotation);
            cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, qangle, Time.deltaTime * camRotationSpeed);
            //Debug.Log("rotating..." + cam.transform.rotation + " | " + qangle + " || " + Quaternion.Angle(cam.transform.rotation, qangle));
            yield return null;
        }
        //Debug.Log("loop end");
        StopAllCoroutines();
    }
}
