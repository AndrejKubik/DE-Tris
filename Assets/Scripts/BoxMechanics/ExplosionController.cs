using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    #region Singleton
    public static ExplosionController instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private Rigidbody[] boxRigidbody;
    public bool shouldExplode = false;
    public float force;
    public float upForce;
    public float radius;

    public Vector3 positionOffset;

    private void FixedUpdate()
    {
        if(shouldExplode)
        {
            for(int i = 0; i < boxRigidbody.Length; i++)
            {
                if(boxRigidbody[i] != null)
                {
                    boxRigidbody[i].constraints = RigidbodyConstraints.None; //unlock all movement on the current rigidbody
                    boxRigidbody[i].AddExplosionForce(force, positionOffset, radius, upForce); //push all objects around the object away and a bit upwards
                    boxRigidbody[i].useGravity = true; //let the boxes fall
                }
            }
            shouldExplode = false;
        }
    }
}
