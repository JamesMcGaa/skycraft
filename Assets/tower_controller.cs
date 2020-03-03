using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_controller : MonoBehaviour
{

    public GameObject bullet;
    public float next_fire = -1f;
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

    void Shoot(){
      Vector3 bullet_pos = transform.position;
      bullet_pos.y += .5f;
      Instantiate(bullet, bullet_pos, Quaternion.identity);
      Invoke("Shoot", fire_rate);
    }
}
