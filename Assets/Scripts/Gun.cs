using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float ShootPower;
    public Transform Muzzle;
    public GameObject Bullet;
    public GameObject Prefab_Bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        { 
            Bullet = Instantiate(Prefab_Bullet, Muzzle.position, Quaternion.identity);

            Rigidbody2D rigidbody = Bullet.GetComponent<Rigidbody2D>();
            if(rigidbody != null)
            {
                rigidbody.AddForce(Vector3.right * ShootPower);
                Destroy(Bullet, 1);
            }
        }
    }
}
