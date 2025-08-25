using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] public bool isBattle;
    [SerializeField] public GameObject[] childMove;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      DontDestroyOnLoad(this);
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Battle")) 
        {
            foreach (GameObject obj in childMove) 
            {
                GameObject newParentGameObject = GameObject.FindGameObjectWithTag("BattleUI");
                obj.transform.SetParent(newParentGameObject.transform, true);
                Debug.Log("Moved");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void sceneMove() 
    {
    }
}
