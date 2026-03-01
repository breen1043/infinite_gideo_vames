using UnityEngine;
using UnityEngine.UI;

public class HoneyHandler : MonoBehaviour
{
    public static HoneyHandler instance;

    public Image fill;
    public Slider slider;

    [SerializeField] private int honeyCount;
    [SerializeField] private int honeyRecruitReq = 6;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = GetComponent<HoneyHandler>();
        honeyCount = 0;
        slider.value = honeyCount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHoney()
    {
        slider.value = honeyCount;
    }

    public void GainHoney()
    {
        honeyCount += Random.Range(2, 5);
        Debug.Log("You got honey!");
        SetHoney();
        Debug.Log("Honey Count: " + honeyCount);
        if (honeyCount >= honeyRecruitReq)
        {
            // create a new bee squad once the player has enough honey
            RecruitBees.instance.RecruitSquad();
            Debug.Log("You got a new squad!");
            // remove that honey from your pool
            honeyCount -= honeyRecruitReq;
            SetHoney();

        }
    }
}
