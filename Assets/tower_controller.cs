using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TOWER_TYPE {
  BASIC,
  SHOTGUN,
  SNIPER,
  DOUBLE
};

public class tower_controller : MonoBehaviour
{
    private bool left;
    public GameObject bullet;
    public TOWER_TYPE type;
    public float fire_rate = .1f;

    // Start is called before the first frame update
    void Start()
    {
      left = true;
      switch (type) {
        case TOWER_TYPE.BASIC:
          fire_rate = .1f;
          break;
        case TOWER_TYPE.SHOTGUN:
          fire_rate = .1f;
          break;
        case TOWER_TYPE.SNIPER:
          fire_rate = .3f;
          break;
        case TOWER_TYPE.DOUBLE:
          fire_rate = .05f;
          break;
        }
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
          GameObject bullet0 = Instantiate(bullet, bullet_pos, Quaternion.identity);
          bullet0.GetComponent<bullet_controller>().bullet_velocity = new Vector3(0, 0.1f, 0f);
          bullet0.GetComponent<bullet_controller>().damage = 2;
          break;
        case TOWER_TYPE.SHOTGUN:
          bullet_pos.y += .5f;
          GameObject bullet1 = Instantiate(bullet, bullet_pos, Quaternion.identity);
          bullet1.GetComponent<bullet_controller>().bullet_velocity = new Vector3(-0.01f, 0.1f, 0f);
          bullet1.GetComponent<bullet_controller>().damage = 1;
          GameObject bullet2 = Instantiate(bullet, bullet_pos, Quaternion.identity);
          bullet2.GetComponent<bullet_controller>().bullet_velocity = new Vector3(0f, 0.1f, 0f);
          bullet2.GetComponent<bullet_controller>().damage = 1;
          GameObject bullet3 = Instantiate(bullet, bullet_pos, Quaternion.identity);
          bullet3.GetComponent<bullet_controller>().bullet_velocity = new Vector3(0.01f, 0.1f, 0f);
          bullet3.GetComponent<bullet_controller>().damage = 1;
          break;
        case TOWER_TYPE.SNIPER:
          bullet_pos.y += .5f;
          GameObject bullet4 = Instantiate(bullet, bullet_pos, Quaternion.identity);
          bullet4.GetComponent<bullet_controller>().bullet_velocity = new Vector3(0, 0.2f, 0f);
          bullet4.GetComponent<bullet_controller>().damage = 100;
          break;
        case TOWER_TYPE.DOUBLE:
          bullet_pos.y += .5f;
          Vector3 doubleOffset = new Vector3(0.06f, 0, 0);
          if (left) {
            bullet_pos -= doubleOffset;
            left = false;
          } else {
            bullet_pos += doubleOffset;
            left = true;
          }
          GameObject bullet5 = Instantiate(bullet, bullet_pos, Quaternion.identity);
          bullet5.GetComponent<bullet_controller>().bullet_velocity = new Vector3(0, 0.1f, 0f);
          bullet5.GetComponent<bullet_controller>().damage = 1;
          break;
        default:
          break;
      }

      Invoke("Shoot", fire_rate);
    }
}
