﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public struct tower_stats {
  public tower_stats(TOWER_TYPE tType, float fRate, GameObject tPrefab, GameObject bPrefab, int nBullets, int bDamage) {
    type = tType;
    fireRate = fRate;
    towerPrefab = tPrefab;
    bulletPrefab = bPrefab;
    numBullets = nBullets;
    damage = bDamage;
  }
  public TOWER_TYPE type {get; set;}
  public GameObject towerPrefab {get; set;}
  public GameObject bulletPrefab {get; set;}
  public float fireRate {get; set;}
  public int numBullets {get; set;}
  public int damage {get; set;}
}

public class skyship_controller : MonoBehaviour
{
    private TOWER_TYPE placed = TOWER_TYPE.NULL;
    private float lastUpgradedTime;
    private GameObject leftTower;
    private GameObject rightTower;
    private Vector3 frontTowerOffset = new Vector3(0f, 0.2f, 0f);
    private Vector3 leftTowerOffset = new Vector3(-.4f, .13f, 0f);
    private Vector3 rightTowerOffset = new Vector3(.4f, .13f, 0f);
    private static int money = 0;

    public float acceleration = 4f;
    public float hp = 100f;
    public Vector3 velocity = new Vector3(0f, 0f, 0f);
    public float max_velocity = 6f;

    public Dictionary<string, TOWER_TYPE> hotkeys = new Dictionary<string, TOWER_TYPE>();
    public Dictionary<TOWER_TYPE, int> upgradeLevels = new Dictionary<TOWER_TYPE, int>();

    // public GameObject mainGunProj;
    // public float next_fire = -1f;
    // public float fire_rate = .1f;

    public static float MIN_X = -11.5f;
    public static float MIN_Y = -6f;
    public static float MAX_X = 11.5f;
    public static float MAX_Y = 6f;


    public GameObject basicTower;
    public GameObject shottyTower;
    public GameObject sniperTower;
    public GameObject doubleTower;

    public GameObject basicBullet;
    public GameObject shottyBullet;
    public GameObject sniperBullet;
    public GameObject doubleBullet;

    public TextMeshPro moneyText;

    public Dictionary<TOWER_TYPE, List<tower_stats> > upgradePaths = new Dictionary<TOWER_TYPE, List<tower_stats> >();

    public GameObject explosion;


    void SetStats(GameObject tower, tower_stats stats) {
      tower_controller controller = tower.GetComponent<tower_controller>();
      controller.type = stats.type;
      controller.bulletPrefab = stats.bulletPrefab;
      controller.numBullets = stats.numBullets;
      controller.bullet_damage = stats.damage;
      controller.fire_rate = stats.fireRate;
    }

    void EquipTowers(TOWER_TYPE type, int upgradeLevel) {
      tower_stats stats = upgradePaths[type][upgradeLevel];
      leftTower = Instantiate(stats.towerPrefab, transform.position + leftTowerOffset, Quaternion.identity);
      rightTower = Instantiate(stats.towerPrefab, transform.position + rightTowerOffset, Quaternion.identity);
      SetStats(leftTower, stats);
      SetStats(rightTower, stats);
    }

    // Start is called before the first frame update
    void Start()
    {
      QualitySettings.vSyncCount = 1;
      placed = TOWER_TYPE.NULL;
      lastUpgradedTime = Time.time;

      upgradeLevels[TOWER_TYPE.BASIC] = 0;
      upgradeLevels[TOWER_TYPE.SHOTGUN] = 0;
      upgradeLevels[TOWER_TYPE.SNIPER] = 0;
      upgradeLevels[TOWER_TYPE.DOUBLE] = 0;

      hotkeys["1"] = TOWER_TYPE.BASIC;
      hotkeys["2"] = TOWER_TYPE.SHOTGUN;
      hotkeys["3"] = TOWER_TYPE.SNIPER;
      hotkeys["4"] = TOWER_TYPE.DOUBLE;

      upgradePaths[TOWER_TYPE.BASIC] = new List<tower_stats> {
        new tower_stats(TOWER_TYPE.BASIC, .1f, basicTower, basicBullet, 1, 2),
        new tower_stats(TOWER_TYPE.BASIC, .1f, basicTower, basicBullet, 1, 4),
        new tower_stats(TOWER_TYPE.BASIC, .05f, basicTower, basicBullet, 1, 5)
      };

      upgradePaths[TOWER_TYPE.SHOTGUN] = new List<tower_stats> {
        new tower_stats(TOWER_TYPE.SHOTGUN, .15f, shottyTower, shottyBullet, 3, 1),
        new tower_stats(TOWER_TYPE.SHOTGUN, .12f, shottyTower, shottyBullet, 5, 2),
        new tower_stats(TOWER_TYPE.SHOTGUN, .1f, shottyTower, shottyBullet, 7, 4)
      };

      upgradePaths[TOWER_TYPE.SNIPER] = new List<tower_stats> {
        new tower_stats(TOWER_TYPE.SNIPER, .2f, sniperTower, sniperBullet, 1, 5),
        new tower_stats(TOWER_TYPE.SNIPER, .2f, sniperTower, sniperBullet, 1, 20),
        new tower_stats(TOWER_TYPE.SNIPER, .2f, sniperTower, sniperBullet, 1, 50)
      };

      upgradePaths[TOWER_TYPE.DOUBLE] = new List<tower_stats> {
        new tower_stats(TOWER_TYPE.DOUBLE, .05f, doubleTower, doubleBullet, 1, 2),
        new tower_stats(TOWER_TYPE.DOUBLE, .05f, doubleTower, doubleBullet, 1, 4),
        new tower_stats(TOWER_TYPE.DOUBLE, .025f, doubleTower, doubleBullet, 1, 5)
      };
    }

