using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.Networking;

public class getplanes : MonoBehaviour
{
    public GameObject meshF;
    public ARPlaneManager planeManager;
    //public ARMeshManager m_MeshManager;
    public MeshFilter meshFilterr;
    public Material mat;
    float width = 1;
    float height = 1;
    //ARPlaneManager m_ARPlaneManager;
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
    Vector3[] pontok = new Vector3[11];
    float speed = 100.0f;
    // Start is called before the first frame update

    void Start()
    {
        pontok[0].x = 0;
        pontok[0].y = 0;
        pontok[1].x = 0;
        pontok[1].y = -6;
        pontok[2].x = -3;
        pontok[2].y = -6;
        pontok[3].x = -6;
        pontok[3].y = -3;
        pontok[4].x = -6;
        pontok[4].y = 0;
        pontok[5].x = -6;
        pontok[5].y = 3;
        pontok[6].x = -3;
        pontok[6].y = 6;
        pontok[7].x = 0;
        pontok[7].y = 6;
        pontok[8].x = 6;
        pontok[8].x = 3;
        pontok[9].x = 6;
        pontok[9].y = -3;
        pontok[10].x = 3;
        pontok[10].y = -6;


        

        
            
            
           
            

            

           
           

            

           

           

           
        
        
        
       

         

        
        /*  var planeManager = GetComponent<ARPlaneManager>();
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
          }*/
    }

    // Update is called once per frame
    
    void Update()
    {

        Mesh mesh = new Mesh();

        var planeManager = GetComponent<ARPlaneManager>();

        foreach (ARPlane plane in planeManager.trackables)
        { 
            //debug.text = "I'm working2";
            if (true)
            {
                //Vector3[] vertices = new Vector3[3];
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
                debug.text = vectors[0].ToString()+ vectors[1].ToString()+ vectors[2].ToString()+ plane.boundary.Length.ToString()+"center"+plane.center.ToString()+"position"+plane.transform.position.ToString()+"rotation"+plane.transform.rotation;
                Debug.Log(vectors[1]);
                isit = false;
                Vector3[] vertices = new Vector3[plane.boundary.Length];
                //position és rotation miatt jobb ha gameobject

                GameObject newMeshF = Instantiate(meshF);
               
                int i;
                for (i = 0; i < plane.boundary.Length; i++)
                {
                    vertices[i] = new Vector3(vectors[i].x,0, vectors[i].y);
                }
                mesh.vertices = vertices;

                int[] tria = new int[3 *( plane.boundary.Length-2)];
                for (int c = 0; c < plane.boundary.Length - 2; c++)
                {
                    tria[3 * c] = 0;
                    tria[3 * c + 1] = c + 1;
                    tria[3 * c + 2] = c + 2;
                }
                /*tria[(3 * 9)] = 0;
                tria[(3 * 9) + 1] = 10;
                tria[(3 * 9) + 2] = 1;*/
                mesh.triangles = tria;
                mesh.RecalculateNormals();
                newMeshF.GetComponent<MeshFilter>().mesh = mesh;
                newMeshF.GetComponent<MeshRenderer>().material = mat;
                newMeshF.transform.position = plane.transform.position;
                newMeshF.transform.rotation = plane.transform.rotation;
                isit = false;
            }

           // if (planeNew == plane)
               // debug.text = "I'm alive";
            // Do something with the ARPlane
        }
    }
}
