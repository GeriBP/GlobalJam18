using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCenterCamera : MonoBehaviour {
    public List<Transform> targets;
    public Vector3 offset;
    public float smooth, smooth2;
    public float minZoom, maxZoom, zoomLimiter;
    private Camera cam;
    public float size;

    private void Start()
    {
        cam = Camera.main;
        PlayerMove[] player = GameObject.FindObjectsOfType<PlayerMove>();
        for (int i = 0; i < player.Length; i++)
        {
            targets.Add(player[i].transform);
        }
    }

    void FixedUpdate () {
        if (targets.Count == 0) return;
        Vector3 centerPoint = GetCenterPosition() + offset;
        transform.position = Vector3.Lerp(transform.position, centerPoint, smooth);

        Zoom();
    }

    void Zoom()
    {
        Bounds bound = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bound.Encapsulate(targets[i].position);
        }
        size = bound.size.x;
        float newZoom = Mathf.Lerp(maxZoom, minZoom, size / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, smooth2); ;
    }

    Vector3 GetCenterPosition()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        Bounds bound = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bound.Encapsulate(targets[i].position);
        }

        return bound.center;
    }
}
