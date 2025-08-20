using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellfallFangs : MonoBehaviour
{
    [SerializeField] public List<GameObject> swords;
    [SerializeField] public GameObject goal;
    [SerializeField] public float gravityScale;
    [SerializeField] public GameObject spawn1;
    [SerializeField] public GameObject spawn2;
    [SerializeField] public OptionPicker cetusOptionPicker;

    [SerializeField] MoverDetector moverLeft;
    [SerializeField] MoverDetector moverRight;

    [SerializeField] public bool hitLeft;
    [SerializeField] public bool hitRight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(moverLeft.hitNow && Input.GetKeyDown(KeyCode.K) && !hitLeft) 
            {
                hitLeft = true;
                swords.Find(obj => obj.name == "Sword1").SetActive(false);
            }
        if (moverRight.hitNow && Input.GetKeyDown(KeyCode.L) && !hitRight)
        {
            hitRight = true;
            swords.Find(obj => obj.name == "Sword2").SetActive(false);
        }
    }
    public void StartAttack() 
    {
        foreach (GameObject obj in swords) 
        {
            if(obj == swords[0]) 
            {
                obj.transform.position = spawn1.transform.position;
            }
            if (obj == swords[1])
            {
                obj.transform.position = spawn2.transform.position;
            }
            obj.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        StartCoroutine(bladeDrop());
    }

    public IEnumerator bladeDrop()
    {
       
            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
            swords[0].GetComponent<Rigidbody2D>().gravityScale = Random.Range(gravityScale, gravityScale + 1f);
            yield return new WaitForSeconds(.2f);
            swords[1].GetComponent<Rigidbody2D>().gravityScale = gravityScale + 1f;
        yield return new WaitForSeconds(1f);
        EndAttack();
    }

    public void EndAttack() 
    {
        if(!hitLeft && !hitRight) 
        {
            Debug.Log("Failed");
        }
        if(hitLeft && !hitRight || hitRight && !hitLeft) 
        {
            Debug.Log("Good");
        }
        if(hitLeft && hitRight) 
        {
            Debug.Log("Exellent!");
        }
    }
}
