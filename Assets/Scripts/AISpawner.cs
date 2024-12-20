using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class AIObjects {
    // ------>
    // declare variables
    public string AIGroupName { get { return m_aiGroupName; } }
    public GameObject objectPrefab { get { return m_prefab;}}
    public int spawnAmount { get { return m_maxSpawnAmount;}}
    public bool randomizeStats { get { return m_randomizeStats;}}
    public bool enableSpawner {get { return m_enableSpawner;} }
    

    [Header("AI Group Stats")]
    [SerializeField]
    private string m_aiGroupName;    
    [SerializeField]
    private GameObject m_prefab;    
    [SerializeField]
    [Range(0f, 20f)]
    private int m_maxSpawnAmount;

    [Header("Main Settings")]
    [SerializeField]
    private bool m_enableSpawner;
    [SerializeField]
    private bool m_randomizeStats;
    
    
    public AIObjects(string Name, GameObject Prefab, int MaxAI, int SpawnAmount, bool RandomizeStats){
        this.m_aiGroupName = Name;
        this.m_prefab = Prefab;
        this.m_maxSpawnAmount = SpawnAmount;
        this.m_randomizeStats = RandomizeStats;
    }

    public void setValues(int SpawnAmount){
        this.m_maxSpawnAmount = spawnAmount;
    }
}

public class AISpawner : MonoBehaviour
{
    // ------>
    // declare variables
    public List<Transform> Waypoints = new();

    public float spawnTimer { get {return m_SpawnTimer;}}
    public Vector3 spawnArea {get {return m_SpawnArea;}}
    [Header("Global Stats")]
    [Range(0f, 600f)]
    [SerializeField]
    private float m_SpawnTimer;
    [SerializeField]
    private Color m_SpawnColor = new Color(1.000f, 0.000f, 0.000f, 0.300f);
    [SerializeField]
    private Vector3 m_SpawnArea = new Vector3(20f, 10f, 20f);

    [Header("AI Groups Settings")]
    public AIObjects[] AIObject = new AIObjects[5];
    public bool randomWaypoint = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetWaypoints();
        RandomizeGroups();
        CreateAIGroups();
        SpawnNPC();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SpawnNPC(){
        for(int i =0;i<AIObject.Count();i++){
            if (AIObject[i].enableSpawner && AIObject[i].objectPrefab != null){
                GameObject tempGroup = GameObject.Find(AIObject[i].AIGroupName);
                if (tempGroup.GetComponentInChildren<Transform>().childCount < AIObject[i].spawnAmount){
                    for (int y = 0; y< AIObject[i].spawnAmount; y++){
                        Quaternion randomRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(0, 360), 0);
                        GameObject tempSpawn;
                        tempSpawn = Instantiate(AIObject[i].objectPrefab, RandomPosition(), randomRotation);
                        tempSpawn.transform.parent = tempGroup.transform;
                        tempSpawn.AddComponent<AIMove>();
                    }
                }
            }
        }
    }

    public Vector3 RandomPosition(){
        Vector3 randomPos = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            Random.Range(-spawnArea.y, spawnArea.y),
            Random.Range(-spawnArea.z, spawnArea.z)
        );
        randomPos = transform.TransformPoint(randomPos*.5f);
        return randomPos;
    }

    public Vector3 RandomWaypoint(){
        int randomWP = Random.Range(0, (Waypoints.Count-1));
        Vector3 randomWaypoint = Waypoints[randomWP].transform.position;
        return randomWaypoint;
    }

    void RandomizeGroups(){
        for (int i = 0; i < AIObject.Count(); i++){
            if (AIObject[i].randomizeStats){
                /*AIObject[i] = new AIObjects(
                    AIObject[i].AIGroupName, 
                    AIObject[i].objectPrefab, 
                    Random.Range(1, 30),  
                    Random.Range(1, 10), 
                    true);*/
                AIObject[i].setValues(Random.Range(1, 30));
            }
        } 
    }

    void CreateAIGroups(){
        GameObject m_AIGroupSpawn;
        for (int i =0; i < AIObject.Count(); i++){
            m_AIGroupSpawn = new GameObject(AIObject[i].AIGroupName);
            m_AIGroupSpawn.transform.parent = this.gameObject.transform;
        }
    }

    void GetWaypoints(){
        Transform[] wpList = this.transform.GetComponentsInChildren<Transform>();
        foreach (var wp in wpList) {
            if (wp.CompareTag("waypoint")){
                Waypoints.Add(wp);
            }
        }
    }

    void OnDrawGizmosSelected(){
         Gizmos.color = m_SpawnColor;
        Gizmos.DrawCube(transform.position, spawnArea);
    }
}
