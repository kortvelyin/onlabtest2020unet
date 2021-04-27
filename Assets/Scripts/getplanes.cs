using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;


[System.Serializable]
public struct PlaneData// egy változó a plane-nek a pontjaira, és a plane id-jára
    {
    public int planeId;
    public string Jvertice;
    public NetworkInstanceId netId;

     public PlaneData(string Jvertice, int planeId, NetworkInstanceId netId)
    {
        this.planeId = planeId;
        this.netId = netId;
        this.Jvertice = Jvertice;
    }
    }


public class getplanes : NetworkBehaviour
{
    

    public SyncListPlaneData onnewPlayer= new SyncListPlaneData();

    public class SyncListPlaneData : SyncListStruct<PlaneData> { }

    ARAnchorManager m_AnchorManager;
    List<ARAnchor> m_Anchors = new List<ARAnchor>();
    Dictionary<string, string> verticesDict = new Dictionary<string, string>();
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
    //float width = 1;
    //float height = 1;
    //ARPlaneManager m_ARPlaneManager;
    ARPlane planeNew;
    public Text debug;
    int idk;
    Vector3 smt;
    Quaternion identity;
    Transform any;
    //bool isit = true;
    Vector2[] smt2;
    List<Vector2> list;
    Unity.Collections.NativeArray<Vector2> vectors;
    PlayerObject playerobjscript;
    Vector3[] pontok = new Vector3[11];
    //float speed = 100.0f;
    // Start is called before the first frame update

    //PlayerObject playerobjscript;

    //tuner.EnableLocalEndpoint();
    void Start()
    {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        m_AnchorManager = GetComponent<ARAnchorManager>();

        if (isLocalPlayer)
            planeManager = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();
            if (planeManager!=null)
             {
                   Debug.Log("Plane manager found");
             }

   

       


    }

   

    void Update()
    {

        if (!isLocalPlayer) return;
    
              
       
        ///////////////////////////////////////////////////////////////////////////////////////////
        
           
        foreach (ARPlane plane in planeManager.trackables)
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
            
            Mesh mesh = new Mesh();
           
            Vector3[] vertices = new Vector3[plane.boundary.Length];
            int i;
                for (i = 0; i < plane.boundary.Length; i++)
                {
                        vertices[i] = new Vector3(vectors[i].x, 0, vectors[i].y);
                }

            string json = JsonConvert.SerializeObject(vertices);
           

           
           CmdDoSomelvnevnlv(json, plane.transform.position, plane.transform.rotation, plane.GetInstanceID(), plane.boundary.Length, playerNetID);
     
        }

    }

    /// A függvény ami kezeli a kilensen a mesh létrehozást
    public void CreatePlane(Vector3[] vertices, Vector3 position, Quaternion rotation, int id, int boundarylength, NetworkInstanceId playerNetID)
    {

        ARAnchor anchor = null;
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
        newMeshF.AddComponent<MeshCollider>();
        //newMeshF.GetComponent<MeshCollider>().
        newMeshF.AddComponent<Rigidbody>().isKinematic = true; 
        newMeshF.transform.position = position;
        newMeshF.transform.rotation = rotation;
        anchor = newMeshF.GetComponent<ARAnchor>();
        if (anchor == null)
        {
            anchor = newMeshF.AddComponent<ARAnchor>();
            if (anchor != null)
            {
                //Debug.Log("anchor worked");
                //worked++;
               // anchorThingie.text = worked.ToString();
            }
        }
        m_Anchors.Add(anchor);

        //debug.text = "in create plane" + vertices[0].ToString() + vertices[1].ToString() + vertices[2].ToString() + "position" + position.ToString() + "rotation" + rotation.ToString();
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


    [Command] //Serverre küldi a Plane adatokat
    public void CmdDoSomelvnevnlv(string json, Vector3 position, Quaternion rotation, int id, int boundarylength, NetworkInstanceId playerNetID)
    {
        PlaneData pd;
        pd.Jvertice = json;
        pd.netId = playerNetID;
        pd.planeId = id;

        onnewPlayer.Add(pd);

        //Vector3[] verticess = new Vector3[boundarylength];
        //verticess = meshprefab.GetComponent<MeshFilter>().mesh.vertices;
        Rpcmeshextra(json, position, rotation, id, boundarylength, playerNetID);
        //int[] tria = new int[3 * (boundarylength - 2)];
        //tria= meshprefab.GetComponent<MeshFilter>().mesh.triangles;
        debug.text = "I work"+json;
        // RpcmeshSomelvnevnlv(verticess, tria, position, rotation, id, boundarylength);
        //MakeMesh();
    }
   

    [ClientRpc]//Plane adatból állítja elõ a mesh-t
    void Rpcmeshextra(string json, Vector3 position, Quaternion rotation, int id, int boundarylength, NetworkInstanceId playerNetID)
    {

        var vertices = JsonConvert.DeserializeObject<List<Vector3>>(json);
        Vector3[] verticess = vertices.ToArray();
        //Debug.Log(name);
        // Debug.Log(id);
        //isit = false;
        //debug.text = "Rpc2"+vertices[0].ToString()+ vertices[2].ToString();
        // MakeMesh();
        CreatePlane(verticess, position, rotation, id, boundarylength, playerNetID);
        //MakeMeshextra(vertices,tria);
    }

    [ClientRpc]//Mit csinál az RPC hívással
    public void Rpcmesh()
    {
        //once = false;
        debug.text = "Rpc";
        MakeMesh();
    }
    [Command]//Még nincs playerID
    public void Cmdplayerplus()
    {
        PlayerId++;
    }

    [Command]//Még nincs playerID
    public void CmdTryingToCallCommand()
    {
        RpcWritesmt();
        //Rpcmesh();
    }
    [ClientRpc]
    public void RpcWritesmt()
    {
        debug.text = "i can call an Rpc and therefore a command too in getplanes";
    }

   
}
