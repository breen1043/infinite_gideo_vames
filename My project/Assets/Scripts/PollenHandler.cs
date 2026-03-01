using UnityEngine;

public class PollenHandler : MonoBehaviour
{
    public static PollenHandler instance;
    [SerializeField] private int pollen;

    //  for choosing level up
    private BeeSquad.Stats levelUpStat;
    [SerializeField] private Mesh[] beePromotionModels;

    public enum PollenAmount
    {
        Low,
        Medium,
        High
    }

    private void Start()
    {
        instance = GetComponent<PollenHandler>();
    }

    /*
     *  write code about buying level ups here
     *  probably will mostly be handled in ui i'm
     *  assuming.
     */

}