using NUnit.Framework;
using System.Collections;
using System.Linq;
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

    [SerializeField] public RaticAttack raticAttack;
    [SerializeField] public GameObject raticAttackAction;
    [SerializeField] public GameObject countdownText;
    //End turn
    [SerializeField] public bool endTurn;
    [SerializeField] public bool myTurn;

    [SerializeField] public bool optionOn;

    [SerializeField] public bool canGoBack;
    [SerializeField] public GameObject targetEnemy;

    [SerializeField] public bool tauntOn;
    [SerializeField] public int tauntTurns;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canGoBack = true;

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
        
    }

    // Update is called once per frame
    void Update()
    {

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
                actionPicked = false;
                attackOn = false;
                specialOn = false;
                enemyPicker.SetActive(false);
                styleBox.SetActive(false);
            }
            //Confirm Option
            if (Input.GetKeyDown(KeyCode.K))
            {
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
                            styles[0].SetActive(true);
                            styles[1].SetActive(true);
                            break;
                    }
                }


                actionPicked = true;

            }
            //Attack
            if (attackOn)
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


            if (specialOn)
            {
               
                if (this.gameObject.name == "CetusOptionManager")
                {
                   
                    styleText[0].SetActive(true);
                    styleText[0].SetActive(true);

                }
                if (this.gameObject.name == "RaticOptionManager")
                {
                    styleText[2].SetActive(true);
                    styleText[3].SetActive(true);
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
