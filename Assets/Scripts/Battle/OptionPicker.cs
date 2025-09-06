using NUnit.Framework;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class OptionPicker : MonoBehaviour
{
    [SerializeField] public GameObject[] attackOptions;
    //0 is attack, 1 is style, etc.
    [SerializeField] public GameObject selectedOption;
    [SerializeField] public bool selectCooldown;
    [SerializeField] public int optionNumber;


    [SerializeField] public BattleManager battleManager;

    [SerializeField] public bool actionPicked;
    [SerializeField] public bool attackOn;
    [SerializeField] public bool specialOn;
    //Attack
    [SerializeField] public int enemyNumber;
    [SerializeField] public int maxEnemy;
    [SerializeField] public GameObject enemyPicker;
    [SerializeField] public CetusAttack cetusAttack;
    [SerializeField] public GameObject cetusAttackAction;
    [SerializeField] public GameObject actionBox;

    //Special 
    [SerializeField] public GameObject styleBox;
    [SerializeField] public GameObject[] styles;
    [SerializeField] public GameObject[] styleText;
    [SerializeField] public int styleNumber;
    [SerializeField] public int maxStyle;
    [SerializeField] Taunt taunt;
    [SerializeField] public bool tauntOn;
    [SerializeField] public int tauntTurns;
    [SerializeField] Diagnosis diagnosis;
    [SerializeField] public bool diagnosisOn;
    [SerializeField] GameObject CetusStyle;
    [SerializeField] GameObject RaticStyle;
    [SerializeField] GameObject CetusStyle2;
    [SerializeField] GameObject RaticStyle2;
    [SerializeField] HellfallFangs hellfallFangs;
    [SerializeField] FirstAid firstAid;
    [SerializeField] public bool firstAidOn;
    [SerializeField] public RaticAttack raticAttack;
    [SerializeField] public GameObject raticAttackAction;
    [SerializeField] public GameObject countdownText;

    //Items
    [SerializeField] Inventory inventory;
    [SerializeField] public bool itemOn;


    //Defend
    [SerializeField] public bool isDefend;
    //End turn
    [SerializeField] public bool endTurn;
    [SerializeField] public bool myTurn;

    [SerializeField] public bool optionOn;

    [SerializeField] public bool canGoBack;
    [SerializeField] public GameObject targetEnemy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        canGoBack = true;
        firstAid.gameObject.SetActive(false);
        foreach (GameObject obj in styles)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
        styleBox.SetActive(false);
        cetusAttackAction.SetActive(false);
        raticAttackAction.SetActive(false);
        countdownText.SetActive(false);
        selectedOption = attackOptions[0];
        optionNumber = 0;
        selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y + 1);
        enemyPicker.SetActive(false);
       if(this.gameObject.name == "CetusOptionManager") 
        {
            foreach (GameObject obj in attackOptions)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
        else 
        {
            foreach (GameObject obj in attackOptions)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
        inventory = GameObject.FindWithTag("InventorySystem").GetComponent<Inventory>();

    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.inventoryBattleUsed) 
        {
            itemOn = false;

            myTurn = false;
            endTurn = true;
            if(this.gameObject.name == "RaticOptionManager") 
            {
                inventory.inventoryBattleUsed = false;
            }
            inventory.inventoryPicker.transform.position = inventory.raticHPSpawn1.transform.position;
        }
     
        if (myTurn)
        {


            //Advance Option
            if (Input.GetKeyDown(KeyCode.D) && !selectCooldown && !actionPicked)
            {
                if (optionNumber == 3)
                {
                    selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y - 1);
                    selectedOption = attackOptions[0];
                    optionNumber = 0;
                    selectCooldown = true;
                    selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y + 1);
                    StartCoroutine(AttackWait());
                }
                else
                {
                    selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y - 1);
                    selectedOption = attackOptions[optionNumber + 1];
                    optionNumber++;
                    selectCooldown = true;
                    selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y + 1);

                    StartCoroutine(AttackWait());
                }

            }
            //Reverse Option
            if (Input.GetKeyDown(KeyCode.A) && !selectCooldown && !actionPicked)
            {
                if (optionNumber == 0)
                {
                    selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y - 1);
                    selectedOption = attackOptions[3];
                    optionNumber = 3;
                    selectCooldown = true;
                    StartCoroutine(AttackWait());
                    selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y + 1);

                }
                else
                {
                    selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y - 1);
                    selectedOption = attackOptions[optionNumber - 1];
                    optionNumber--;
                    selectCooldown = true;
                    StartCoroutine(AttackWait());
                    selectedOption.transform.localPosition = new Vector2(selectedOption.transform.localPosition.x, selectedOption.transform.localPosition.y + 1);

                }

            }
            //Reverse
            if (Input.GetKeyDown(KeyCode.L) && canGoBack) 
            {
                foreach (GameObject obj in attackOptions)
                {
                    if (obj != null)
                    {
                        obj.SetActive(true);
                    }
                    

                }
                //Come back and add item and defend
                if (attackOn) 
                {
                    selectedOption = attackOptions[0];  
                }
                if (specialOn)
                {
                    selectedOption = attackOptions[1];
                }
                if (itemOn)
                {
                    selectedOption = attackOptions[2];
                    inventory.BattleClose();
                    itemOn = false;
                }
                actionPicked = false;
                attackOn = false;
                specialOn = false;
                enemyPicker.SetActive(false);
                styleBox.SetActive(false);
            }
            //Confirm Option
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (diagnosisOn) 
                {

                    diagnosis.gameObject.SetActive(true);
                    diagnosis.StartAttack();
                    diagnosisOn = false;
                    enemyPicker.SetActive(false);
                    actionBox.SetActive(true);
                }
                if (firstAidOn) 
                {
                    firstAidOn = false;
                    enemyPicker.SetActive(false);
                    actionBox.SetActive(true);
                    firstAid.gameObject.SetActive(true);
                    firstAid.StartAttack();
                }
                foreach (GameObject obj in attackOptions)
                {
                    if (obj != null)
                    {
                        obj.SetActive(false);
                    }
                }
                //If attacking
                if (attackOn)
                {

                    attackOn = false;
                    enemyPicker.SetActive(false);
                    actionBox.SetActive(true);
                    if (this.gameObject.name == "CetusOptionManager")
                    {
                        canGoBack = false;
                        cetusAttackAction.SetActive(true);
                        cetusAttack.StartAttack();
                    }
                    if (this.gameObject.name == "RaticOptionManager")
                    {
                        canGoBack = false;

                        raticAttackAction.SetActive(true);
                        raticAttack.StartAttack();
                    }

                }
                //If Special-ing
                if (specialOn) 
                {
                    
                    if (styleNumber == 0) 
                    {
                        if(this.gameObject.name == "CetusOptionManager" && battleManager.CetusSP >= 2) 
                        {
                            battleManager.CetusSP = battleManager.CetusSP - 2;
                            styleBox.SetActive(false);
                            foreach (GameObject obj in styles)
                            {
                                enemyPicker.SetActive(false);
                                if (obj != null)
                                {
                                    obj.SetActive(false);
                                }
                            }
                            canGoBack = false;

                            taunt.gameObject.SetActive(true);
                            taunt.StartAttack();
                            specialOn = false;
                        }
                        if (this.gameObject.name == "RaticOptionManager" && battleManager.RaticSP >= 2)
                        {
                            battleManager.RaticSP = battleManager.RaticSP - 2;
                            styleBox.SetActive(false);
                            foreach (GameObject obj in styles)
                            {
                                enemyPicker.SetActive(false);
                                if (obj != null)
                                {
                                    obj.SetActive(false);
                                }
                            }
                            canGoBack = false;
                            diagnosis.gameObject.SetActive(true);
                            diagnosisOn = true;
                            specialOn = false;
                            
                           
                        }
                    }
                    if (styleNumber == 1)
                    {
                        if (this.gameObject.name == "CetusOptionManager" && battleManager.CetusSP >= 3)
                        {
                            battleManager.CetusSP = battleManager.CetusSP - 3;
                            styleBox.SetActive(false);
                            foreach (GameObject obj in styles)
                            {
                                enemyPicker.SetActive(false);
                                if (obj != null)
                                {
                                    obj.SetActive(false);
                                }
                            }
                            canGoBack = false;
                            actionBox.SetActive(true);
                            hellfallFangs.gameObject.SetActive(true);
                            hellfallFangs.StartAttack();
                            specialOn = false;
                        }
                        if (this.gameObject.name == "RaticOptionManager" && battleManager.RaticSP >= 3)
                        {
                            battleManager.RaticSP = battleManager.RaticSP - 3;
                            styleBox.SetActive(false);
                            foreach (GameObject obj in styles)
                            {
                                enemyPicker.SetActive(false);
                                if (obj != null)
                                {
                                    obj.SetActive(false);
                                }
                            }
                            canGoBack = false;
                            specialOn = false;
                            firstAidOn = true;
                        }
                    }
                }
                if (!actionPicked)
                {
                    switch (optionNumber)
                    {
                        case 0:
                            attackOn = true;
                            break;
                        case 1:
                            specialOn = true;
                            styleBox.SetActive(true);
                            enemyPicker.SetActive(true);
                            foreach (GameObject obj in styles) 
                            {
                                obj.SetActive(true);
                            }
                            break;
                        case 2:
                            itemOn = true;
                            actionPicked = true;
                            inventory.inventoryPicker.transform.position = inventory.itemVisual[0].transform.GetChild(0).transform.position;

                            inventory.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                            inventory.BattleOpen();
                            break;
                        case 3:
                            isDefend = true;
                            if(this.gameObject.name == "CetusOptionManager") 
                            {
                                battleManager.CetusDef = battleManager.CetusDef + 1;
                                if(battleManager.CetusHPObject.GetComponent<HPWorld>().SP != battleManager.CetusHPObject.GetComponent<HPWorld>().MaxSP) 
                                {
                                    battleManager.CetusHPObject.GetComponent<HPWorld>().SP = battleManager.CetusHPObject.GetComponent<HPWorld>().SP + 1;

                                }
                                myTurn = false;
                                endTurn = true;
                            }
                            if (this.gameObject.name == "RaticOptionManager")
                            {
                                battleManager.RaticDef = battleManager.RaticDef + 1;
                                if (battleManager.RaticHPObject.GetComponent<HPWorld>().SP != battleManager.RaticHPObject.GetComponent<HPWorld>().MaxSP) 
                                {
                                    battleManager.RaticHPObject.GetComponent<HPWorld>().SP = battleManager.RaticHPObject.GetComponent<HPWorld>().SP + 1;

                                }

                                myTurn = false;
                                endTurn = true;
                            }
                            
                            break;
                    }
                }


                actionPicked = true;

            }
            //Attack
            if (attackOn || diagnosisOn)
            {
            
                enemyPicker.SetActive(true);
                //Advance Option
                if (Input.GetKeyDown(KeyCode.D) && !selectCooldown)
                {
                    if (enemyNumber == maxEnemy)
                    {

                        selectedOption = battleManager.enemyList[0];
                        enemyNumber = 0;
                        selectCooldown = true;
                        StartCoroutine(AttackWait());
                    }
                    else
                    {
                        selectedOption = battleManager.enemyList[enemyNumber + 1];
                        enemyNumber++;
                        selectCooldown = true;

                        StartCoroutine(AttackWait());
                    }

                }
                //Reverse Option
                if (Input.GetKeyDown(KeyCode.A) && !selectCooldown)
                {
                    if (enemyNumber == 0)
                    {

                        selectedOption = battleManager.enemyList[maxEnemy];
                        enemyNumber = 2;
                        selectCooldown = true;
                        StartCoroutine(AttackWait());
                    }
                    else
                    {
                        selectedOption = battleManager.enemyList[enemyNumber - 1];
                        enemyNumber--;
                        selectCooldown = true;

                        StartCoroutine(AttackWait());
                    }

                }

                //Moves selector
                switch (enemyNumber)
                {
                    case 0:
                        enemyPicker.transform.position = battleManager.enemyList[0].transform.position - new Vector3(1, 0, 0);
                        targetEnemy = battleManager.enemyList[0];
                        break;
                    case 1:
                        enemyPicker.transform.position = battleManager.enemyList[1].transform.position - new Vector3(1, 0, 0);
                        targetEnemy = battleManager.enemyList[1];

                        break;
                    case 2:
                        enemyPicker.transform.position = battleManager.enemyList[2].transform.position - new Vector3(1, 0, 0);
                        targetEnemy = battleManager.enemyList[2];

                        break;
                }
            }
            if (firstAidOn) 
            {

                enemyPicker.SetActive(true);
                //Advance Option
                if (Input.GetKeyDown(KeyCode.D) && !selectCooldown)
                {
                    if (enemyNumber == 1)
                    {

                        selectedOption = battleManager.partyList[0];
                        enemyNumber = 0;
                        selectCooldown = true;
                        StartCoroutine(AttackWait());
                    }
                    else
                    {
                        selectedOption = battleManager.partyList[enemyNumber + 1];
                        enemyNumber++;
                        selectCooldown = true;

                        StartCoroutine(AttackWait());
                    }

                }
                //Reverse Option
                if (Input.GetKeyDown(KeyCode.A) && !selectCooldown)
                {
                    if (enemyNumber == 0)
                    {

                        selectedOption = battleManager.partyList[1];
                        enemyNumber = 1;
                        selectCooldown = true;
                        StartCoroutine(AttackWait());
                    }
                    else
                    {
                        selectedOption = battleManager.partyList[enemyNumber - 1];
                        enemyNumber--;
                        selectCooldown = true;

                        StartCoroutine(AttackWait());
                    }

                }

                //Moves selector
                switch (enemyNumber)
                {
                    case 0:
                        enemyPicker.transform.position = battleManager.partyList[0].transform.position - new Vector3(1, -1.5f, 0);
                        targetEnemy = battleManager.partyList[0];
                        break;
                    case 1:
                        enemyPicker.transform.position = battleManager.partyList[1].transform.position - new Vector3(1, -1.5f, 0);
                        targetEnemy = battleManager.partyList[1];
                        break;
                   

                   
                }
            }


            if (specialOn)
            {
               
                if (this.gameObject.name == "CetusOptionManager")
                {
                   
                    styleText[0].SetActive(true);
                    styleText[0].SetActive(true);
                    CetusStyle.SetActive(true);
                    RaticStyle.SetActive(false);
                    if (styles.Length > 1) 
                    {
                        CetusStyle2.SetActive(true);
                    }
                    RaticStyle2.SetActive(false);
                }
                if (this.gameObject.name == "RaticOptionManager")
                {
                    styleText[2].SetActive(true);
                    styleText[3].SetActive(true);
                    CetusStyle.SetActive(false);
                    RaticStyle.SetActive(true);
                    CetusStyle2.SetActive(false);
                    RaticStyle2.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.D) && !selectCooldown)
                {
                    if (styleNumber == maxStyle)
                    {
                        
                        selectedOption = styles[0];
                        styleNumber = 0;
                        selectCooldown = true;
                        StartCoroutine(AttackWait());
                    }
                    else
                    {
                        selectedOption = styles[styleNumber + 1];
                        styleNumber++;
                        selectCooldown = true;

                        StartCoroutine(AttackWait());
                    }
                }
                if (Input.GetKeyDown(KeyCode.A) && !selectCooldown)
                {
                    if (styleNumber == 0)
                    {

                        selectedOption = styles[1];
                        styleNumber = 1;
                        selectCooldown = true;
                        StartCoroutine(AttackWait());
                    }
                    else
                    {
                        selectedOption = styles[styleNumber - 1];
                        styleNumber--;
                        selectCooldown = true;

                        StartCoroutine(AttackWait());
                    }
                }
                switch (styleNumber)
                {
                    case 0:
                            enemyPicker.transform.position = styles[0].transform.position - new Vector3(1, -1, 0);                       
                    
                        break;
                    case 1:
                        enemyPicker.transform.position = styles[1].transform.position - new Vector3(1, -1, 0);
                       
                        break;
               
                }
            }
        }
        else
        {

        }
    }
    

    public IEnumerator AttackWait() 
    {
        yield return new WaitForSeconds(.2f);
        selectCooldown = false;
    }
    //Starts a new turn after an option is given
   public void turnOptionsOn()
    {
        Debug.Log("over and over");
      foreach (GameObject obj in attackOptions)
                {
                    if (obj != null)
                    {
                        obj.SetActive(true);
                    }
                }
    }
   
}
