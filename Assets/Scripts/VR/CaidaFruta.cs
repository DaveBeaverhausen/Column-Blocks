using UnityEngine;

public class CaidaFruta : MonoBehaviour
{
    public GameObject[] fruitPrefabs;     
    public float spawnInterval = 2f;       
    public float xRange = 5f;   
    public float zRange = 5f;    

    void Start()
    {
         
        InvokeRepeating(nameof(Caida), 3f, spawnInterval);
    }

    void Caida()
    {
        
        int index = Random.Range(0, fruitPrefabs.Length);

        Vector3 spawnPos = new Vector3(
            Random.Range(-xRange, xRange), 
            transform.position.y, 
            Random.Range(-zRange, zRange)
        );

     
        Instantiate(fruitPrefabs[index], spawnPos, Quaternion.identity);
    }
}
