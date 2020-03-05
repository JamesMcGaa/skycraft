using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyship_controller : MonoBehaviour
{
    private bool placed;
    private GameObject leftTower;
    private GameObject rightTower;
    private Vector3 frontTowerOffset = new Vector3(0f, 0.2f, 0f);
    private Vector3 leftTowerOffset = new Vector3(-.4f, .13f, 0f);
    private Vector3 rightTowerOffset = new Vector3(.4f, .13f, 0f);


    public float acceleration = 4f;
    public float hp = 100f;
    public Vector3 velocity = new Vector3(0f, 0f, 0f);
    public float max_velocity = 6f;

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

    // Start is called before the first frame update
    void Start()
    {
    QualitySettings.vSyncCount = 1;
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


        if (Input.GetKey("1") && !placed) {
          placed = true;
          leftTower = Instantiate(basicTower, transform.position + leftTowerOffset, Quaternion.identity);
          rightTower = Instantiate(basicTower, transform.position + rightTowerOffset, Quaternion.identity);
        } else if (Input.GetKey("2") && !placed) {
          placed = true;
          leftTower = Instantiate(shottyTower, transform.position + leftTowerOffset, Quaternion.identity);
          rightTower = Instantiate(shottyTower, transform.position + rightTowerOffset, Quaternion.identity);
        } else if (Input.GetKey("3") && !placed) {
          placed = true;
          leftTower = Instantiate(sniperTower, transform.position + leftTowerOffset, Quaternion.identity);
          rightTower = Instantiate(sniperTower, transform.position + rightTowerOffset, Quaternion.identity);
        } else if (Input.GetKey("4") && !placed) {
          placed = true;
          leftTower = Instantiate(doubleTower, transform.position + leftTowerOffset, Quaternion.identity);
          rightTower = Instantiate(doubleTower, transform.position + rightTowerOffset, Quaternion.identity);
        }

        if (Input.GetKey("space") && placed) {
          Destroy(leftTower);
          Destroy(rightTower);
          placed = false;
        }

        if (placed) {
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

     void UpdateTowerPos() {

     }
}
