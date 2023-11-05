using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerVFXManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem BounceSparkVFX;
    public void BounceSpark()
    {
        // BounceSparkVFX.Clear();
        BounceSparkVFX.Play();
    }
}
