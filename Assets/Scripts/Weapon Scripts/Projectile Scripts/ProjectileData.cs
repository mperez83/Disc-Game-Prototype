using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

public class ProjectileData : MonoBehaviour
{
    //General properties
    public int damage;
    public float damageForce;
    public bool canHitOwner;
    public int bounces;

    public bool causeExplosion;
    public float explosionRadius;
    public bool explodeEveryBounce;

    public bool hitscan;

    //Hitscan properties
    public float decayTimerLength;
    float decayTimer;
    public bool instantTravel;

    //Physical properties
    public float velocityMagnitude;
}



[CustomEditor(typeof(ProjectileData))]
public class MyScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ProjectileData script = target as ProjectileData;

        //Selectively show fields depending on if hitscan is toggled
        //script.hitscan = GUILayout.Toggle(script.hitscan, "Hitscan");
        script.hitscan = EditorGUILayout.Toggle("Hitscan", script.hitscan);
        if (script.hitscan)
        {
            script.decayTimerLength = EditorGUILayout.FloatField("Decay Timer Length:", script.decayTimerLength);
            script.instantTravel = EditorGUILayout.Toggle("Instant Travel:", script.instantTravel);
        }
        else
        {
            script.velocityMagnitude = EditorGUILayout.FloatField("Velocity Magnitude:", script.velocityMagnitude);
        }
    }
}