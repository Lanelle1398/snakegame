using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//received help from sources: https://youtu.be/U8gUnpeaMbQ &&  https://youtu.be/8ztq9fQT6Kc
//code has been significantly modified
public class Food : MonoBehaviour
{
    float min_x=(float)-11f, max_x=(float)11f, min_z=(float)-9f, max_z=(float)9f;
    float y_pos=(float)0f;
    public TextMeshProUGUI score_Text;
    public int scoreCount;
    void Start()
    {
        RandomizePosition();
    }

    void RandomizePosition(){
         this.transform.position=new Vector3(Random.Range(min_x, max_x), y_pos,Random.Range(min_z, max_z)); 
    }
    // Update is called once per frame
   void OnTriggerEnter(Collider other){
      Debug.Log(gameObject.tag + " entered Trigger tagged " + other.gameObject.tag);
       if(other.tag=="Player"){
           RandomizePosition();
           scoreCount++;
            score_Text.SetText("Score:"+ scoreCount);
       }
   }
}
