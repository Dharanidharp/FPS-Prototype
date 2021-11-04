using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddTorque(new Vector3(0, 100, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.resource += 100;
            Debug.Log("Resource: " + PlayerController.resource);
            Destroy(gameObject);
        }
    }
}
