using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDisplay : MonoBehaviour
{
    public MeleeBase meleeBase;
    public TrailRenderer[] trails;

    Renderer meleeMesh;
    public Material baseMaterial;
    public int indexOfBladeMaterial;

    public AudioSource swingAS;



    void Start()
    {
        meleeMesh = GetComponent<Renderer>();
    }



    public void ColorBlade(Color bladeColor)
    {
        Material[] tempMaterialsArray = new Material[meleeMesh.materials.Length];
        System.Array.Copy(meleeMesh.materials, tempMaterialsArray, meleeMesh.materials.Length);

        Material bladeMaterial = new Material(baseMaterial);
        bladeMaterial.color = bladeColor;

        tempMaterialsArray[indexOfBladeMaterial] = bladeMaterial;
        meleeMesh.materials = tempMaterialsArray;

        Color trailColor = bladeColor.GetModifiedBrightness(3f);
        foreach (TrailRenderer trail in trails)
        {
            trail.startColor = trailColor;
            trail.endColor = trailColor;
        }
    }

    public void ActivateHitbox()
    {
        meleeBase.ActivateHitbox();
        swingAS.PlayRandomize();
    }

    public void DeactivateHitbox()
    {
        meleeBase.DeactivateHitbox();
    }

    public void ActivateTrails()
    {
        foreach (TrailRenderer trail in trails) trail.gameObject.SetActive(true);
    }

    public void DeactivateTrails()
    {
        foreach (TrailRenderer trail in trails) trail.gameObject.SetActive(false);
    }

    public void ClearDamagedPlayers()
    {
        meleeBase.ClearDamagedPlayers();
    }
}