using System.Collections;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Taunt : MonoBehaviour
{

    [SerializeField] public GameObject actionBox;
    [SerializeField] public GameObject[] words;

    [SerializeField] public GameObject[] wordSpawns;
    [SerializeField] public GameObject mover;
    [SerializeField] public GameObject moverSpawn;

    [SerializeField] Vector2 movement;

    [SerializeField] public Rigidbody2D rb;

    [SerializeField] public float moveSpeed;

    [SerializeField] public BoxCollider2D moverHit;

    [SerializeField] public int wordsHit;

    [SerializeField] Text countDown;

    [SerializeField] OptionPicker cetusOptionPicker;

    [SerializeField] public BattleManager battleManager;
    // Start is called once before the first
    // execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

    }

    public void StartAttack() 
    {
        cetusOptionPicker.animator.SetBool("TauntStart", true);

        mover.SetActive(true);
        mover.transform.position = moverSpawn.transform.position;
        actionBox.SetActive(true);
        StartCoroutine(WordSpawn());
        countDown.gameObject.SetActive(true);
        StartCoroutine(CountDown());
    }

    public void EndAttack() 
    {
        if(wordsHit < 5) 
        {
            Debug.Log("Missed!");
        }
        if(wordsHit >= 5 && wordsHit < 9) 
        {
            Debug.Log("Good!");
            cetusOptionPicker.tauntOn = true;
            battleManager.partyDodgeList.RemoveAll(item => item.name == "RaticDodge");
            battleManager.CetusDef = battleManager.CetusDef + 1;
            cetusOptionPicker.tauntTurns = 1;
        }
        if(wordsHit >= 9) 
        {
            Debug.Log("Peroihnjefoiasuehf");
            cetusOptionPicker.tauntOn = true;
            battleManager.partyDodgeList.RemoveAll(item => item.name == "RaticDodge");
            battleManager.CetusDef = battleManager.CetusDef + 1;

            cetusOptionPicker.tauntTurns = 2;
        }
        StartCoroutine(attackAnimation());
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("WordTaunt");

        // Loop through the array and destroy each GameObject
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
        mover.SetActive(false);
        wordsHit = 0;
        actionBox.SetActive(false);
        countDown.gameObject.SetActive(false);
      
    }
    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }
    public IEnumerator WordSpawn() 
    {
        for (int i = 0; i < 7; i++) 
        {
            Vector3 randomPosition = Vector3.Lerp(wordSpawns[0].transform.position, wordSpawns[1].transform.position, Random.Range(0f, 1f));
           Instantiate(words[Random.Range(0, words.Length)], randomPosition, Quaternion.identity);
            Vector3 randomPosition2 = Vector3.Lerp(wordSpawns[2].transform.position, wordSpawns[3].transform.position, Random.Range(0f, 1f));
            Instantiate(words[Random.Range(0, words.Length)], randomPosition2, Quaternion.identity);

            yield return new WaitForSeconds(1f);
        }
    }
    public IEnumerator CountDown()

    { 
                countDown.text = "7";
        yield return new WaitForSeconds(1f);
        countDown.text = "6";
        yield return new WaitForSeconds(1f);
        countDown.text = "5";
        yield return new WaitForSeconds(1f);
        countDown.text = "4";
        yield return new WaitForSeconds(1f);
        countDown.text = "3";
        yield return new WaitForSeconds(1f);
        countDown.text = "2";
        yield return new WaitForSeconds(1f);
        countDown.text = "1";
        yield return new WaitForSeconds(1f);
        countDown.text = "0";

        EndAttack();
    }
    public IEnumerator attackAnimation()
    {
        if (wordsHit >= 5) 
        {
            cetusOptionPicker.animator.SetBool("TauntRelease", true);
            yield return new WaitForSeconds(.4f);
            cetusOptionPicker.animator.SetBool("TauntRelease", false);
            cetusOptionPicker.animator.SetBool("TauntStart", false);

        }

        else
        {
            cetusOptionPicker.animator.SetBool("TauntStart", false);

        }
        this.gameObject.SetActive(false);
        cetusOptionPicker.endTurn = true;
        cetusOptionPicker.myTurn = false;
    }
}
