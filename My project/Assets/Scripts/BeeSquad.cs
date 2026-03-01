using System;
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

    public int Level;
    public Stats SquadStats;

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
