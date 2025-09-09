using System.Collections;
using TMPro;
using UnityEngine;

public class FirstAid : MonoBehaviour
{
    [SerializeField] public GameObject[] lines;
    [SerializeField] public GameObject[] lineSpawns;
    [SerializeField] public GameObject[] cutLine;

    [SerializeField] public GameObject scissor;
    [SerializeField] public GameObject hitLine;

    [SerializeField] public Transform scissorSpawn;
    [SerializeField] public Transform scissorEnd;

    [SerializeField] public float scissorSpeed;

    [SerializeField] public Animator scissorAnim;
    [SerializeField] public int SnipsHit;

    [SerializeField] OptionPicker raticOptionPicker;
    [SerializeField] BattleManager battleManager;

    [SerializeField] public GameObject cetusHP;
    [SerializeField] public GameObject raticHP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cetusHP = GameObject.FindWithTag("CetusHP");
        raticHP = GameObject.FindWithTag("RaticHP");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && scissor.GetComponent<MoverDetector>().hitNow == false)
        {
            StartCoroutine(scissorCut());
            EndAttack();
        }
        scissor.transform.position = Vector2.MoveTowards(scissor.transform.position, scissorEnd.transform.position, scissorSpeed * Time.deltaTime);
        if(scissor.GetComponent<MoverDetector>().hitNow && Input.GetKeyDown(KeyCode.K)) 
        {
            StartCoroutine(scissorCut());
            if(hitLine == lines[0])
            {
                cutLine[0].SetActive(true);
                cutLine[0].transform.position = lines[0].transform.position;
                lines[0].SetActive(false);
                scissorSpeed = scissorSpeed + 1.5f;
            }
            if (hitLine == lines[1])
            {
                cutLine[1].SetActive(true);
                cutLine[1].transform.position = lines[1].transform.position;
                lines[1].SetActive(false);
                scissorSpeed = scissorSpeed + 1.5f;

            }
            if (hitLine == lines[2])
            {
                cutLine[2].SetActive(true);
                cutLine[2].transform.position = lines[2].transform.position;
                lines[2].SetActive(false);
                scissorSpeed = scissorSpeed + 1.5f;

            }

            SnipsHit = SnipsHit + 1;
            
        }
        
        if (scissor.transform.position == scissorEnd.transform.position)
        {
            EndAttack();
        }
    }

    public void StartAttack() 
    {
        foreach (GameObject obj in lines) 
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in cutLine) 
        {
            obj.SetActive(false);
        }
        scissor.transform.position = scissorSpawn.transform.position;
        lines[0].transform.position = lineSpawns[Random.Range(0, 2)].transform.position;
        lines[1].transform.position = lineSpawns[Random.Range(3, 5)].transform.position;
        lines[2].transform.position = lineSpawns[Random.Range(6, 8)].transform.position;

    }

    public void EndAttack() 
    {
        if(SnipsHit == 0 || SnipsHit == 1) 
        {
            Debug.Log("Missed!");
        }
        if(SnipsHit == 2)
        {
            if(raticOptionPicker.enemyNumber == 0) 
            {
                if (battleManager.CetusHP < battleManager.CetusMaxHP) 
                {
                    cetusHP.GetComponent<HPWorld>().HP = cetusHP.GetComponent<HPWorld>().HP + 1;
                }
            }
            if (raticOptionPicker.enemyNumber == 1)
            {
                if (battleManager.RaticHP < battleManager.RaticMaxHP)
                {
                    raticHP.GetComponent<HPWorld>().HP = raticHP.GetComponent<HPWorld>().HP + 1;
                }
            }
        }
        if (SnipsHit == 3)
        {
            if (raticOptionPicker.enemyNumber == 0)
            {
                if (battleManager.CetusHP == battleManager.CetusMaxHP -1)
                {
                    cetusHP.GetComponent<HPWorld>().HP = cetusHP.GetComponent<HPWorld>().HP + 1;
                }
                if (battleManager.CetusHP < battleManager.CetusMaxHP -1)
                {
                    cetusHP.GetComponent<HPWorld>().HP = cetusHP.GetComponent<HPWorld>().HP + 2;  
                }
            }
            if (raticOptionPicker.enemyNumber == 1)
            {
                if (battleManager.RaticHP == battleManager.RaticMaxHP - 1)
                {
                    raticHP.GetComponent<HPWorld>().HP = raticHP.GetComponent<HPWorld>().HP + 1;
                }
                if (battleManager.RaticHP < battleManager.RaticMaxHP - 1)
                {
                    raticHP.GetComponent<HPWorld>().HP = raticHP.GetComponent<HPWorld>().HP + 2;
                }
            }
        }
                SnipsHit = 0;
        scissorSpeed = 2f;
        this.gameObject.SetActive(false);
        raticOptionPicker.endTurn = true;
        raticOptionPicker.myTurn = false;
    }
    public IEnumerator scissorCut() 
    {
        scissorAnim.SetBool("Cut", true);
        yield return new WaitForSeconds(.2f);
        scissorAnim.SetBool("Cut", false);
    }
}
