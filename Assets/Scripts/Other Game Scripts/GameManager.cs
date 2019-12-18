using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Range(0f, 5f)]
    public float cameraShakeModifier = 1;

    float screenTopEdge;
    float screenBottomEdge;
    float screenLeftEdge;
    float screenRightEdge;



    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);

        UpdateScreenEdges();
    }

    void Update()
    {
        UpdateScreenEdges();
    }



    void UpdateScreenEdges()
    {
        screenTopEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, -(Camera.main.transform.position.z))).y;
        screenBottomEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -(Camera.main.transform.position.z))).y;
        screenLeftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -(Camera.main.transform.position.z))).x;
        screenRightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, -(Camera.main.transform.position.z))).x;
    }

    public bool IsTransformOffCamera(Transform transform)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        if (pos.y > screenTopEdge || pos.y < screenBottomEdge || pos.x < screenLeftEdge || pos.x > screenRightEdge) return true;
        else return false;
    }
}