using System;
using UnityEngine;

public class BeeSquad : MonoBehaviour
{
    [Serializable]
    public struct Stats
    {
        public int FlightSpeed;
        public int DANCE;
        public int Sharpness;
        public int Hivemind;
    }
    public int Level;
    public Stats SquadStats;
    public int points;

    public bool Available = true;
    public int HoursUnilArrival;

    public enum MissionResult
    {
        Success,
        Failure,
        NearMiss
    }

    /*
     *  note: not sure how to structure squads completely yet.
     */

    public MissionResult MissionCheck(Mission mission)
    {
        //  stat calculations and removal from list when killed
        return MissionResult.Failure;
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
