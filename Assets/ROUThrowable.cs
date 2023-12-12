using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ROUThrowable : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public float timeBetweenProjections;
    public float maxTimeRolling;

    [Header("References")]
    public LayerMask bouncesOffOf;
    public GameObject ROUProjection;

    Rigidbody rb;
    float timeRolling = 0;
    float timeSinceLastProjection = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision col)
    {
        //bumped into wall
        if(1<<col.gameObject.layer == bouncesOffOf.value)
        {
            //find the normal of the object bumped into
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, 100f, bouncesOffOf);


            //sets the new rotation]
            Quaternion middleRotation = Quaternion.LookRotation(hit.normal, hit.collider.gameObject.transform.up);

            Vector3 newRot = transform.eulerAngles - middleRotation.eulerAngles;

            transform.eulerAngles = (middleRotation.eulerAngles - newRot) - (Vector3.up * 180);

            //sets the new velocity
            rb.velocity = transform.forward * speed;
        }
    }

    private void Update()
    {
        timeRolling += Time.deltaTime;
        timeSinceLastProjection += Time.deltaTime;

        if (timeBetweenProjections <= timeSinceLastProjection)
        {
            CreateProjection();
            timeSinceLastProjection = 0;
        }

        if(timeRolling >= maxTimeRolling)
        {
            DestroyROU();
        }

    }

    void CreateProjection()
    {
        Instantiate(ROUProjection, transform.position, transform.rotation);
    }

    void DestroyROU()
    {
        Debug.Log("Destorying " + this.name);
        Destroy(this.gameObject);
    }
}
