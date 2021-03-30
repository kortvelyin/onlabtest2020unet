using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.Networking;

public class getplanes : NetworkBehaviour
{
    //[SyncVar]
    //public int[] tria;
    public GameObject Cube;
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


        
     
    }

   
    // Update is called once per frame
    bool kjgnv = true;
    void Update()
    {
        
            if (kjgnv)
            {

            Vector3[] verticess = new Vector3[11];

            // debug.text = "I'm working1";
            //GameObject newMeshF = Instantiate(meshF);

                int i;
                for (i = 0; i < 11; i++)
                {
                    verticess[i] = new Vector3(pontok[i].x, pontok[i].y);
                }
            //mesh.vertices = verticess;

            int[] triaa = new int[3 * 10];
                for (int c = 0; c < 9; c++)
                {
                    triaa[3 * c] = 0;
                    triaa[3 * c + 1] = c + 1;
                    triaa[3 * c + 2] = c + 2;
                }
                    triaa[(3 * 9)] = 0;
                    triaa[(3 * 9) + 1] = 10;
                    triaa[(3 * 9) + 2] = 1;
            GameObject obj = Instantiate(Cube);
            //Rpcmeshextra();
            NetworkServer.Spawn(obj);
            kjgnv = false;
            //Rpcmesh();
            //Rpcmeshextra(verticess, triaa);
            //MakeMeshextra(verticess, triaa);
            // CmdCreatePlane2();

            // MakeMesh();
            //kjgnv = false;
        }

        //  OnStartLocalPlayer();
        // {
        //debug.text = "in start local player";
        var planeManager = GetComponent<ARPlaneManager>();

            foreach (ARPlane plane in planeManager.trackables)
            {
                debug.text = "in planemanager";
                //debug.text = "I'm working2";
                if (false)
                {
                    Mesh mesh = new Mesh();
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
                    //////////////////////////////////////////////////////
                    Debug.Log(vectors[1]);
                    //isit = false;
                    //debug.text = vectors[0].ToString()+ vectors[1].ToString()+ vectors[2].ToString()+ plane.boundary.Length.ToString()+"center"+plane.center.ToString()+"position"+plane.transform.position.ToString()+"rotation"+plane.transform.rotation;
                    Debug.Log(vectors[1]);
                    //GameObject newMeshF = Instantiate(meshF);
                    Vector3[] vertices = new Vector3[plane.boundary.Length];
                    int i;
                    for (i = 0; i < plane.boundary.Length; i++)
                    {
                        vertices[i] = new Vector3(vectors[i].x, 0, vectors[i].y);
                    }
                    //RpcFromPlane(plane, mesh);

                    //newMeshF.GetComponent<MeshRenderer>().material = mat;

                    int[] tria = new int[3 * (plane.boundary.Length - 2)];
                    for (int c = 0; c < plane.boundary.Length - 2; c++)
                    {
                        tria[3 * c] = 0;
                        tria[3 * c + 1] = c + 1;
                        tria[3 * c + 2] = c + 2;
                    }
                    //CmdCreatePlanefromplane(vertices, tria, plane.boundary.Length, plane.transform.position, plane.transform.rotation);
                    /*mesh.vertices = vertices;


                    mesh.triangles = tria;
                    mesh.RecalculateNormals();
                    newMeshF.GetComponent<MeshFilter>().mesh = mesh;
                    //newMeshF.GetComponent<MeshRenderer>().material = mat;
                    newMeshF.transform.position = plane.transform.position;
                    newMeshF.transform.rotation = plane.transform.rotation;
                    NetworkServer.Spawn(newMeshF);*/
                    //position és rotation miatt jobb ha gameobject
                    debug.text = "I'm alive2";
                    //RpcCreatePlane(vertices, tria, plane.boundary.Length, newMeshF, plane.transform.position, plane.transform.rotation);
                    //neMeshF.GetComponent<MeshRenderer>().material = mat;
                    /*tria[(3 * 9)] = 0;
                    tria[(3 * 9) + 1] = 10;
                    tria[(3 * 9) + 2] = 1;*/
                }
                //isit = false;
                //  }

                // if (planeNew == plane)
                // debug.text = "I'm alive";
                // Do something with the ARPlane
            }      // }
            //}
      //  }
    }

    /*  [ClientRpc]
      public void RpcFromPlane (ARPlane plane, Mesh mesh)
      {
          Vector3[] vertices = new Vector3[plane.boundary.Length];
          GameObject newMeshF = Instantiate(meshF);

          int i;
          for (i = 0; i < plane.boundary.Length; i++)
          {
              vertices[i] = new Vector3(vectors[i].x, 0, vectors[i].y);
          }
          mesh.vertices = vertices;

          int[] tria = new int[3 * (plane.boundary.Length - 2)];
          for (int c = 0; c < plane.boundary.Length - 2; c++)
          {
              tria[3 * c] = 0;
              tria[3 * c + 1] = c + 1;
              tria[3 * c + 2] = c + 2;
          }
          mesh.triangles = tria;
          mesh.RecalculateNormals();
          newMeshF.GetComponent<MeshFilter>().mesh = mesh;
          newMeshF.GetComponent<MeshRenderer>().material = mat;
          newMeshF.transform.position = plane.transform.position;
          newMeshF.transform.rotation = plane.transform.rotation;
      }*/

   [ClientRpc]
    public void RpcCreatePlane(Vector3[] vertices, int[] tria, int planeBoundaryLength, GameObject newMeshF, Vector3 position, Quaternion rotation)
    {
       
        Mesh mesh = new Mesh();
        
        mesh.vertices = vertices;

       
        mesh.triangles = tria;
        mesh.RecalculateNormals();
        newMeshF.GetComponent<MeshFilter>().mesh = mesh;
        //newMeshF.GetComponent<MeshRenderer>().material = mat;
        newMeshF.transform.position = position;
        newMeshF.transform.rotation = rotation;


        //debug.text = vertices[0].ToString() + vertices[1].ToString() + vertices[2].ToString() + planeBoundaryLength.ToString() + "position" + position.ToString() + "rotation" + rotation.ToString();
    }

    [Command]
    public void CmdCreatePlanefromplane(Vector3[] vertices, int[] tria, int planeBoundaryLength, Vector3 position, Quaternion rotation)
    {
        //CreatePlane(vertices, tria, planeBoundaryLength, position, rotation);
    }
    [ClientRpc]
    public void RpcCreatePlanefromplane(Vector3[] vertices, int[] tria, int planeBoundaryLength, Vector3 position, Quaternion rotation)
    {
        CreatePlane(vertices,tria,  planeBoundaryLength, position, rotation);
    }
    public void CreatePlane(Vector3[] vertices, int[] tria, int planeBoundaryLength, Vector3 position, Quaternion rotation)
    {
        GameObject newMeshF = Instantiate(meshF);
        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
       

        mesh.triangles = tria;
        mesh.RecalculateNormals();
        newMeshF.GetComponent<MeshFilter>().mesh = mesh;
        newMeshF.GetComponent<MeshRenderer>().material = mat;
       newMeshF.transform.position = position;
        newMeshF.transform.rotation = rotation;


        debug.text = "in create plane"+vertices[0].ToString() + vertices[1].ToString() + vertices[2].ToString() + planeBoundaryLength.ToString() + "position" + position.ToString() + "rotation" + rotation.ToString();
    }

    public void MakeMesh()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[11];
        for (int z = 0; z < 5; z++)
        {
            // debug.text = "I'm working1";
            GameObject newMeshF = Instantiate(meshF);

            int i;
            for (i = 0; i < 11; i++)
            {
                vertices[i] = new Vector3(pontok[i].x, pontok[i].y, z);
            }
            mesh.vertices = vertices;

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
            mesh.RecalculateNormals();
            NetworkServer.Spawn(newMeshF);
            newMeshF.GetComponent<MeshFilter>().mesh = mesh;
            newMeshF.GetComponent<MeshRenderer>().material = mat;

        }
    }
    public void MakeMeshextra(Vector3[] verticess, int[] triaa)
    {
        
        

        Mesh mesh = new Mesh();
        GameObject newMeshFF = Instantiate(meshF);

        mesh.vertices = verticess;
        mesh.triangles = triaa;
            mesh.RecalculateNormals();
            //NetworkServer.Spawn(newMeshF);
            newMeshFF.GetComponent<MeshFilter>().mesh = mesh;
            newMeshFF.GetComponent<MeshRenderer>().material = mat;

        
    }

    /*  [Command]
      public void CmdCreatePlane2()
      {
          debug.text = "Cmd";
          MakeMesh();
      }*/
    [ClientRpc]
    public void Rpcmeshextra(Vector3[] vertices, int[] tria)
    {
       
        debug.text = "Rpc2";
        MakeMeshextra(vertices,tria);
    }

    [ClientRpc]
    public void Rpcmesh()
    {
        kjgnv = false;
        debug.text = "Rpc";
       MakeMesh();
    }
    
}
