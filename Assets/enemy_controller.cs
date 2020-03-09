using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controller : MonoBehaviour
{
    // Start is called before the first frame update
    public static Dictionary<int, enemy> enemyTypeDict;
    public static Dictionary<int, Vector3[]> pathTypeDict;
    public GameObject enemyPrefab;
    private Vector3 startingPos;

    void Awake(){
      startingPos = new Vector3(0, 0, 0);
    	enemyTypeDict = new Dictionary<int, enemy>();
        pathTypeDict = new Dictionary<int, Vector3[]>();


        //ENEMIES///////////////////////
        enemyTypeDict.Add(0, new enemy(
        	Resources.Load<Sprite>("enemy8"),
        	new Dictionary<string, int> {{"hp",20}, {"ar",1}, {"dmg",1}, {"spd",3}})
        );
        ///////////////////////////////




        //PATHS///////////////////////
        pathTypeDict.Add(0, new Vector3[]{
    		new Vector3(0,.9f,0),
    		new Vector3(.9f,.9f,0),
            new Vector3(-1f,-1f,-1)
        });

        pathTypeDict.Add(1, new Vector3[]{
            new Vector3(.9f, .9f, 0),
            new Vector3(0, .9f, 0),
            new Vector3(1.5f, -.5f,0)
        });
        ///////////////////////////////

    }



    void Start()
    {
      Spawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn()
    {
      GameObject enemy = Instantiate(enemyPrefab, startingPos, Quaternion.identity);
      enemy.GetComponent<enemy>().enemyType = 0;
      Invoke("Spawn", 1f);
    }

    public static Vector3 processWaypt(Vector3 pt){
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
        return new Vector3((2 * pt[0] - 1) * stageDimensions[0], (2 * pt[1] - 1) * stageDimensions[1], 0);
    }
}
