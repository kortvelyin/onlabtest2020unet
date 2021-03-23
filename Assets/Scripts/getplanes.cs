using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class getplanes : MonoBehaviour
{
    ARPlaneManager m_ARPlaneManager;
    ARPlane planeNew;
    public Text debug;
    int idk;
    Vector3 smt;
    Quaternion identity;
    Transform any;
    bool isit=true;
    Vector2[] smt2;
    List <Vector2> list;
    Unity.Collections.NativeArray<Vector2> vectors;
    // Start is called before the first frame update
    
    void Start()
    {
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
        var planeManager = GetComponent<ARPlaneManager>();
        foreach (ARPlane plane in planeManager.trackables)
        {
            debug.text = "I'm working";
            if (isit)
            {
                // Do something with the ARPlane
                //plane.alignment;
                smt = plane.transform.localPosition;
                identity = plane.transform.localRotation;
                smt = plane.transform.localScale;
                // smt = plane.transform.localToWorldMatrix;
                smt = plane.transform.position;
                identity = plane.transform.rotation;
                any = plane.transform;

                // smt = plane.transform.worldToLocalMatrix;
                smt = plane.size;
                smt = plane.normal;
                // plane.nativePtr;
                // smt = plane.infinitePlane;
                isit = plane.enabled;
                smt = plane.centerInPlaneSpace;
                smt = plane.center;
                // smt2 = plane.boundary;
                // smt = plane.boundary.Length;
                // smt2 = plane.boundary;
                //vectors=plane.boundary.ToArray;
                vectors = plane.boundary;
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        var planeManager = GetComponent<ARPlaneManager>();
        foreach (ARPlane plane in planeManager.trackables)
        { 
            debug.text = "I'm working2";
            if (isit||plane!=null)
            {
                planeNew = plane;
                // Do something with the ARPlane
                //plane.alignment;
                smt = plane.transform.localPosition;
                identity = plane.transform.localRotation;
                smt = plane.transform.localScale;
                // smt = plane.transform.localToWorldMatrix;
                smt = plane.transform.position;
                identity = plane.transform.rotation;
                any = plane.transform;

                // smt = plane.transform.worldToLocalMatrix;
                smt = plane.size;
                smt = plane.normal;
                // plane.nativePtr;
                // smt = plane.infinitePlane;
                isit = plane.enabled;
                smt = plane.centerInPlaneSpace;
                smt = plane.center;
                // smt2 = plane.boundary;
                // smt = plane.boundary.Length;
                // smt2 = plane.boundary;
                //vectors=plane.boundary.ToArray;
                vectors = plane.boundary;
                Debug.Log(vectors[1]);
                //isit = false;
                debug.text = vectors[1].ToString();
                Debug.Log(vectors[1]);
                isit = false;
                m_ARPlaneManager.enabled = !m_ARPlaneManager.enabled;

                //GetComponent<ARPlaneManager>().enabled = !GetComponent<ARPlaneManager>().enabled;
            }

            if (planeNew == plane)
                debug.text = "I'm alive";
            // Do something with the ARPlane
        }
    }
}
