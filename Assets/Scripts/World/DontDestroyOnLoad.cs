using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] public bool isBattle;
    [SerializeField] public GameObject[] childMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
      
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Battle") && isBattle == false)
        {
         
            foreach (GameObject obj in childMove)
            {
                obj.transform.SetParent(null);
                SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("Battle"));

            }

            isBattle = true;
        }
    }

    public void sceneMove() 
    {
    }
}
