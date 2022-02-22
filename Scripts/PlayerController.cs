using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
//Source: //https://youtu.be/mbzXIOKZurA      
{
  public float speedMovement=20f; //movement of player
  //public GameObject playerPrefab; 
  public LayerMask stopMovement;

  Vector3 targetposition;  
  Vector3 startPosition;
  public Transform point;//move point to  the position we want to move next on the grid //target position we want to go to


    // Start is called before the first frame update
    void Start()
    {
        point.parent=null;
    }

    // Update is called once per frame
    void Update()
    {  
  
      //only move when the player is at the point we're going to
    //the play moves towards the point we're going to so we're transforming the player's position from current point to target point 
      transform.position=Vector3.MoveTowards(transform.position, point.position, speedMovement * Time.deltaTime); //transform.position is current position, point.position is target poin
        if(Vector3.Distance(transform.position, point.position)<=0.03){ //if our current position and target position is less  than 0.03 apart 
          if((Input.GetAxisRaw("Horizontal")==1f) || (Input.GetAxisRaw("Horizontal")==-1f)  && !Input.GetKeyDown(KeyCode.A)){ //if we're pressing  all the way to the right or left  
                if (!Physics2D.OverlapCircle(point.position+new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f),0.3f, stopMovement)){ //physics 2d overlap circle draws an imagainary circle around a particular space and checks for colliders in this area
                point.position+=new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f); //x,y,z //move point to  the left or right by one unit
                }
            }

           if(Input.GetAxisRaw("Vertical")==1f|| Input.GetAxisRaw("Vertical")==-1f ){ //if we're pressing  all the way up or down
                if (!Physics2D.OverlapCircle(point.position+new Vector3(0f, 0f,  Input.GetAxisRaw("Vertical")),0.3f, stopMovement)){
                //trying to move ball back or forth  by one unit
                point.position+=new Vector3(0f,  0f, Input.GetAxisRaw("Vertical")); //x,y,z               
                }         
            }
          
        //    if(Input.GetKeyDown(KeyCode.B)){
        //     if(!Physics2D.OverlapCircle(point.position+new Vector3(0f, 1f, 0f),0.3f, stopMovement)){
        //       //try to move the ball up or down by one unit- y axis
        //       point.position+=new Vector3(0f, 1f, 0f); //x,y,z       
        //     }

        //   }

        //     if(Input.GetKeyDown(KeyCode.X)){
        //        if(!Physics2D.OverlapCircle(point.position+new Vector3(0f, -1f, 0f),0.3f, stopMovement)){
        //       //try to move the ball up or down by one unit- y axis
        //       point.position+=new Vector3(0f, -1f, 0f); //x,y,z       
        //      }
        //     }

        }
     
    }


}