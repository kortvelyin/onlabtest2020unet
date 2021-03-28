using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class getplanes : MonoBehaviour
{
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

        Quaternion rot;
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[11];
        for (int z = 0; z < 5; z++)
        {
            // debug.text = "I'm working1";
            MeshFilter newMesh = Instantiate(meshFilterr);
            newMesh.GetComponent<MeshRenderer>().material = mat;
            mesh = newMesh.GetComponent<MeshFilter>().mesh;


            int i;
            for (i = 0; i < 11; i++)
            {
                vertices[i] = new Vector3(pontok[i].x, pontok[i].y, z);
            }

            debug.text = vertices[0].ToString() + vertices[1].ToString() + vertices[2].ToString() + vertices[3].ToString();

            /*vertices[0] = new Vector3(-width, -height);
            vertices[1] = new Vector3(-width, height);
            vertices[2] = new Vector3(width, height);
            vertices[3] = new Vector3(width, -height);*/
            mesh.vertices = vertices;
            //mesh.triangles = new int[] { 0, 2, 3, 0, 1, 2, 0, 3, 4 };

            //material = mat;
            //plane.boundary points now 10
            int[] tria = new int[3 * 10];
            for (int c = 0; c < 9; c++)
            {
                tria[3 * c] = 0;
                tria[3 * c + 1] = c + 1;
                tria[3 * c + 2] = c + 2;
            }
            tria[(3 * 9)] = 0;
            tria[(3 * 9) + 1] = 10;
            tria[(3 * 9) + 2] = 1;
            mesh.triangles = tria;



            /*Mesh zyzmesh = meshFilterr.GetComponent<MeshFilter>().sharedMesh;
            Mesh mesh2 = Instantiate(zyzmesh);
            mesh2 = mesh;
            meshFilterr.GetComponent<MeshFilter>().sharedMesh = mesh2;*/
            // meshFilterr.mesh = mesh;
            // MeshFilter newMesh = Instantiate(meshFilterr);
            // newMesh.mesh = mesh;
            // newMesh.GetComponent<MeshRenderer>().material = mat;
            //meshFilterr.GetComponent<MeshRenderer>().material = mat;
        }
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
        Vector3[] vertices = new Vector3[11];
        var planeManager = GetComponent<ARPlaneManager>();
        
        foreach (ARPlane plane in planeManager.trackables)
        { 
            //debug.text = "I'm working2";
            if (isit)
            {
               
                /*Mesh mesh = new Mesh();
                vectors = plane.boundary;
                Vector3[] vertices = new Vector3[3];
               
                mesh.vertices = vertices;
                mesh.triangles = new int[] { 0, 2, 1 };
                GetComponent<MeshFilter>().mesh = mesh;
                GetComponent<MeshRenderer>().material = mat;*/
                //Mesh mesh = new Mesh();

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
                debug.text = vectors[0].ToString()+ vectors[1].ToString()+ vectors[2].ToString()+ plane.boundary.Length.ToString();
                Debug.Log(vectors[1]);
                isit = false;
                // GetComponent<ARPlaneManager>().enabled = !GetComponent<ARPlaneManager>().enabled;
               /* int i;
                for (i=0; i < plane.boundary.Length; i++)
                    {
                        vertices[i] = new Vector3(vectors[i].x, vectors[i].y);
                    }*/

                
               /* vertices[0] = new Vector3(vectors[0].x, vectors[0].y);
                vertices[1] = new Vector3(vectors[1].x, vectors[1].y);
                vertices[2] = new Vector3(vectors[2].x, vectors[2].y);*/
                vertices[0] = new Vector3(-width, -height);
                vertices[1] = new Vector3(-width, height);
                vertices[2] = new Vector3(width, height);
                vertices[2] = new Vector3(width, -height);

                /* vertices[0] = new Vector3(vectors[0].x, vectors[0].y);
                     vertices[1] = new Vector3(vectors[1].x, vectors[1].y);
                     vertices[2] = new Vector3(vectors[2].x, vectors[2].y);*/
                mesh.vertices = vertices;
                //for (int z = 0; z < plane.boundary.Length; z++)
                
                    //i*3 db tag, 0 0+1 0+2 0 1+1 1+2
                    /*    int[] tria = new int[i];
                    for(int c=0; c< i-2; c++)
                    {
                        tria[3*c] = 0;
                        tria[3*c + 1] = c + 1;
                        tria[3*c + 2] = c + 2;
                    }
                    mesh.triangles = tria;*/
                
               mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
                // m_NoneMeshPrefab.mesh.vertices = vertices;
                // m_NoneMeshPrefab.mesh.triangles = new int[] { 0, 2, 1 };

                meshFilterr.mesh = mesh;
               // meshFilterr.GetComponent<MeshRenderer>().material = mat;
                //m_ARPlaneManager.enabled = !m_ARPlaneManager.enabled;
               // GetComponent<MeshFilter>().mesh = mesh;
               // GetComponent<MeshRenderer>().material = mat;
                //GetComponent<ARPlaneManager>().enabled = !GetComponent<ARPlaneManager>().enabled;
            }

           // if (planeNew == plane)
               // debug.text = "I'm alive";
            // Do something with the ARPlane
        }
    }
}
