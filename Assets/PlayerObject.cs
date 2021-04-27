using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;


public class PlayerObject : NetworkBehaviour
{
    Dictionary<string, GameObject> planesDict = new Dictionary<string, GameObject>();
    // public Button moveBtn;
    public GameObject PlayerUnitPrefab;
    //public GameObject myPlayerUnit;
    private NetworkInstanceId playerNetID;
    static public GameObject PlayerObjectsmeshF; //Ami PO-val kezdõdik, annak van megfelelõje a getplanes scriptben
    static public Vector3 POposition;
    static public Quaternion POrotation;
    static public int POid;
    static public int POboundarylength;
    static public bool theMeshIsChanged = false;
    static public string POjson;
    getplanes getplanesscript;
    public Material mat;
    public GameObject meshF;
    public Text debug;
    // Start is called before the first frame update
    void Start()
    {

        if (isLocalPlayer == false)
            return;
        

        CmdSpawnMyUnit();

       // getplanesscript = FindObjectOfType<getplanes>();
        //playerNetID = GetComponent<NetworkIdentity>().netId;
    }

    [Command]
    public void CmdSpawnMyUnit()
    {
       GameObject obj= Instantiate(PlayerUnitPrefab);
        //NetworkServer.Spawn(obj);
        //myPlayerUnit = obj;
        NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer == false)
        {
           // debug.text = "well,well,well";
            return;
        }

        /*if (theMeshIsChanged)//akkor fut le, ha a getplanes scriptben meg lettek változtatva az itteni változók és a theMeshIsChanged true-lesz
        {
            debug.text = "mesh is changed működik";
            CmdTryingToCallCommand();
            string jpos = JsonUtility.ToJson(POposition);
            string jrot = JsonUtility.ToJson(POrotation);
            string NetId = playerNetID.ToString();
            CmdDoSomelvnevnlv(POjson, jpos, jrot, POid, POboundarylength, NetId);
            theMeshIsChanged = false;
        }*/

            /* if (Input.GetKeyDown(KeyCode.Space))
             {
                 PlayerUnitPrefab.transform.Translate(0, 1, 0);
             }

             moveBtn = GetComponent<Button>();
             //moveBtn.GetComponent<NetworkIdentity>().AssignClientAuthority(PlayerUnitPrefab.GetComponent<NetworkIdentity>().connectionToClient);
             moveBtn.onClick.AddListener(RpcMoveObject);*/

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
        debug.text = "i can call an Rpc and therefore a command too in PO";
    }

    [Command] //Serverre küldi a Plane adatokat
    public void CmdDoSomelvnevnlv(string json, string position, string rotation, int id, int boundarylength,string playerNetID)
    {
        Rpcmeshextra(json, position, rotation, id, boundarylength, playerNetID);
        // debug.text = "I work" + json;
        debug.text = "Cmd" + json;
    }

    [ClientRpc]
    void Rpcmeshextra(string json, string position, string rotation, int id, int boundarylength, string playerNetID)
    {

        var vertices = JsonConvert.DeserializeObject<List<Vector3>>(json);
        Vector3[] verticess = vertices.ToArray();
        Vector3 positions = JsonUtility.FromJson<Vector3>(position);
        Quaternion rotations = JsonUtility.FromJson<Quaternion>(rotation);
        CreatePlane(verticess, positions, rotations, id, boundarylength, playerNetID);
        debug.text = "Rpc" + json;
    }
    public void CreatePlane(Vector3[] vertices, Vector3 position, Quaternion rotation, int id, int boundarylength, string playerNetID)
    {
        string idtoDict = id.ToString() + playerNetID;
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


        //debug.text = "in create plane" + vertices[0].ToString() + vertices[1].ToString() + vertices[2].ToString() + "position" + position.ToString() + "rotation" + rotation.ToString();
    }

    /*[ClientRpc]
    void RpcMoveObject()
    {
        PlayerUnitPrefab.transform.Translate(0, 1, 0);
    }*/
    /* [Command]
     void CmdMove()
     {
         this.transform.Translate(1, 1, 2);
     }

     public void Move()
     {
         CmdMove();
     }*/


}
