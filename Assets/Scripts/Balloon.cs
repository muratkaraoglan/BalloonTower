using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public float massCrate;
    public Transform connectedPoint;
    public bool startFly { get; set; }

    public Material[] materials;

    private Rigidbody rb;
    private LineRenderer lineRenderer;

    [SerializeField]
    private bool isReached = false;
    
    private float maxRopeLenght = 5f;
    private Vector3 startScale;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        int matIndex = Random.Range(0, 5);

        GetComponent<MeshRenderer>().material = materials[matIndex];
        startScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        if ( startFly )
        {
            if ( isReached ) return;

            float distance = Vector3.Distance(connectedPoint.position, transform.position);

            if ( distance < maxRopeLenght && !isReached )
            {
                lineRenderer.SetPosition(0, connectedPoint.position);
                lineRenderer.SetPosition(0, transform.position);
                rb.AddForce(-Physics.gravity * (rb.mass + massCrate) + new Vector3(Random.Range(-.5f, .5f), 0f, Random.Range(-.5f, .5f)));
                float t = distance / maxRopeLenght;
                transform.localScale = Vector3.Lerp(startScale, Vector3.one, t);
            }
            else
            {
                rb.useGravity = false;
                isReached = true;
                rb.velocity = Vector3.zero;
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                rb.isKinematic = true;
            }
        }
    }

    bool isDirected = false;
    private void OnCollisionEnter(Collision collision)
    {
        if ( !isReached && !isDirected )
        {
            //transform.position += new Vector3(Random.Range(-.1f, .1f), 0f, Random.Range(-.1f, .1f));
            rb.AddForce(new Vector3(Random.Range(-.1f, .1f), 0f, Random.Range(-.1f, .1f)));
            isDirected = true;
        }

    }
}
