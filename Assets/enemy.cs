using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int enemyType;
    public Sprite sprite;
    public GameObject explosion;

    public int pathType;
    public Vector3[] waypoints;
    
    Dictionary<string, int> stats; //we need hp, attack rate, damage, speed

    private int curr_pt = 1;
    public enemy(int ty, int pathty){
        enemyType = ty;
        pathType = pathty;
    }
    //use this only for defining enemy archetypes
    public enemy(Sprite spr, Dictionary<string, int> sts){
        sprite = spr;
        stats = sts;
    }

    void Start()
    {
        enemy archetype = enemy_controller.enemyTypeDict[enemyType];
        sprite = archetype.sprite;
        stats = archetype.stats;
        print(stats["hp"]);
        GetComponent<SpriteRenderer>().sprite = sprite;
        waypoints = enemy_controller.pathTypeDict[pathType];
        for(int i=0; i < waypoints.Length; i++){
            waypoints[i] = enemy_controller.processWaypt(waypoints[i]);
        }
        transform.position = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        //have we reached the next checkpoint?
        transform.position = Vector3.MoveTowards(transform.position, waypoints[curr_pt], stats["spd"] * Time.deltaTime);
        if(curr_pt < waypoints.Length - 1 && Vector3.Distance(transform.position, waypoints[curr_pt]) < 1e-2){
            curr_pt++;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        print(stats["hp"]);
        Destroy(collision.gameObject);
        stats["hp"]--;
        if(stats["hp"] <= -40){
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}
