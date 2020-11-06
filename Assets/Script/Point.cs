using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.gameObject.SetActive(false);
            PlayManager.Instance.ScoreValue(1);
        }
    }
}
