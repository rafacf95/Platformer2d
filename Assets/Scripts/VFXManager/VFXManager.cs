using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class VFXManager : Singleton<VFXManager>
{
    public enum VFXType
    {
        JUMP,
        VFX_2
    }
    public List<VFXManagerSetup> vfxSetup;

    public void PlayVFXByType(VFXType vfxType, Vector3 position)
    {
        foreach(var i in vfxSetup)
        {
            if(i.vfxType == vfxType)
            {
                var item = Instantiate(i.prefab);
                item.transform.position = position;
                item.GetComponent<ParticleSystem>().Play();
                // Destroy(item.gameObject, item.GetComponent<ParticleSystem>().main.duration);
                Destroy(item.gameObject, 2f);
                break;
            }
        }
    }
}

[System.Serializable]
public class VFXManagerSetup
{
    public VFXManager.VFXType vfxType;
    public GameObject prefab;
}