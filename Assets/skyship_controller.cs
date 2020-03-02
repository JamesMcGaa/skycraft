﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyship_controller : MonoBehaviour
{
    private bool placed; // will become better data structure
    private GameObject frontTower;
    public Vector3 frontTowerOffset = new Vector3(0f, 0.2f, 0f);


    public float acceleration = 4f;
    public float hp = 100f;
    public Vector3 velocity = new Vector3(0f, 0f, 0f);
    public float max_velocity = 6f;

    // public GameObject mainGunProj;
    // public float next_fire = -1f;
    // public float fire_rate = .1f;

    public GameObject tower;

    // Start is called before the first frame update
    void Start()
    {
      placed = false;
    }

    // Update is called once per frame

     void Update () {
         Vector3 pos = transform.position;

        //use this to see if we are moving diagonally, and then slow down
         int count = 0;

         //no movement, deccelerate
         if(!Input.GetKey("up") || !Input.GetKey("left") || !Input.GetKey("down") || !Input.GetKey ("right")){
             velocity /= 2f;
         }

        //accelerate
         if (Input.GetKey ("up")) {
             velocity.y += acceleration * Time.deltaTime;
             if(velocity.y > max_velocity){
                 velocity.y = max_velocity;
             }
             count++;
         }
         if (Input.GetKey ("down")) {
             velocity.y -= acceleration * Time.deltaTime;
             if(velocity.y < -max_velocity){
                 velocity.y = -max_velocity;
             }
             count++;
         }
         if (Input.GetKey ("right")) {
             velocity.x += acceleration * Time.deltaTime;
             if(velocity.x > max_velocity){
                 velocity.x = max_velocity;
             }
             count++;
         }
         if (Input.GetKey ("left")) {
             velocity.x -= acceleration * Time.deltaTime;
             if(velocity.x < -max_velocity){
                 velocity.x = -max_velocity;
             }
             count++;
         }

        //move
        if(count > 1){
         transform.position += velocity / 1.4f;
        }
        else{
         transform.position += velocity;
        }

        if (Input.GetKey("space") && !placed) {
          placed = true;
          frontTower = Instantiate(tower, transform.position + frontTowerOffset, Quaternion.identity);
        }

        if (placed) {
          frontTower.transform.position = transform.position + frontTowerOffset;
        }

        //SHOOTING
        // if (Input.GetKey ("z") && (next_fire < 0 || Time.time > next_fire)) {
        //     next_fire = Time.time + fire_rate;
        //     Vector3 main_bullet_pos = transform.position;
        //     main_bullet_pos.y += .5f;
        //     Instantiate(mainGunProj, main_bullet_pos, Quaternion.identity);
        // }
     }
}
