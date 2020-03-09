using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TOWER_TYPE {
  NULL,
  BASIC,
  SHOTGUN,
  SNIPER,
  DOUBLE
};

public class tower_controller : MonoBehaviour
{
    private bool left;
    public int numBullets;
    public GameObject bulletPrefab;
    public int bullet_damage;
    public TOWER_TYPE type;
    public float fire_rate = .1f;

    // Start is called before the first frame update
    void Start()
    {
      left = true;
      Shoot();
    }

    // Update is called once per frame
    void Update()
    {
      // if (next_fire < 0 || Time.time > next_fire) {
      //     next_fire = Time.time + fire_rate;
      // }
    }

    void ShootBasic() {
      Vector3 bullet_pos = transform.position;
      bullet_pos.y += .5f;
      GameObject bullet = Instantiate(bulletPrefab, bullet_pos, Quaternion.identity);
      bullet.GetComponent<bullet_controller>().bullet_velocity = new Vector3(0, 0.1f, 0f);
      bullet.GetComponent<bullet_controller>().damage = bullet_damage;
    }

    void ShootShotgun() {
      Vector3 bullet_pos = transform.position;
      bullet_pos.y += .5f;
      float spray_width = 0.025f;
      for (int i = 0; i < numBullets; ++i) {
        GameObject bullet = Instantiate(bulletPrefab, bullet_pos, Quaternion.identity);
        float x_velocity = -spray_width + (2*i*spray_width)/(float)(numBullets-1);
        bullet.GetComponent<bullet_controller>().bullet_velocity = new Vector3(x_velocity, 0.1f, 0f);
        bullet.GetComponent<bullet_controller>().damage = bullet_damage;
      }
    }

    void ShootSniper() {
      Vector3 bullet_pos = transform.position;
      bullet_pos.y += .5f;
      GameObject bullet = Instantiate(bulletPrefab, bullet_pos, Quaternion.identity);
      bullet.GetComponent<bullet_controller>().bullet_velocity = new Vector3(0, 0.2f, 0f);
      bullet.GetComponent<bullet_controller>().damage = bullet_damage;
    }

    void ShootDouble() {
      Vector3 bullet_pos = transform.position;
      bullet_pos.y += .5f;
      Vector3 doubleOffset = new Vector3(0.06f, 0, 0);
      if (left) {
        bullet_pos -= doubleOffset;
        left = false;
      } else {
        bullet_pos += doubleOffset;
        left = true;
      }
      GameObject bullet = Instantiate(bulletPrefab, bullet_pos, Quaternion.identity);
      bullet.GetComponent<bullet_controller>().bullet_velocity = new Vector3(0, 0.1f, 0f);
      bullet.GetComponent<bullet_controller>().damage = bullet_damage;
    }

    void Shoot() {
      switch (type) {
        case TOWER_TYPE.BASIC:
          ShootBasic();
          break;
        case TOWER_TYPE.SHOTGUN:
          ShootShotgun();
          break;
        case TOWER_TYPE.SNIPER:
          ShootSniper();
          break;
        case TOWER_TYPE.DOUBLE:
          ShootDouble();
          break;
        default:
          break;
      }

      Invoke("Shoot", fire_rate);
    }
}
