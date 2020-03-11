using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controller : MonoBehaviour
{
    // Start is called before the first frame update
    public static Dictionary<int, enemy> enemyTypeDict;
    public static Dictionary<int, Vector3[]> pathTypeDict;
    public GameObject enemyPrefab;
    public GameObject enemyhpBarPrefab;
    private Vector3 startingPos;

    public enemy_generator wave1;
    private float timer = 0;
    void Awake(){
      startingPos = new Vector3(0, 0, 0);
    	enemyTypeDict = new Dictionary<int, enemy>();
        pathTypeDict = new Dictionary<int, Vector3[]>();


        //ENEMIES///////////////////////
        enemyTypeDict.Add(0, new enemy(
        	Resources.Load<Sprite>("enemy8"),
        	new Dictionary<string, int> {{"hp",20}, {"ar",1}, {"dmg",1}, {"spd",3}, {"money", 10}, {"width", 1}}, 1.5f)
        );
        enemyTypeDict.Add(1, new enemy(
            Resources.Load<Sprite>("enemy2"),
            new Dictionary<string, int> {{"hp",3}, {"ar",1}, {"dmg",1}, {"spd",8}, {"money", 5}, {"width", 1}}, .75f)
        );

        enemyTypeDict.Add(100, new enemy(
        	Resources.Load<Sprite>("enemy7"),
        	new Dictionary<string, int> {{"hp",500}, {"ar",1}, {"dmg",3}, {"spd",5}, {"money", 200}, {"width", 5}}, 6f)
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
        pathTypeDict.Add(100, new Vector3[]{
            new Vector3(.1f, .9f, 0),
            new Vector3(.9f, .9f, 0),
            new Vector3(.1f, .1f, 0),
            new Vector3(.9f, .1f, 0),
            new Vector3(.1f, .1f, 0),
            new Vector3(-1000f, -1000f, -1000f)
        });
        ///////////////////////////////

        wave1 = new enemy_generator();
        wave1.set(0,0,.1f);
        wave1.set(0,1,.1f);
        wave1.set(1,0,.4f);
        wave1.set(1,1,.4f);
    }



    void Start()
    {
      Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 10f){ //boss 1
            GameObject enemy = Instantiate(enemyPrefab, startingPos, Quaternion.identity);
            enemy.GetComponent<enemy>().enemyType = 100;
            enemy.GetComponent<enemy>().pathType = 100;
            enemy.GetComponent<enemy>().hpBarPrefab = enemyhpBarPrefab;
            timer = -10000;
        }
    }

    void Spawn()
    {

      GameObject enemy = Instantiate(enemyPrefab, startingPos, Quaternion.identity);
      KeyValuePair<int,int> enemyInfo = wave1.getOutcome();
      enemy.GetComponent<enemy>().enemyType = enemyInfo.Key;
      enemy.GetComponent<enemy>().pathType = enemyInfo.Value;
      enemy.GetComponent<enemy>().hpBarPrefab = enemyhpBarPrefab;
      Invoke("Spawn", 1f);
    }

    public static Vector3 processWaypt(Vector3 pt){
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
        return new Vector3((2 * pt[0] - 1) * stageDimensions[0], (2 * pt[1] - 1) * stageDimensions[1], 0);
    }
}
