using System.Collections;
using TMPro;
using UnityEngine;

public class Taunt : MonoBehaviour
{

    [SerializeField] public GameObject actionBox;
    [SerializeField] public GameObject[] words;
    [SerializeField] public GameObject[] currentWords;

    [SerializeField] public GameObject[] wordSpawns;
    [SerializeField] public GameObject mover;
    [SerializeField] public GameObject moverSpawn;

    [SerializeField] Vector2 movement;

    [SerializeField] public Rigidbody2D rb;

    [SerializeField] public float moveSpeed;
    // Start is called once before the first
    // execution of Update after the MonoBehaviour is created
    void Start()
    {
        actionBox.SetActive(false);
        mover.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    public void StartAttack() 
    {
        mover.SetActive(true);
        mover.transform.position = moverSpawn.transform.position;
        actionBox.SetActive(true);
        StartCoroutine(WordSpawn());
    }

    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }
    public IEnumerator WordSpawn() 
    {
        for (int i = 0; i < 5; i++) 
        {
            Vector3 randomPosition = Vector3.Lerp(wordSpawns[0].transform.position, wordSpawns[1].transform.position, Random.Range(0f, 1f));
           Instantiate(words[Random.Range(0, words.Length)], randomPosition, Quaternion.identity);
            Vector3 randomPosition2 = Vector3.Lerp(wordSpawns[2].transform.position, wordSpawns[3].transform.position, Random.Range(0f, 1f));
            Instantiate(words[Random.Range(0, words.Length)], randomPosition2, Quaternion.identity);

            yield return new WaitForSeconds(1f);
        }
    }
}
