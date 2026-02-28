using UnityEngine;

public class MissionNode : MonoBehaviour
{
    public Mission mission;

    public enum Location
    {
        Forest,
        Meadow,
        Pond,
        Picnic
    }

    void Start()
    {
        
    }

    /*
     *  this script will be on several objects that are spread
     *  across the map representing the different areas.
     *  it will also be used to display mission data to the player
     *  when they click/hover over a mission.
     */

    public void HoverInfo()
    {
        Debug.Log(gameObject.name);
    }

    public void ClickSelect()
    {
        Debug.Log("clicked "+gameObject.name);
    }
}
