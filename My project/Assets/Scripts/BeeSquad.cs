using System;
using System.Reflection;
using UnityEngine;

public class BeeSquad : MonoBehaviour
{
    [Serializable]
    public struct Stats
    {
        public float FlightSpeed;
        public float DANCE;
        public float Sharpness;
        public float Hivemind;
    }

    public string squadname;
    public int Level;
    public Stats SquadStats;
    public int points;

    public bool Available = true;

    public MissionStatus missionStatus;

    public enum MissionResult
    {
        Success,
        Failure,
        NearMiss
    }

    /*
     *  note: not sure how to structure squads completely yet.
     */

    public MissionResult MissionCheck()
    {
        /*
        //  stat calculations and removal from list when killed
        
        int rng = UnityEngine.Random.Range(0, 2);

        if(rng == 0)
        {
            missionStatus.status = MissionStatus.Status.failed;
            MissionSelector.instance.BeeSquadGraveyard.Add(this);
            MissionSelector.instance.BeeSquadUnits.Remove(this);

        }
        else
        {
            missionStatus.status = MissionStatus.Status.complete;
            Available = true;
        }

        missionStatus = null;

        return rng == 0 ? MissionResult.Failure : MissionResult.Success;
        */

        float standardResults = 0;
        float weightedResults = 0;
        float successNumber = 0;

        switch (missionStatus.Mission.WeightedStat1)
        {
            // if one of the weighted stats is Flight Speed, and...
            case Mission.WeightedStat.FlightSpeed:
                switch (missionStatus.Mission.WeightedStat2)
                {
                    // the other weighted stat is DANCE
                    case Mission.WeightedStat.DANCE:
                        weightedResults = (1.5f * (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed)) + (1.5f * (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE));
                        standardResults = (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness) + (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind);
                        break;

                    // the other weighted stat is Sharpness
                    case Mission.WeightedStat.Sharpness:
                        weightedResults = (1.5f * (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed)) + (1.5f * (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness));
                        standardResults = (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE) + (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind);
                        break;

                    // the other weighted stat is Hivemind
                    case Mission.WeightedStat.Hivemind:
                        weightedResults = (1.5f * (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed)) + (1.5f * (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind));
                        standardResults = (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE) + (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness);
                        break;
                }
                break;
            // if one of the weighted stats is DANCE, and...
            case Mission.WeightedStat.DANCE:
                switch (missionStatus.Mission.WeightedStat2)
                {
                    // the other weighted stat is Flight Speed
                    case Mission.WeightedStat.FlightSpeed:
                        weightedResults = (1.5f * (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE)) + (1.5f * (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed));
                        standardResults = (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness) + (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind);
                        break;

                    // the other weighted stat is Sharpness
                    case Mission.WeightedStat.Sharpness:
                        weightedResults = (1.5f * (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE)) + (1.5f * (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness));
                        standardResults = (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed) + (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind);
                        break;

                    // the other weighted stat is Hivemind;
                    case Mission.WeightedStat.Hivemind:
                        weightedResults = (1.5f * (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE)) + (1.5f * (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind));
                        standardResults = (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed) + (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness);
                        break;
                }
                break;
            case Mission.WeightedStat.Sharpness:
                switch (missionStatus.Mission.WeightedStat2)
                {
                    // the other weighted stat is Flight Speed
                    case Mission.WeightedStat.FlightSpeed:
                        weightedResults = (1.5f * (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness)) + (1.5f * (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed));
                        standardResults = (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE) + (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind);
                        break;

                    // the other weighted stat is DANCE
                    case Mission.WeightedStat.DANCE:
                        weightedResults = (1.5f * (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness)) + (1.5f * (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE));
                        standardResults = (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed) + (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind);
                        break;

                    // the other weighted stat is Hivemind
                    case Mission.WeightedStat.Hivemind:
                        weightedResults = (1.5f * (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness)) + (1.5f * (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind));
                        standardResults = (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.DANCE) + (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE);
                        break;
                }
                break;
            case Mission.WeightedStat.Hivemind:
                switch (missionStatus.Mission.WeightedStat2)
                {
                    // the other weighted stat is Flight Speed
                    case Mission.WeightedStat.FlightSpeed:
                        weightedResults = (1.5f * (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind)) + (1.5f * (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed));
                        standardResults = (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE) + (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness);
                        break;

                    // the other weighted stat is DANCE
                    case Mission.WeightedStat.DANCE:
                        weightedResults = (1.5f * (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind)) + (1.5f * (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE));
                        standardResults = (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed) + (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness);
                        break;

                    // the other weighted stat is Sharpness
                    case Mission.WeightedStat.Sharpness:
                        weightedResults = (1.5f * (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind)) + (1.5f * (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness));
                        standardResults = (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed) + (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE);
                        break;
                }
                break;
        }

        successNumber = weightedResults + standardResults;

        if (successNumber < 0)
        {
            missionStatus.status = MissionStatus.Status.failed;
            MissionSelector.instance.BeeSquadGraveyard.Add(this);
            MissionSelector.instance.BeeSquadUnits.Remove(this);
            missionStatus = null;
            PopUpSpawner.instance.SpawnMissionResult(MissionResult.Failure, name);
            return MissionResult.Failure;
        }

        missionStatus.status = MissionStatus.Status.complete;
        Available = true;
        missionStatus = null;
        PopUpSpawner.instance.SpawnMissionResult(MissionResult.Success, name);
        return MissionResult.Success;
    }

    public MissionResult BearCheck()
    {
        float standardResults = 0;
        float weightedResults = 0;
        float successNumber = 0;

        switch (missionStatus.Mission.WeightedStat1)
        {
            case Mission.WeightedStat.FlightSpeed:
                weightedResults = (1.5f * (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed));
            standardResults = (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE) +
                    (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness) + (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind);
                break;
            case Mission.WeightedStat.DANCE:
                weightedResults = (1.5f * (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE));
            standardResults = (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed) +
                    (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness) + (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind);
                break;
            case Mission.WeightedStat.Sharpness:
                weightedResults = (1.5f * (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness));
            standardResults = (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed) +
                   (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE) + (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind);
                break;
            case Mission.WeightedStat.Hivemind:
                weightedResults = (1.5f * (SquadStats.Hivemind - missionStatus.Mission.TargetStats.Hivemind));
            standardResults = (SquadStats.FlightSpeed - missionStatus.Mission.TargetStats.FlightSpeed) +
                (SquadStats.DANCE - missionStatus.Mission.TargetStats.DANCE) + (SquadStats.Sharpness - missionStatus.Mission.TargetStats.Sharpness);
                break;
        }

        successNumber = weightedResults + standardResults;

        if (successNumber < 0)
        {
            missionStatus.status = MissionStatus.Status.failed;
            MissionSelector.instance.BeeSquadGraveyard.Add(this);
            MissionSelector.instance.BeeSquadUnits.Remove(this);
            missionStatus = null;
            return MissionResult.Failure;
        }


        missionStatus.status = MissionStatus.Status.complete;
        Available = true;
        missionStatus = null;
        return MissionResult.Success;
    }

    public void LevelUp()
    {
        //  stat increase code, will probably need parameters for player choice
        //  probably will be called by pollen handler
    }

    //  change to random model after leveling up
    public void Beevolve()
    {

    }

    public void NameBee(string name)
    {
        gameObject.name = name;
    }
}
