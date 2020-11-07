using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorReSpawn : MonoBehaviour
{
  
    void Start()
    {

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SetActiveFalse();
            PlayManager.Instance.SpawnFloor();
            
        }
    }

    void SetActiveFalse()
    {
        transform.root.gameObject.SetActive(false);
        PlayManager.Instance.Floorpool.Push(transform.root.gameObject);
        
    }
   
   
}
