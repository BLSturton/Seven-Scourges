using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleManager : MonoBehaviour
{
    [SerializeField] public GameObject[] enemyList;
    [SerializeField] public GameObject[] partyList;
    //1 For Cetus, 2 for Ratic
    [SerializeField] public int currentPartyTurn;
    [SerializeField] public bool enemyTurn;

    [SerializeField] OptionPicker cetusOptionPicker;
    [SerializeField] OptionPicker raticOptionPicker;

    [SerializeField] GameObject playerBackground;
    [SerializeField] GameObject playerObjects;
    [SerializeField] GameObject enemyBackground;
    [SerializeField] GameObject enemyObjects;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPartyTurn = 1;
        cetusOptionPicker.myTurn = true;

        playerBackground.SetActive(true);
        playerObjects.SetActive(true);
        enemyBackground.SetActive(false);
        enemyObjects.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cetusOptionPicker.endTurn) 
        {
            
            if(partyList.Length > 1) 
            {
                raticOptionPicker.myTurn = true;
            }
            else 
            {
              enemyTurn = true;

            }
        }
        if (raticOptionPicker.endTurn) 
        {
            raticOptionPicker.myTurn = false;
            enemyTurn = true;

        }

        //Starts enemy turn
        if (enemyTurn) 
        {
            playerBackground.SetActive(false);
            playerObjects.SetActive(false);
            enemyBackground.SetActive(true);
            enemyObjects.SetActive(true);

        }
    }
}
