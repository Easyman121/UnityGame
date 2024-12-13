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
    public int maxAI { get { return m_maxAI;} }
    public int spawnRate { get { return m_spawnRate;}}
    public int spawnAmount { get { return m_maxSpawnAmount;}}
    public bool randomizeStats { get { return m_randomizeStats;}}

    [Header("AI Group Stats")]
    [SerializeField]
    private string m_aiGroupName;    
    [SerializeField]
    private GameObject m_prefab;    
    [SerializeField]
    [Range(0f, 30f)]
    private int m_maxAI;
    [SerializeField]
    [Range(0f, 20f)]
    private int m_spawnRate;
    [SerializeField]
    [Range(0f, 10f)]
    private int m_maxSpawnAmount;
    [SerializeField]
    private bool m_randomizeStats;
    
    public AIObjects(string Name, GameObject Prefab, int MaxAI, int SpawnRate, int SpawnAmount, bool RandomizeStats){
        this.m_aiGroupName = Name;
        this.m_prefab = Prefab;
        this.m_maxAI = MaxAI;
        this.m_spawnRate = SpawnRate;
        this.m_maxSpawnAmount = SpawnAmount;
        this.m_randomizeStats = RandomizeStats;
    }

    public void setValues(int MaxAI, int SpawnRate, int SpawnAmount){
        this.m_maxAI = MaxAI;
        this.m_spawnRate = SpawnRate;
        this.m_maxSpawnAmount = SpawnAmount;
    }
}

public class AISpawner : MonoBehaviour
{
    // ------>
    // declare variables
    public List<Transform> Waypoints = new();
    [Header("AI Groups Settings")]
    public AIObjects[] AIObject = new AIObjects[5];
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetWaypoints();
        RandomizeGroups();
        CreateAIGroups();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void RandomizeGroups(){
        for (int i = 0; i < AIObject.Count(); i++){
            if (AIObject[i].randomizeStats){
                /*AIObject[i] = new AIObjects(
                    AIObject[i].AIGroupName, 
                    AIObject[i].objectPrefab, 
                    Random.Range(1, 30), 
                    Random.Range(1, 20), 
                    Random.Range(1, 10), 
                    true);*/
                AIObject[i].setValues(Random.Range(1, 30), Random.Range(1, 20), Random.Range(1, 10));
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
}
