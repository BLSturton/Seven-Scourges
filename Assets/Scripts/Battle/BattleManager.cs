using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] public GameObject[] enemyList;
    [SerializeField] public GameObject[] partyList;
    //1 For Cetus, 2 for Ratic
    [SerializeField] public int currentPartyTurn;

    [SerializeField] OptionPicker cetusOptionPicker;
    [SerializeField] OptionPicker raticOptionPicker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPartyTurn = 1;
        cetusOptionPicker.myTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cetusOptionPicker.endTurn) 
        {
            raticOptionPicker.myTurn = true;
            
        }
        if (raticOptionPicker.endTurn) 
        {
            raticOptionPicker.myTurn = false;
            Debug.Log("enemy timeeeeeee");
        }
    }
}
