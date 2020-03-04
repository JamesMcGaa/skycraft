using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TOWER_TYPE {
  BASIC,
  SHOTGUN,
  SNIPER
};

public class tower_controller : MonoBehaviour
{

    public GameObject bullet;
    public TOWER_TYPE type;
    public float fire_rate = .1f;

    // Start is called before the first frame update
    void Start()
    {
      Shoot();
    }

    // Update is called once per frame
    void Update()
    {
      // if (next_fire < 0 || Time.time > next_fire) {
      //     next_fire = Time.time + fire_rate;
      // }
    }

    void Shoot() {
      Vector3 bullet_pos = transform.position;
      switch (type) {
        case TOWER_TYPE.BASIC:
          bullet_pos.y += .5f;
          Instantiate(bullet, bullet_pos, Quaternion.identity);
          break;
        case TOWER_TYPE.SHOTGUN:
          bullet_pos.y += .5f;
          GameObject bullet1 = Instantiate(bullet, bullet_pos, Quaternion.identity);
          bullet1.GetComponent<bullet_controller>().bullet_velocity = new Vector3(-0.01f, 0.1f, 0f);
          GameObject bullet2 = Instantiate(bullet, bullet_pos, Quaternion.identity);
          bullet2.GetComponent<bullet_controller>().bullet_velocity = new Vector3(0f, 0.1f, 0f);
          GameObject bullet3 = Instantiate(bullet, bullet_pos, Quaternion.identity);
          bullet3.GetComponent<bullet_controller>().bullet_velocity = new Vector3(0.01f, 0.1f, 0f);
          break;
        default:
          break;
      }

      Invoke("Shoot", fire_rate);
    }
}
