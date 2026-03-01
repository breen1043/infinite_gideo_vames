using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "ScriptableObjects/Mission")]
public class Mission : ScriptableObject
{
    public BeeSquad.Stats TargetStats;

    public enum WeightedStat
    {
        None,
        FlightSpeed,
        DANCE,
        Sharpness,
        Hivemind
    }

    [Header("Weighted Stats")]
    public WeightedStat WeightedStat1;
    public WeightedStat WeightedStat2;

    [Header("How many time units the mission takes")]
    [Range(1, 6)]
    public int duration;

    [Header("Mission Details")]
    [TextArea(0, 18)]
    public string Title;
    [TextArea(0, 30)]
    public string Description;

    [Space(5)]
    public MissionNode.Location Location;
}
