using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorReSpawn : MonoBehaviour
{
    public GameObject point;
    public Vector3 size;
    void Start()
    {
       
        for (int i = 0; i < 20; i++)
        {
            SpawnPoint();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayManager.Instance.SpawnFloor();
            SetActiveFalse();
        }
    }

    void SetActiveFalse()
    {
        PlayManager.Instance.Floorpool.Push(transform.root.gameObject);
        transform.root.gameObject.SetActive(false);
    }
    public void SpawnPoint()
    {
        Vector3 pos = new Vector3(transform.position.x,transform.position.y+1,transform.position.z) + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));

        GameObject temp=Instantiate(point, pos, Quaternion.identity);
        temp.transform.SetParent(transform);
    }
   
}