    // Update is called once per frame

     void Update () {
         Vector3 pos = transform.position;

         moneyText.text = "$ " + money.ToString();

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

        //bounds
        Vector3 clipped = transform.position;
        if(clipped.x > MAX_X){
            clipped.x = MAX_X;
        }
        if(clipped.y > MAX_Y){
            clipped.y = MAX_Y;
        }
        if(clipped.x < MIN_X){
            clipped.x = MIN_X;
        }
        if(clipped.y < MIN_Y){
            clipped.y = MIN_Y;
        }
        transform.position = clipped;

        foreach(string hotkey in hotkeys.Keys) {
          if (Input.GetKey(hotkey) && placed == TOWER_TYPE.NULL) {
            TOWER_TYPE type = hotkeys[hotkey];
            placed = type;
            EquipTowers(type, upgradeLevels[type]);
          }
        }

        if (Input.GetKey("u") && placed != TOWER_TYPE.NULL && (Time.time - lastUpgradedTime > 2.0f)) {
          UpgradeTower(placed);
          lastUpgradedTime = Time.time;
        }

        if (Input.GetKey("space") && placed != TOWER_TYPE.NULL) {
          Destroy(leftTower);
          Destroy(rightTower);
          placed = TOWER_TYPE.NULL;
        }

        if (placed != TOWER_TYPE.NULL) {
          rightTower.transform.position = transform.position + rightTowerOffset;
          leftTower.transform.position = transform.position + leftTowerOffset;
        }

        //SHOOTING
        // if (Input.GetKey ("z") && (next_fire < 0 || Time.time > next_fire)) {
        //     next_fire = Time.time + fire_rate;
        //     Vector3 main_bullet_pos = transform.position;
        //     main_bullet_pos.y += .5f;
        //     Instantiate(mainGunProj, main_bullet_pos, Quaternion.identity);
        // }
     }

     public static void AwardMoney(int amt) {
       money += amt;
     }

     void UpgradeTower(TOWER_TYPE type) {
       int upgradeCost = 100;
       int maxLevel = upgradePaths[type].Count - 1;
       if (upgradeLevels[type] < maxLevel && money >= upgradeCost) {
         money -= upgradeCost;
         upgradeLevels[type]++;
         Destroy(leftTower);
         Destroy(rightTower);
         EquipTowers(type, upgradeLevels[type]);
       }
     }

     void UpdateTowerPos() {

     }
     void OnTriggerEnter2D(Collider2D collision)
     {
        print("start of collision");
        if(collision.gameObject.GetComponent<bullet_controller>())
        {
            int damage = collision.gameObject.GetComponent<bullet_controller>().damage;
            BULLET_TYPE type = collision.gameObject.GetComponent<bullet_controller>().type;
            print("in first if");
            if(type == BULLET_TYPE.ENEMY)
            {
                Destroy(collision.gameObject);
                hp -= damage;
                if(hp <= 0)
                {
                    Destroy(gameObject);
                    if (placed != TOWER_TYPE.NULL) {
                      Destroy(rightTower);
                      Destroy(leftTower);
                    }

                    Instantiate(explosion, transform.position, Quaternion.identity);
                }
                print("skyship taking damage");
            }

        }







     }
}
