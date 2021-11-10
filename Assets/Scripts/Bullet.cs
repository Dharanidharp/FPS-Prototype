using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - startPosition).magnitude > 20)
        {
            Destroy(gameObject);
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){

            //PlayerController.enemyKills += 1;
            collision.gameObject.GetComponent<Enemy>().DropItem();
            Destroy(collision.gameObject);
            
        }
        Destroy(gameObject);
    }
}
