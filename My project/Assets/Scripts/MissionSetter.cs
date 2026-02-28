using System.Collections.Generic;
using UnityEngine;

public class MissionSetter : MonoBehaviour
{

    private TimeManager time;
    private MissionSelector missionSelector;

    public List<Mission> Day1MissionPool;
    public List<Mission> Day2MissionPool;
    public List<Mission> Day3MissionPool;
    public Transform[] Nodes;

    private void Start()
    {
        time = GetComponent<TimeManager>();
        for (int i=0;i<3;i++)
        {
            PopMission(Day1MissionPool);
        }
    }

    public void PopMission(List<Mission> missionPool)
    {
        int randomMission = Random.Range(0, missionPool.Count);
        SetMission(missionPool[randomMission]);
        missionPool.RemoveAt(randomMission);
    }

    public void SetMission(Mission mission)
    {
        switch (mission.Location)
        {
            case MissionNode.Location.Forest:
                checkOccupied(Nodes[(int)MissionNode.Location.Forest], mission);
                break;

            case MissionNode.Location.Meadow:
                checkOccupied(Nodes[(int)MissionNode.Location.Meadow], mission);
                break;

            case MissionNode.Location.Pond:
                checkOccupied(Nodes[(int)MissionNode.Location.Pond], mission);
                break;

            case MissionNode.Location.Picnic:
                checkOccupied(Nodes[(int)MissionNode.Location.Picnic], mission);
                break;
        }
    }

    public void checkOccupied(Transform node, Mission mission)
    {
        for (int i=0; i<node.childCount; i++)
        {
            MissionNode child = node.GetChild(i).GetComponent<MissionNode>();
            if (!child.gameObject.activeSelf)
            {
                child.mission = mission;
                child.gameObject.SetActive(true);
                return;
            }
        }
    }
}
