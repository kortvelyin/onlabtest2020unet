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
    Dictionary<string, GameObject> planesDict = new Dictionary<string, GameObject>();
    //public static HashSet<Player> ActivePlayers = new HashSet<Player>();
    [SyncVar]
    int PlayerId = 0;
    int ownID;
    private NetworkInstanceId playerNetID;
    //int playerid = ConnectedPlayer.playerId;
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
    bool isit = true;
    Vector2[] smt2;
    List<Vector2> list;
    Unity.Collections.NativeArray<Vector2> vectors;
    PlayerObject playerobjscript;
    Vector3[] pontok = new Vector3[11];
    float speed = 100.0f;
    // Start is called before the first frame update




    void Start()
    {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        //////Még nem találtam Player ID-t
        Cmdplayerplus();

        ownID = PlayerId;
        Debug.Log(PlayerId);


        playerobjscript = FindObjectOfType<PlayerObject>();

        //////ha esetleg kéne egy plane kód kipróbálásra
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

    void OnStartLocalPlayer() { Debug.Log("Hello"); }
    bool once = true;

    void Update()
    {
        /* Mesh mesh = new Mesh();
         mesh.vertices = pontok;
         mesh.RecalculateNormals();

         meshF.GetComponent<MeshFilter>().mesh = mesh;
         PlayerObject.PlayerObjectsmeshF = meshF;
         PlayerObject.theMeshIsChanged = true;*/
        

        /*if (isServer)
        {
            return;
        }*/
        if (false)
        {
            //CmdTryingToCallCommand();
            PlayerObject.POid = 2;
            PlayerObject.theMeshIsChanged = true;
            once = false;
        }
        // if (!isLocalPlayer) return;

        // if (!isLocalPlayer) return;
        if (!hasAuthority) return;
        ///////////////////////////////////////////////////////////////////////////////////////////
        var planeManager = GetComponent<ARPlaneManager>();
           
        foreach (ARPlane plane in planeManager.trackables)
        {
            if (once)
            {
                //debug.text = "in plane";
                /////////////Milyen adatokkal rendeklezik a plane, ha esetleg kéne egyszer
                planeNew = plane;
                smt = plane.transform.localPosition;
                identity = plane.transform.localRotation;
                smt = plane.transform.localScale;
                smt = plane.transform.position;
                identity = plane.transform.rotation;
                plane.GetInstanceID();
                vectors = plane.boundary;

                //debug.text = vectors[0].ToString()+ vectors[1].ToString()+ vectors[2].ToString()+ plane.boundary.Length.ToString()+"center"+plane.center.ToString()+"position"+plane.transform.position.ToString()+"rotation"+plane.transform.rotation;

                //////Létrehozom a GameObjectet amibe berakok mindent amit nem tudok máshogy átküldeni
                GameObject newMeshFF = Instantiate(meshF);
                Mesh mesh = new Mesh();
                
                Vector3[] vertices = new Vector3[plane.boundary.Length];
                int i;
                for (i = 0; i < plane.boundary.Length; i++)
                {
                    vertices[i] = new Vector3(vectors[i].x, 0, vectors[i].y);
                }

                
                mesh.vertices = vertices;
                mesh.RecalculateNormals();
                newMeshFF.GetComponent<MeshFilter>().mesh = mesh;


                ///Ez a kettõ amit próbáltam
                //newMeshFF.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
                //newMeshFF.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
               /* PlayerObject.PlayerObjectsmeshF = newMeshFF;
                PlayerObject.POposition = plane.transform.position;
                PlayerObject.POrotation = plane.transform.rotation;
                PlayerObject.POid = plane.GetInstanceID();
                PlayerObject.POboundarylength = plane.boundary.Length;*/
                //PlayerObject.theMeshIsChanged = true;

                CreatePlane(vertices, plane.transform.position, plane.transform.rotation, plane.GetInstanceID(), plane.boundary.Length, playerNetID);

                //ANormalFunctionToCallCmdDoSomelvnevnlv(newMeshFF, plane.transform.position, plane.transform.rotation, plane.GetInstanceID(), plane.boundary.Length, playerNetID);
                Destroy(newMeshFF);//Mert csak info továbbításra volt

               
            }



        }

    }



    


    /// A függvény ami kezeli a kilensen a mesh létrehozást
    public void CreatePlane(Vector3[] vertices, Vector3 position, Quaternion rotation, int id, int boundarylength, NetworkInstanceId playerNetID)
    {
        string idtoDict = id.ToString() + playerNetID.ToString();
        GameObject newMeshF = Instantiate(meshF);
        Mesh mesh = new Mesh();
            if (planesDict.ContainsKey(idtoDict))
                {
                    DestroyImmediate(planesDict[idtoDict], true);
                    planesDict.Remove(idtoDict);
                }

        planesDict.Add(idtoDict, newMeshF);
        int[] tria = new int[3 * (boundarylength - 2)];
            for (int c = 0; c < boundarylength - 2; c++)
                {
                    tria[3 * c] = 0;
                    tria[3 * c + 1] = c + 1;
                    tria[3 * c + 2] = c + 2;
                }
        mesh.vertices = vertices;
        mesh.triangles = tria;
        mesh.RecalculateNormals();
        newMeshF.GetComponent<MeshFilter>().mesh = mesh;
        newMeshF.GetComponent<MeshRenderer>().material = mat;
        newMeshF.transform.position = position;
        newMeshF.transform.rotation = rotation;


        debug.text = "in create plane" + vertices[0].ToString() + vertices[1].ToString() + vertices[2].ToString() + "position" + position.ToString() + "rotation" + rotation.ToString();
    }

    /// <summary>
    /// Elõállítja a pontok tömb alapján a mesh-t
    /// </summary>
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

    //kintrõl kapja az adatokat, nem benne van
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



    /*[Command]
       void CmdsrvrmeshToPlane(float[] boundaryX, float[] boundaryY, Vector3 position, Quaternion rotation, int id, int boundarylength)
      {
          Vector3[] vertices = new Vector3[boundarylength];
          int i;
          for (i = 0; i < boundarylength; i++)
          {
              vertices[i] = new Vector3(boundaryX[i], 0, boundaryY[i]);
          }

          Rpcmeshextra(vertices, position, rotation, id, boundarylength);
      }
      */
    public void ANormalFunctionToCallCmdDoSomelvnevnlv(GameObject meshprefab, Vector3 position, Quaternion rotation, int id, int boundarylength, NetworkInstanceId playerNetID)
    {
        CmdDoSomelvnevnlv( meshprefab,  position,  rotation, id,  boundarylength, playerNetID);
    }

    [Command] //Serverre küldi a Plane adatokat
    public void CmdDoSomelvnevnlv(GameObject meshprefab, Vector3 position, Quaternion rotation, int id, int boundarylength, NetworkInstanceId playerNetID)
    {
        Vector3[] verticess = new Vector3[boundarylength];
        verticess = meshprefab.GetComponent<MeshFilter>().mesh.vertices;
        Rpcmeshextra(verticess, position, rotation, id, boundarylength, playerNetID);
        //int[] tria = new int[3 * (boundarylength - 2)];
        //tria= meshprefab.GetComponent<MeshFilter>().mesh.triangles;
        debug.text = "I work";
        // RpcmeshSomelvnevnlv(verticess, tria, position, rotation, id, boundarylength);
        //MakeMesh();
    }
    /* [ClientRpc]
     void RpcmeshSomelvnevnlv(Vector3[] vertices, int[] tria, Vector3 position, Quaternion rotation, int id, int boundarylength)
     {
         debug.text = "Rpc2";

         CreateSomelvnevnlv(vertices, tria, position, rotation, id, boundarylength);  
     }
     public void CreateSomelvnevnlv(Vector3[] vertices, int[] tria, Vector3 position, Quaternion rotation, int id, int boundarylength)
     {
         //GameObject newMeshF = Instantiate(meshprefab);
         GameObject newMeshF = Instantiate(meshF);
         Mesh mesh = new Mesh();
        if (planesDict.ContainsKey(id.ToString()))
         {
             DestroyImmediate(planesDict[id.ToString()], true);
             planesDict.Remove(id.ToString());
         }

         planesDict.Add(id.ToString(), newMeshF);
         mesh.vertices = vertices;
         mesh.triangles = tria;
         //newMeshF = meshprefab;
         //mesh.vertices = meshprefab.GetComponent<MeshFilter>().mesh.vertices;
         //mesh.triangles = meshprefab.GetComponent<MeshFilter>().mesh.triangles;
          mesh.RecalculateNormals();
         newMeshF.GetComponent<MeshFilter>().mesh = mesh;
        // newMeshF.GetComponent<MeshFilter>().mesh.RecalculateNormals();
         newMeshF.GetComponent<MeshRenderer>().material = mat;
         newMeshF.transform.position = position;
         newMeshF.transform.rotation = rotation;


         //debug.text = "in create plane" + vertices[0].ToString() + vertices[1].ToString() + vertices[2].ToString() + "position" + position.ToString() + "rotation" + rotation.ToString();
     }*/




    [ClientRpc]//Plane adatból állítja elõ a mesh-t
    void Rpcmeshextra(Vector3[] vertices, Vector3 position, Quaternion rotation, int id, int boundarylength, NetworkInstanceId playerNetID)
    {


        //Debug.Log(name);
        // Debug.Log(id);
        //isit = false;
        debug.text = "Rpc2"+vertices[0].ToString()+ vertices[2].ToString();
        // MakeMesh();
        CreatePlane(vertices, position, rotation, id, boundarylength, playerNetID);
        //MakeMeshextra(vertices,tria);
    }

    [ClientRpc]//Mit csinál az RPC hívással
    public void Rpcmesh()
    {
        once = false;
        debug.text = "Rpc";
        Debug.Log("i can call it from getplanes and also I come from the cmd");
        //MakeMesh();
    }
    [Command]//Még nincs playerID
    public void Cmdplayerplus()
    {
        PlayerId++;
    }

    [Command]//Próbálom máshonnan meghívni a lényeget, azaz hogy az átadott gameobjectet megtudja hívni és hogy hozzáfér-e
    public void CmdTryingToCallCommand(GameObject Meshf)
    {
        Vector3[] verticess = new Vector3[3];
        verticess = Meshf.GetComponent<MeshFilter>().mesh.vertices;
        Debug.Log("in the thing where you try to access the gameobject");
       Rpcmesh();
    }

}
