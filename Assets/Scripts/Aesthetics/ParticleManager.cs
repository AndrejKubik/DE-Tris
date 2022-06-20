using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    #region Singleton
    public static ParticleManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject tapParticle;
    public GameObject winParticle;
    public GameObject obstacleParticle;
    public GameObject failParticle;
    public GameObject levelEndParticle;
}
