using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public float SizeX;
    public GameObject Object;
    public bool Wall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && collider.isTrigger)
            if (!Wall)
            Instantiate(Object, new Vector2(Object.transform.position.x + SizeX, Object.transform.position.y), quaternion.identity, Object.transform.parent);
            else 
                Instantiate(Object, new Vector2(Object.transform.position.x + SizeX, Object.transform.position.y), quaternion.identity, Object.transform);

    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player" && collider.isTrigger)
            if (!Wall)
        Destroy(Object);
    }
}
