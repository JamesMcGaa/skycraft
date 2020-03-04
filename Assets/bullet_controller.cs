﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{

    public Vector3 bullet_velocity;
    public int damage;
    public const float BULLET_SURVIVAL_TIME = 1f;

    // Start is called before the first frame update
    void Start()
    {
      // Destroy(gameObject, BULLET_SURVIVAL_TIME);
    }

    // Update is called once per frame
    void Update()
    {
      Vector3 pos = transform.position;
      pos += bullet_velocity;
      transform.position = pos;
      if(pos.y > skyship_controller.MAX_Y){
        Destroy(gameObject);
      }
    }

}
