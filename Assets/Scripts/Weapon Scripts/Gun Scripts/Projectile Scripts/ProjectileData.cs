using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

#if UNITY_EDITOR
using UnityEditor.UIElements;
#endif

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


#if UNITY_EDITOR
[CustomEditor(typeof(ProjectileData))]
public class MyScriptEditor : Editor
{
    SerializedProperty p_damage;
    SerializedProperty p_damageForce;
    SerializedProperty p_canHitOwner;
    SerializedProperty p_bounces;
    SerializedProperty p_causeExplosion;
    SerializedProperty p_explodeRadius;
    SerializedProperty p_explodeEveryBounce;
    SerializedProperty p_hitscan;
    SerializedProperty p_decayTimerLength;
    SerializedProperty p_instantTravel;
    SerializedProperty p_velocityMagnitude;

    protected virtual void OnEnable()
    {
        p_damage = serializedObject.FindProperty("damage");
        p_damageForce = serializedObject.FindProperty("damageForce");
        p_canHitOwner = serializedObject.FindProperty("canHitOwner");
        p_bounces = serializedObject.FindProperty("bounces");
        p_causeExplosion = serializedObject.FindProperty("causeExplosion");
        p_explodeRadius = serializedObject.FindProperty("explosionRadius");
        p_explodeEveryBounce = serializedObject.FindProperty("explodeEveryBounce");
        p_hitscan = serializedObject.FindProperty("hitscan");
        p_decayTimerLength = serializedObject.FindProperty("decayTimerLength");
        p_instantTravel = serializedObject.FindProperty("instantTravel");
        p_velocityMagnitude = serializedObject.FindProperty("velocityMagnitude");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();  //Set the serialized object stream to the values inside the actual class

        EditorGUILayout.PropertyField(p_damage);
        EditorGUILayout.PropertyField(p_damageForce);
        EditorGUILayout.PropertyField(p_canHitOwner);
        EditorGUILayout.PropertyField(p_bounces);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        //Explosion stuff
        EditorGUILayout.PropertyField(p_causeExplosion);
        GUI.enabled = p_causeExplosion.boolValue.Equals(true);
        EditorGUILayout.PropertyField(p_explodeRadius);
        EditorGUILayout.PropertyField(p_explodeEveryBounce);
        GUI.enabled = true;

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        //Hitscan/Physical stuff
        EditorGUILayout.PropertyField(p_hitscan);
        if (p_hitscan.boolValue.Equals(true))
        {
            EditorGUILayout.PropertyField(p_decayTimerLength);
            EditorGUILayout.PropertyField(p_instantTravel);
        }
        else
        {
            EditorGUILayout.PropertyField(p_velocityMagnitude);
        }

        /*EditorGUILayout.PropertyField(p_causeExplosion);
        if (p_causeExplosion.boolValue.Equals(true))
        {
            EditorGUILayout.PropertyField(p_explodeRadius);
            EditorGUILayout.PropertyField(p_explodeEveryBounce);
        }

        EditorGUILayout.PropertyField(p_hitscan);
        if (p_hitscan.boolValue.Equals(true))
        {
            EditorGUILayout.PropertyField(p_decayTimerLength);
            EditorGUILayout.PropertyField(p_instantTravel);
        }
        else
        {
            EditorGUILayout.PropertyField(p_velocityMagnitude);
        }*/

        serializedObject.ApplyModifiedProperties(); //Take the serialized object stream and apply it to the actual class
    }
}
#endif