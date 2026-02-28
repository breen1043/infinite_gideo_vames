using System.Collections;
using UnityEngine;

public class MissionSelector : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Quaternion tableAngle;
    [SerializeField] private Quaternion squadAngle;
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
        SquadSelect = true;
        StartCoroutine(CameraRotate(squadAngle));
    }

    public void TableAngle()
    {
        SquadSelect = false;
        StartCoroutine(CameraRotate(tableAngle));
    }

    private IEnumerator CameraRotate(Quaternion angle)
    {
        while (cam.transform.rotation != angle)
        {
            //Debug.Log(angle +" "+cam.transform.rotation);
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, angle, Time.deltaTime * camRotationSpeed);
            yield return null;
        }
        StopAllCoroutines();
    }
}
