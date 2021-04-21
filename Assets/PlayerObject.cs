using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerObject : NetworkBehaviour
{
    Dictionary<string, GameObject> planesDict = new Dictionary<string, GameObject>();
    public Material mat;
    public Text debug;
    private NetworkInstanceId playerNetID;
    // public Button moveBtn;
    public GameObject PlayerUnitPrefab;
    //public GameObject myPlayerUnit;
    public GameObject meshF;
    static public GameObject PlayerObjectsmeshF; //Ami PO-val kezdõdik, annak van megfelelõje a getplanes scriptben
    static public Vector3 POposition;
    static public Quaternion POrotation;
    static public int POid;
    static public int POboundarylength;
    static public bool theMeshIsChanged=false;
    Vector3[] pontok = new Vector3[11];
    getplanes getplanesscript;
    // Start is called before the first frame update
    void Start()
    {

        if (isLocalPlayer == false)
            return;

        getplanesscript = FindObjectOfType<getplanes>();
        playerNetID = GetComponent<NetworkIdentity>().netId;
        /*PlayerObjectsmeshF = meshF;
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
        pontok[10].y = -6;*/

    }


   
    
    bool once = true;

    // Update is called once per frame
    void Update()
    {
       
        if (isLocalPlayer == false)
        {
            return;
        }

        if (false)
        {
            //CmdTryingToCallCommand();

            // FindObjectOfType<getplanes>().CmdTryingToCallCommand();
            //Debug.Log(POid);
        }

        /*if(theMeshIsChanged)//akkor fut le, ha a getplanes scriptben meg lettek változtatva az itteni változók és a theMeshIsChanged true-lesz
        {
            Vector3[] vertices = new Vector3[POboundarylength];
            vertices = PlayerObjectsmeshF.GetComponent<MeshFilter>().mesh.vertices;
            //string idtoDict = POid.ToString() + playerNetID.ToString();
            GameObject newMeshF = Instantiate(meshF);
            Mesh mesh = new Mesh();
            if (planesDict.ContainsKey(POid.ToString()))
            {
                DestroyImmediate(planesDict[POid.ToString()], true);
                planesDict.Remove(POid.ToString());
            }

            planesDict.Add(POid.ToString(), newMeshF);
            int[] tria = new int[3 * (POboundarylength - 2)];
            for (int c = 0; c < POboundarylength - 2; c++)
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
            newMeshF.transform.position = POposition;
            newMeshF.transform.rotation = POrotation;
            CmdSpawnMeshObjectSomehow(newMeshF, POposition, POrotation);
            //CmdCreateMeshFromPlaneInPlayerObject(PlayerObjectsmeshF, POposition, POrotation, POid, POboundarylength, playerNetID);
            Debug.Log(POid+"POid");
            theMeshIsChanged = false;
        }*/
        //PlayerObjectsmeshF = getplanes.meshF;

        /* if (Input.GetKeyDown(KeyCode.Space))
         {
             PlayerUnitPrefab.transform.Translate(0, 1, 0);
         }

         moveBtn = GetComponent<Button>();
         //moveBtn.GetComponent<NetworkIdentity>().AssignClientAuthority(PlayerUnitPrefab.GetComponent<NetworkIdentity>().connectionToClient);
         moveBtn.onClick.AddListener(RpcMoveObject);*/

    }

    [Command]
    void CmdSpawnMeshObjectSomehow(GameObject meshprefab, Vector3 position, Quaternion rotation)
    {
        GameObject go = Instantiate(meshprefab, position, rotation);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    [ClientRpc]
    void RpcSpawnMeshObjectSomehow(GameObject meshprefab)  
    {
        
    }

    [Command]//Még nincs playerID
    public void CmdTryingToCallCommand()
    {
        Rpcmesh();
    }
    [ClientRpc]//Mit csinál az RPC hívással, 
    public void Rpcmesh()
    {
        once = false;
        Debug.Log("player object can call it");
        // debug.text = "Rpc";
        MakeMesh();
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
    [Command]
    public void CmdSpawnMyUnit()
    {
        GameObject obj = Instantiate(PlayerUnitPrefab);
        //NetworkServer.Spawn(obj);
        //myPlayerUnit = obj;
        NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
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

    [Command] //Serverre küldi a Plane adatokat
    public void CmdCreateMeshFromPlaneInPlayerObject(GameObject meshprefab, Vector3 position, Quaternion rotation, int id, int boundarylength, NetworkInstanceId playerNetID)
    {
        Debug.Log("Cmdmeg lett hívva");
        Mesh mesh = new Mesh();
        GameObject newmeshprefab = Instantiate(meshF);
        Vector3[] verticess = new Vector3[boundarylength];
        verticess = meshprefab.GetComponent<MeshFilter>().mesh.vertices;
        mesh.vertices = verticess;
        mesh.RecalculateNormals();
        newmeshprefab.GetComponent<MeshFilter>().mesh = mesh;
        Rpcmeshfromdata(newmeshprefab, position, rotation, id, boundarylength, playerNetID);
        Destroy(newmeshprefab);
        //int[] tria = new int[3 * (boundarylength - 2)];
        //tria= meshprefab.GetComponent<MeshFilter>().mesh.triangles;
        debug.text = "I work";
        // RpcmeshSomelvnevnlv(verticess, tria, position, rotation, id, boundarylength);
        //MakeMesh();
    }
   [ClientRpc]//Plane adatból állítja elõ a mesh-t
    void Rpcmeshfromdata(GameObject meshprefab, Vector3 position, Quaternion rotation, int id, int boundarylength, NetworkInstanceId playerNetID)
    {

       // Vector3[] vertices = new Vector3[boundarylength];
        //vertices = meshprefab.GetComponent<MeshFilter>().mesh.vertices;
        Debug.Log("In RPC");
        //Debug.Log(name);
        // Debug.Log(id);
        //isit = false;
        //debug.text = "Rpc2" + vertices[0].ToString() + vertices[2].ToString();
        // MakeMesh();
        CreatePlanefromData(meshprefab, position, rotation, id, boundarylength, playerNetID);
        //MakeMeshextra(vertices,tria);
    }
    public void CreatePlanefromData(GameObject meshprefab, Vector3 position, Quaternion rotation, int id, int boundarylength, NetworkInstanceId playerNetID)
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
        mesh.vertices = meshprefab.GetComponent<MeshFilter>().mesh.vertices;
        mesh.triangles = tria;
        mesh.RecalculateNormals();
        newMeshF.GetComponent<MeshFilter>().mesh = mesh;
        newMeshF.GetComponent<MeshRenderer>().material = mat;
        newMeshF.transform.position = position;
        newMeshF.transform.rotation = rotation;


        //debug.text = "in create plane" + vertices[0].ToString() + vertices[1].ToString() + vertices[2].ToString() + "position" + position.ToString() + "rotation" + rotation.ToString();
    }


}
