using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject dropItem;

    public NavMeshAgent agent;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            agent.SetDestination(target.transform.position);
    }

    void move(float speed)
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * speed);
    }

    void turn(float speed)
    {
        Vector3 dir = PlayerController.playerPosition - transform.position;
        dir.y = 0;
        Quaternion q = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, speed);
    }

    private void FixedUpdate()
    {

        float angle = Vector3.Angle(PlayerController.playerPosition - transform.position, transform.forward);
        if (angle < 25f || (PlayerController.playerPosition - transform.position).magnitude < 2)
        {
            GetComponent<Animator>().SetBool("is walk", true);
            move(5f);
        }
        else
        {
            GetComponent<Animator>().SetBool("is walk", false);
            turn(0.3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }

    public void DropItem()
    {
        Debug.Log(Random.Range(0f, 1f));
        if (Random.Range(0f, 1f) > 0.66f)
        {
            var item = Instantiate(dropItem, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(45, 45, 45));
            item.transform.parent = null;
        }
    }


}
