using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BULLET_TYPE {
  FRIENDLY,
  ENEMY,
};
public class bullet_controller : MonoBehaviour
{

    public Vector3 bullet_velocity;
    public int damage;
    public BULLET_TYPE type;
    public const float BULLET_SURVIVAL_TIME = 1f;

    // Start is called before the first frame update
    void Start()
    {
      Move();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Move()
    {
      Vector3 pos = transform.position;
      pos += bullet_velocity;
      transform.position = pos;
      if(pos.y > skyship_controller.MAX_Y || pos.y < skyship_controller.MIN_Y){
        Destroy(gameObject);
      }

      Invoke("Move", .01f);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.GetComponent<bullet_controller>())
        {
            //should do nothing if bullet to bullet collision but it still has bug
        }
    }

}
