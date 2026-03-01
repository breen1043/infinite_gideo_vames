using UnityEngine;

public class RecruitBees : MonoBehaviour
{
    public static RecruitBees instance;
    public GameObject beeSquad;
    public Transform parentObject;

    public Vector3 spawnPosition = new Vector3(0, 0, 0);
    public Quaternion spawnRotation = Quaternion.identity;

    public string squadName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = GetComponent<RecruitBees>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RecruitSquad()
    {
        GameObject newBee = Instantiate(beeSquad, parentObject);
        MissionSelector.instance.BeeSquadUnits.Add(newBee.GetComponent<BeeSquad>());
        newBee.SetActive(false);
    }
}
