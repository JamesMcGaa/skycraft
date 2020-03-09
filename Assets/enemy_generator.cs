using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemy_generator : MonoBehaviour
{
	private Dictionary<KeyValuePair<int,int>, float> probabilities = new Dictionary<KeyValuePair<int,int>, float>(); //<enemy_type, probabilityOfSpawn>
	private float sum = 0f; //in case probabilities dont sum to 1
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void set(int enemyid, int pathid, float to){
        KeyValuePair<int,int> pa = new KeyValuePair<int,int>(enemyid, pathid);
    	if(probabilities.ContainsKey(pa)){
    		sum -= probabilities[pa];
    	}
    	probabilities[pa] = to;
    	sum += to;
    }
    public KeyValuePair<int,int> getOutcome(){
    	float myrand = Random.Range(0.0f, 1.0f) * sum;
    	foreach(var item in probabilities)
		{
		  KeyValuePair<int,int> na = item.Key;
		  float myprob = item.Value;
          if(myrand <= myprob){
            return na;
          }else{
            myrand -= myprob;
          }
		}

        return new KeyValuePair<int,int> (-1,-1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
