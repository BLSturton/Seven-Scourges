using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
//using static UnityEditor.Progress;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> enemyList;
    [SerializeField] public List<GameObject> partyList;
    [SerializeField] public List<GameObject> partyDodgeList;
    [SerializeField] public GameObject targetDodger;
    [SerializeField] public GameObject RaticDodge;
    //1 For Cetus, 2 for Ratic
    [SerializeField] public int currentPartyTurn;
    [SerializeField] public bool enemyTurn;
   
    [SerializeField] public OptionPicker cetusOptionPicker;
    [SerializeField] public OptionPicker raticOptionPicker;
 

    [SerializeField] GameObject playerBackground;
    [SerializeField] GameObject playerObjects;
    [SerializeField] GameObject enemyBackground;
    [SerializeField] GameObject enemyObjects;

    [SerializeField] GameObject actionBox;
    [SerializeField] GameObject dodgerSpawnPoint;

    [SerializeField] public bool targetPicked;

    [SerializeField] public bool turnReset;
    [SerializeField] public bool CetusToRatic;
 


    [SerializeField] public bool enemyTurnEnd;

    [SerializeField] public int CetusHP;
    [SerializeField] public int RaticHP;
    [SerializeField] public int CetusMaxHP;
    [SerializeField] public int RaticMaxHP;
    [SerializeField] public int CetusMaxSP;
    [SerializeField] public int RaticMaxSP;
    [SerializeField] public int damageValue;
    [SerializeField] public int CetusDef;
    [SerializeField] public int RaticDef;
    [SerializeField] public int CetusSP;
    [SerializeField] public int RaticSP;
    [SerializeField] public GameObject CetusHPObject;
    [SerializeField] public GameObject RaticHPObject;


    [SerializeField] public Text CetusHPVisual;
    [SerializeField] public Text RaticHPVisual;
    [SerializeField] public Text CetusSPVisual;
    [SerializeField] public Text RaticSPVisual;

    [SerializeField] public bool CetusDown;
    [SerializeField] public bool RaticDown;

    [SerializeField] public GameObject CetusSprite;
    [SerializeField] public GameObject RaticSprite;

    [SerializeField] public CetusSwordSpin swordSpin;

    [SerializeField] public GameObject hellfallObject;

    [SerializeField] Diagnosis diagnosis;

    [SerializeField] public List<string> diagnosisList;

    [SerializeField] Inventory inventory;

    [SerializeField] Transporter transporter;

    [SerializeField] public GameObject[] enemyLibrary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.FindWithTag("InventorySystem").GetComponent<Inventory>();

        CetusHPObject = GameObject.FindWithTag("CetusHP");

        RaticHPObject = GameObject.FindWithTag("RaticHP");

        foreach(GameObject obj in inventory.enemyTroops) 
        {
            GameObject newObject = Instantiate(obj);

            enemyList.Add(newObject);
        }
        currentPartyTurn = 1;
        cetusOptionPicker.myTurn = true;

        playerBackground.SetActive(true);
        playerObjects.SetActive(true);
        enemyBackground.SetActive(false);
        enemyObjects.SetActive(false);
        actionBox.SetActive(false);
        foreach (GameObject obj in partyDodgeList)
        {
            if (obj != null)
            {
                obj.SetActive(false); // Deactivates the GameObject
            }
        }
        if (enemyList.Count == 1)
        {
            Debug.Log("Moved");
            enemyList[0].transform.position = new Vector2(6.2f, -.36f);
        }
        if (enemyList.Count == 2)
        {
            enemyList[0].transform.position = new Vector2(6.2f, -.36f);
            enemyList[1].transform.position = new Vector2(5.5f, -2.8f);
        }
        if (enemyList.Count == 3)
        {
            enemyList[0].transform.position = new Vector2(4.9f, -.14f);
            enemyList[1].transform.position = new Vector2(6.3f, -1.53f);
            enemyList[2].transform.position = new Vector2(5.3f, -3.7f);

        }
       
    }

    // Update is called once per frame
    void Update()
    {

        CetusMaxHP = CetusHPObject.GetComponent<HPWorld>().MaxHP;
        CetusMaxSP = CetusHPObject.GetComponent<HPWorld>().MaxSP;

        CetusHP = CetusHPObject.GetComponent<HPWorld>().HP;
        CetusSP = CetusHPObject.GetComponent<HPWorld>().SP;

        RaticMaxHP = RaticHPObject.GetComponent<HPWorld>().MaxHP;
        RaticMaxSP = RaticHPObject.GetComponent<HPWorld>().MaxSP;

        RaticHP = RaticHPObject.GetComponent<HPWorld>().HP;
        RaticSP = RaticHPObject.GetComponent<HPWorld>().SP;

        CetusHPVisual = CetusHPObject.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        CetusSPVisual = CetusHPObject.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();

        RaticHPVisual = RaticHPObject.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        RaticSPVisual = RaticHPObject.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        if (cetusOptionPicker.styles.Length == 1)
        {
            hellfallObject.SetActive(false);
        }
        if (enemyList.Count == 0) 
        {
            Debug.Log("Your winner :)");
            StartCoroutine(BattleEnd());
        }
        //Checks if party is down given size
        if(partyList.Count == 1) 
        {
            if (CetusDown) 
            {
                Debug.Log("Game over man!");
            }
        }
        if (partyList.Count == 2)
        {
            if (CetusDown)
            {
                partyDodgeList.RemoveAll(item => item.name == "CetusDodge");
                partyList.RemoveAll(item => item.name == "Cetus");
            }
            if (RaticDown) 
            {
                partyDodgeList.RemoveAll(item => item.name == "RaticDodge");
                partyList.RemoveAll(item => item.name == "Ratic");
            }
        }
        //Displays current HP
        CetusHPVisual.text = CetusHP.ToString();
        RaticHPVisual.text = RaticHP.ToString();
        //SP
        CetusSPVisual.text = CetusSP.ToString();
        RaticSPVisual.text = RaticSP.ToString();
      
        if (cetusOptionPicker.endTurn || diagnosis.goAgain) 
        {
            if (diagnosis.goAgain) 
            {
                    raticOptionPicker.enabled = true;
                    raticOptionPicker.myTurn = true;
                    raticOptionPicker.endTurn = false;
                    raticOptionPicker.actionPicked = false;
                    CetusToRatic = true;
                    foreach (GameObject obj in raticOptionPicker.attackOptions)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                        }
                    }
                    diagnosis.goAgain = false;
                
            }
            if (partyList.Count == 2) 
            {
                if (!CetusToRatic) 
                {
                    raticOptionPicker.enabled = true;
                    raticOptionPicker.myTurn = true;
                    raticOptionPicker.endTurn = false;
                    raticOptionPicker.actionPicked = false;
                    CetusToRatic = true;
                    foreach (GameObject obj in raticOptionPicker.attackOptions)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                        }
                    }
                }
               
            }
            else 
            {
              enemyTurn = true;
                cetusOptionPicker.endTurn = false;
            }
        }
        if (raticOptionPicker.endTurn) 
        {
            raticOptionPicker.myTurn = false;
            enemyTurn = true;
            raticOptionPicker.endTurn = false;
            
        }

        //Starts enemy turn
        if (enemyTurn)
        {

           

                playerBackground.SetActive(false);
            playerObjects.SetActive(false);
            enemyBackground.SetActive(true);
            enemyObjects.SetActive(true);
            if(enemyList.Count == 1) 
            {
                enemyList[0].transform.position = new Vector2(0f, 3.5f);
            }
            if(enemyList.Count == 2) 
            {
                enemyList[0].transform.position = new Vector2(-1.7f, 3.5f);
                enemyList[1].transform.position = new Vector2(1.4f, 3.5f);
            }
            if (enemyList.Count == 3)
            {
                enemyList[0].transform.position = new Vector2(0f, 3.5f);
                enemyList[1].transform.position = new Vector2(-2.7f, 3.5f);
                enemyList[2].transform.position = new Vector2(2.6f, 3.5f);
            }
            actionBox.SetActive(true);
            foreach (GameObject obj in partyList)
            {
                if (obj != null)
                {
                    obj.SetActive(false); // Deactivates the GameObject
                }
            }
          
            if (!targetPicked) 
            {

                targetDodger = partyDodgeList[Random.Range(0, partyDodgeList.Count)];
                targetDodger.transform.position = dodgerSpawnPoint.transform.position;
                targetDodger.GetComponent<SpriteRenderer>().enabled = true;
                targetDodger.SetActive(true);
                targetPicked = true;
                if(targetDodger.gameObject.name == "RaticDodge") 
                {
                    targetDodger.GetComponent<BattleMove>().canDash = true;
                    targetDodger.GetComponent<BattleMove>().isDash = false;
                    targetDodger.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

                }


            }
           if(enemyList.Count == 1) 
            {
               
                enemyList[0].gameObject.GetComponent<EnemyAttacks>().attackLead = true;
                enemyList[0].gameObject.GetComponent<EnemyAttacks>().AttackSelector();
               
            }
            if (enemyList.Count == 2)
            {
                enemyList[0].gameObject.GetComponent<EnemyAttacks>().attackLead = true;
                enemyList[0].gameObject.GetComponent<EnemyAttacks>().AttackSelector();
                enemyList[1].gameObject.GetComponent<EnemyAttacks>().attackLead = false;
                enemyList[1].gameObject.GetComponent<EnemyAttacks>().AttackSelector();

            }
            if (enemyList.Count == 3)
            {
                enemyList[0].gameObject.GetComponent<EnemyAttacks>().attackLead = true;
                enemyList[0].gameObject.GetComponent<EnemyAttacks>().AttackSelector();
                enemyList[1].gameObject.GetComponent<EnemyAttacks>().attackLead = false;
                enemyList[1].gameObject.GetComponent<EnemyAttacks>().AttackSelector();
                enemyList[2].gameObject.GetComponent<EnemyAttacks>().attackLead = false;
                enemyList[2].gameObject.GetComponent<EnemyAttacks>().AttackSelector();
            }
            enemyTurn = false;
        }
        if (enemyTurnEnd) 
        {
            newPlayerTurn();
            if (enemyTurnEnd) 
            {
                if (!CetusDown) 
                {
                    cetusOptionPicker.turnOptionsOn();
                }
                else 
                {
                    raticOptionPicker.turnOptionsOn();
                }

            }
            enemyTurnEnd = false;

        }
    }

    public void newPlayerTurn() 
    {
        if (cetusOptionPicker.isDefend)
        {
            CetusDef = CetusDef - 1;
            cetusOptionPicker.isDefend = false;

        }
        if (raticOptionPicker.isDefend)
        {
            RaticDef = RaticDef - 1;
            raticOptionPicker.isDefend = false;

        }
        if (cetusOptionPicker.tauntTurns != 0) 
        {
            cetusOptionPicker.tauntTurns = cetusOptionPicker.tauntTurns - 1;
        }
        if(cetusOptionPicker.tauntTurns == 0 && cetusOptionPicker.tauntOn)
        {
            cetusOptionPicker.tauntOn = false;
            CetusDef = CetusDef - 1;
            RaticDodge.SetActive(true);
            partyDodgeList.Add(RaticDodge);
            RaticDodge.SetActive(false);

        }
        cetusOptionPicker.canGoBack = true;
        raticOptionPicker.canGoBack = true;
        swordSpin.isSpin = false;
        foreach (GameObject obj in enemyList)
        {
            if (obj != null)
            {
                obj.GetComponent<EnemyAttacks>().AnimationReturn();
            }
        }
        cetusOptionPicker.endTurn = false;
        CetusToRatic = false;
        if (enemyList.Count == 1)
            {
                enemyList[0].transform.position = new Vector2(6.2f, -.36f);
            }
            if (enemyList.Count == 2)
            {
                enemyList[0].transform.position = new Vector2(6.2f, -.36f);
                enemyList[1].transform.position = new Vector2(5.5f, -2.8f);
            }
            if (enemyList.Count == 3)
            {
                enemyList[0].transform.position = new Vector2(4.9f, -.14f);
                enemyList[1].transform.position = new Vector2(6.3f, -1.53f);
                enemyList[2].transform.position = new Vector2(5.3f, -3.7f);

            }
        playerBackground.SetActive(true);
        playerObjects.SetActive(true);
        enemyBackground.SetActive(false);
        enemyObjects.SetActive(false);
        actionBox.SetActive(false);
        turnReset = false;
        targetPicked = false;

        targetDodger.SetActive(false);
        actionBox.SetActive(false);
        if (!CetusDown) 
        {
            raticOptionPicker.myTurn = false;
            cetusOptionPicker.myTurn = true;
            cetusOptionPicker.actionPicked = false;
            cetusOptionPicker.attackOn = false;
        }
        else
        {
            cetusOptionPicker.myTurn = false;
            raticOptionPicker.myTurn = true;
            raticOptionPicker.actionPicked = false;
            raticOptionPicker.attackOn = false;
        }

        if (partyList.Count == 2)
        {
            raticOptionPicker.actionPicked = false;
            raticOptionPicker.attackOn = false;

        }
        foreach (GameObject obj in partyList)
        {
            if (obj != null)
            {
                obj.SetActive(true); 
            }
        }
    }

    public void CetusDamage() 
    {
        if(CetusHP > 0) 
        {
            CetusHPObject.GetComponent<HPWorld>().HP = CetusHPObject.GetComponent<HPWorld>().HP + CetusDef - damageValue;
            if (CetusHPObject.GetComponent<HPWorld>().HP <= 0) 
            {
                CetusDown = true;
            }
            if (cetusOptionPicker.isDefend) 
            {
                CetusDef = CetusDef - 1;
                cetusOptionPicker.isDefend = false;

            }
        }
    }
    public void RaticDamage()
    {
        if (RaticHP > 0)
        {
            RaticHPObject.GetComponent<HPWorld>().HP = RaticHPObject.GetComponent<HPWorld>().HP + RaticDef - damageValue;
            if (RaticHPObject.GetComponent<HPWorld>().HP <= 0)
            {
               RaticDown = true;
            }
            if (raticOptionPicker.isDefend)
            {
                RaticDef = RaticDef - 1;
                raticOptionPicker.isDefend = false;

            }
        }
    }

    public IEnumerator BattleEnd() 
    {

        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene("Beach");
    }
}
