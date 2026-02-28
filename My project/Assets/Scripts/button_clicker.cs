using UnityEngine;
using UnityEngine.InputSystem;

public class WorldClicker : MonoBehaviour
{
    public MissionSelector missionSelector;
    public GameObject uiPanel;
    private Clicker input; // auto-generated class
    private bool quest_panel = false;

    void Awake()
    {
        input = new Clicker();
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        if (input.m1.left_click.WasPerformedThisFrame())
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //Debug.Log("Hit: " + hit.collider.name);
                quest_panel = false;
                if (hit.collider.gameObject == gameObject)
                {
                    quest_panel = true;
                    
                }
            }
           missionSelector.OpenQuest(quest_panel); 
        }
        
    }
}
