using UnityEngine;

public class PollenHandler : MonoBehaviour
{
    MissionSelector missionSelector;
    [SerializeField] private int pollen;

    //  for choosing level up
    private BeeSquad.Stats levelUpStat;
    [SerializeField] private Mesh[] beePromotionModels;

    private void Start()
    {
        missionSelector = GetComponent<MissionSelector>();
    }

    /*
     *  write code about buying level ups here
     *  probably will mostly be handled in ui i'm
     *  assuming.
     */

}