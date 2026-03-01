using System;
using UnityEngine;

[Serializable]
public class MissionStatus
{
    public Mission Mission;

    public enum Status
    {
        inProgress,
        complete,
        failed
    }

    public Status status;

    public int HoursUntilComplete;

    public BeeSquad assignedBeeSquad;
}
