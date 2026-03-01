using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeadBeeCredits : MonoBehaviour

{
    //genuinely awful but i can't really be asked to care at this point
    private List<string> beeNames = new List<string> {"Abbee", "Abeeraham Beencoln", "Amaryllis", "Amber", "Anna Tenna", "Aster", "Azalea", "B(r)een", 
        "Barry Bee Benson", "Beeane", "Beebecca", "Beeborah", "Beechel", "Beechelle", "Beecole", "Beedward", "Beegela", "Beeirre", "Beelissa", "Beella", 
        "Beelladonna", "Beelliam", "Beelliot", "Beemily", "Beendra", "Beenée", "Beenie", "Beennifer", "Beenthia", "Beephen", "Beerah", "Beerbara", 
        "Beerent", "Beergaret", "Beergut Joe", "Beerissa", "Beerolyn", "Beesa", "Beesel", "Beetchell", "Beetherine", "Beethew", "Beetholomew", "Beetricia", 
        "Beetty", "Beevan", "Beeven", "Beevis", "Beeyonce", "Begonia", "Blossom", "Bluebeell", "Bobble", "Bristle", "Bumble", "Buzz Aldrin", "Buzz Lightyear", 
        "Buzzhead", "Carnation", "Chunky", "Clematis", "Clover", "Combala Harris", "Cowboy Beebop", "Crocus", "Daffodil", "Dahlia", "Daisy", "Debeetrius", 
        "Detective Bee Ew", "Elizabeeth", "Fern", "Fluff", "Forget-me-not", "Goldie", "Harvbee", "Heather", "Honey", "Honeydew", "Honeysuckle", "Hyacinth", 
        "Hydrangea", "Isabeella", "Jasmine", "Kathbeen", "Lace", "Larkspur", "Lavender", "Lily", "Mabeeline", "Magnolia", "Marigold", "Mistletoe", "Nectarine", 
        "Nettle", "Olive", "Orchid", "Pansy", "Peony", "Petunia", "Pollen", "Pudge", "Robeen", "Robert Bee Lee", "Rosemary", "Shaquille O'Beel", "Shelsbee", 
        "Stripe", "Sugar", "Sweetbee", "Tobee", "Tulip", "Tumble", "Violet", "Zinnia"};
    //private List<string> creditsBees;
    //private List<string> squadNames; //= MissionSelector.instance.BeeSquadGraveyard;

    private int deadCount;

    public TMP_Text graveyard;

    //private static Random rng = new Random();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

/*
        for(int i = 0; i < (6*squadNames.Count); i++)
        {
            creditsBees.Add(beeNames[Random.Range(0, beeNames.Count)]);
        }
*/
        graveyard = GetComponent<TMP_Text>();

        graveyard.text = "";
        deadCount = 7;

        for(int i = 0; i < MissionSelector.instance.BeeSquadGraveyard.Count; i++)
        {
            deadCount = 7;
            graveyard.text += "The " + MissionSelector.instance.BeeSquadGraveyard[i].name + " Squad:\n";
            if(MissionSelector.instance.BeeSquadGraveyard[i].Level > 1)
            {
                graveyard.text += "Captain " + beeNames[Random.Range(0, beeNames.Count)] + "\n";
                deadCount -= 1;
            }
            for (int j=1; j<deadCount; j++)
            {
                graveyard.text += beeNames[Random.Range(0, beeNames.Count)] + "\n";
            }
            graveyard.text += "\n\n";

        }

        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}


