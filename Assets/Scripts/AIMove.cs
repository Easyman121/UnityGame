using Unity.VisualScripting;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    private AISpawner m_AIManager;
    private bool m_hasTarget = false;
    private bool m_isTurning;

    private Vector3 m_wayPoint;
    private Vector3 m_lastWaypoint = new Vector3(0f, 0f, 0f);
    
    private Animator m_animator;
    private float m_speed;
    private float m_scale;

    private Collider m_collider;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_AIManager = transform.parent.GetComponentInParent<AISpawner>();
        m_animator = GetComponent<Animator>();

        //SetUpNPC();
    }

    
    void SetUpNPC(){
        // Change the scale of NPC 
        /*
        float m_scale = Random.Range(.1f, .5f);
        transform.localScale = new Vector3(m_scale, m_scale, m_scale);*/
        
        // Set collider
        if (transform.GetComponent<Collider>() != null && transform.GetComponent<Collider>().enabled){
            m_collider = transform.GetComponent<Collider>();
            
        }
        else if (transform.GetComponentInChildren<Collider>() != null && transform.GetComponentInChildren<Collider>().enabled){
            m_collider = transform.GetComponentInChildren<Collider>();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_hasTarget){
            m_hasTarget = CanFindTarget();
        }
        else{
            RotateNPC(m_wayPoint, m_speed);
            transform.position = Vector3.MoveTowards(transform.position, m_wayPoint, m_speed*Time.deltaTime);

            //CollidedNPC();
        }

        if (transform.position == m_wayPoint){
            m_hasTarget = false;
        }
    }

    void CollidedNPC(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, transform.localScale.z)){
            if (hit.collider == m_collider | hit.collider.tag == "waypoint"){
                return;
            }
        }
        int randomNum = Random.Range(1, 100);
        if (randomNum < 40) m_hasTarget = false; 
    }

    Vector3 GetWaypoint(bool isRandom){
        if (isRandom){
            return m_AIManager.RandomPosition();
        }
        else{
            return m_AIManager.RandomWaypoint();
        }
    }

    bool CanFindTarget(float start = 0.2f, float end = 0.8f){
        
        if (m_lastWaypoint == m_wayPoint){
            m_wayPoint=GetWaypoint(m_AIManager.randomWaypoint);
            return false;
        }
        else{
            m_lastWaypoint=m_wayPoint;
            m_speed = Random.Range(start, end);
            m_animator.speed = m_speed;
            return true;
        }
    }

    void RotateNPC(Vector3 waypoint, float currentSpeed){
        float TurnSpeed = currentSpeed * Random.Range(1f, 3f);

        Vector3 LookAt = waypoint - this.transform.position;
        transform.rotation= Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookAt), TurnSpeed*Time.deltaTime);
    }
}
