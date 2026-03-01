using UnityEngine;

public class PopUpSpawner : MonoBehaviour
{
    public static PopUpSpawner instance;
    [SerializeField] private GameObject popUp;
    [SerializeField] private Transform popUpContainer;

    private void Start()
    {
        instance = GetComponent<PopUpSpawner>();
    }

    public void SpawnMissionResult(BeeSquad.MissionResult result, string beeName)
    {
        if (result == BeeSquad.MissionResult.Success)
        {
            Instantiate(popUp, popUpContainer).GetComponent<PopUpUIHandler>().ReturnedMessage(beeName);
        }
        else
        {
            Instantiate(popUp, popUpContainer).GetComponent<PopUpUIHandler>().PerishedMessage(beeName);
        }
            
    }
}
