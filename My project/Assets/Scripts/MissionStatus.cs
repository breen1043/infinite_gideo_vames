using UnityEngine;

public class MissionStatus : MonoBehaviour
{
    public Mission Mission;

    public enum Status
    {
        inProgress,
        complete,
        failed
    }

    public Status status;

    public BeeSquad assignedBeeSquad;

    public PollenHandler.PollenAmount Pollen;
}
