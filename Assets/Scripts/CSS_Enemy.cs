using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_Enemy : MonoBehaviour
{

    [Header("Enemy Stats")]
    public float movementSpeed = 3.0f;
    private Vector2 moveDown2D = new Vector2(0, -1);
    //private Vector3 moveDown3D = new Vector3(0, -1, 0);

    public int waypointPos = 1;     // Skip 0 as they spawn at 0
    public int movementPatternID;

    // Start is called before the first frame update
    void Start()
    {
        //movementPatternID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.SetMovementPattern(this.movementPatternID);
        
    }

    public void SetMovementID(int _id)
    {
        this.movementPatternID = _id;
    }

    void SetMovementPattern(int _id)
    {
        switch (_id)
        {
            case 0: // Basic Enemy moving down
                {
                    // Simple Basic move down 
                    // For any items, basic items that needs to move down to off the screen
                    this.transform.position = this.transform.position + new Vector3(0, -1 * this.movementSpeed * Time.deltaTime, 0);
                    break;
                }
            case 1:
                {
                    //CSS_Spawn.Instance.GetAtkRun01();

                    // MoveTowards follows towards target
                    // For more advance movement patterns that requires following waypoints 
                    // or targeting the player itself.

                    Transform[] tempPattern = CSS_Spawn.Instance.GetAtkRun01();
                    this.transform.position = Vector2.MoveTowards(this.transform.position,
                                                                tempPattern[this.waypointPos].position, 
                                                                this.movementSpeed * Time.deltaTime);

                    if(Vector2.Distance(transform.position, tempPattern[this.waypointPos].position) <= 0.1f)
                    {
                        if(this.waypointPos < tempPattern.Length - 1)
                        {
                            this.waypointPos++;
                        }
                        else
                        {
                            // Simple delete
                            Destroy(this.gameObject);
                        }
                    }

                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
