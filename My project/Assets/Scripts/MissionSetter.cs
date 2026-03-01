using System.Collections.Generic;
using UnityEngine;

public class MissionSetter : MonoBehaviour
{
    public static MissionSetter instance;

    public List<Mission> Day1MissionPool;
    public List<Mission> Day2MissionPool;
    public List<Mission> Day3MissionPool;
    public Transform[] Nodes;

    private void Start()
    {
        instance = GetComponent<MissionSetter>();
        for (int i=0;i<3;i++)
        {
            PopMission(Day1MissionPool);
        }
    }

    public void PopMission(List<Mission> missionPool)
    {
        if (missionPool.Count < 1)
        {
            return;
        }
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

    public void ClearMissions()
    {
        for (int i=0; i<Nodes.Length; i++)
        {
            for (int j=0; j < Nodes[i].childCount; j++)
            {
                Transform child = Nodes[i].GetChild(j);
                child.GetComponent<MissionNode>().mission = null;
                child.gameObject.SetActive(false);
            }
        }

        List<Mission> missionPool = Day1MissionPool;

        switch (TimeManager.instance.DaysLeft)
        {
            case 2:
                missionPool = Day2MissionPool;
                break;
            case 1:
                missionPool = Day3MissionPool;
                break;
        }

        for(int i=0; i<3; i++)
        {
            PopMission(missionPool);
        }
    }
}
