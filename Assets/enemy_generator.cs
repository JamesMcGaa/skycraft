using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_generator : MonoBehaviour
{
	private static Dictionary<string, float> probabilities;
	private static float sum = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public static void set(string name, float to){
    	if(probabilities.containsKey(name)){
    		sum -= probabilities[name];
    	}
    	probabilities[name] = to;
    	sum += to;
    }
    public static string getOutcome(){
    	float myrand = (float)Random.NextDouble() * sum;
    	foreach(var item in myDictionary)
		{
		  string na = item.Key;
		  float myprob = item.Value;

		}
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
