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
    public List<Vector3> waypoints = new List<Vector3>();
    private Dictionary<string, int> stats; //we need hp, attack rate, damage, speed
    private int curr_pt = 1;

    public enemy(int ty, int pathty){
        enemyType = ty;
        pathType = pathty;
    }
    //use this only for defining enemy archetypes
    public enemy(Sprite spr, Dictionary<string, int> sts){
        sprite = spr;
        stats = new Dictionary<string, int>(sts);
    }

    void Start()
    {
        enemy archetype = enemy_controller.enemyTypeDict[enemyType];
        sprite = archetype.sprite;
        stats = new Dictionary<string, int>(archetype.stats);
        print(stats["hp"]);
        GetComponent<SpriteRenderer>().sprite = sprite;
        Vector3[] path = enemy_controller.pathTypeDict[pathType];
        for(int i=0; i < path.Length; i++){
            waypoints.Add(enemy_controller.processWaypt(path[i]));
        }
        print(waypoints[0]);
        transform.position = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        //have we reached the next checkpoint?
        transform.position = Vector3.MoveTowards(transform.position, waypoints[curr_pt], stats["spd"] * Time.deltaTime);
        if(curr_pt < waypoints.Count - 1 && Vector3.Distance(transform.position, waypoints[curr_pt]) < 1e-2){
            curr_pt++;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        print(stats["hp"]);
        int damage = collision.gameObject.GetComponent<bullet_controller>().damage;
        Destroy(collision.gameObject);
        stats["hp"]-= damage;
        if(stats["hp"] <= 0){
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}
