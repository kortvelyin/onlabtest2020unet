using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using UnityEngine.UI;

public class PlayerObject : NetworkBehaviour
{
   // public Button moveBtn;
    public GameObject PlayerUnitPrefab;
    //public GameObject myPlayerUnit;
    
    // Start is called before the first frame update
    void Start()
    {

        if (isLocalPlayer == false)
            return;
        

         
       

    }
    [Command]//Még nincs playerID
    public void CmdTryingToCallCommand()
    {
        Rpcmesh();
    }
    [ClientRpc]//Mit csinál az RPC hívással
    public void Rpcmesh()
    {
        once = false;
        Debug.Log("player object can call it");
       // debug.text = "Rpc";
        //MakeMesh();
    }
   /* public void MakeMesh()
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
    }*/
    [Command]
    public void CmdSpawnMyUnit()
    {
       GameObject obj= Instantiate(PlayerUnitPrefab);
        //NetworkServer.Spawn(obj);
        //myPlayerUnit = obj;
        NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
    }
    bool once = true;

    // Update is called once per frame
    void Update()
    {
        if (once)
        {
            CmdTryingToCallCommand();

        }
        if (isLocalPlayer == false)
        {
            return;
        }


       

        /* if (Input.GetKeyDown(KeyCode.Space))
         {
             PlayerUnitPrefab.transform.Translate(0, 1, 0);
         }

         moveBtn = GetComponent<Button>();
         //moveBtn.GetComponent<NetworkIdentity>().AssignClientAuthority(PlayerUnitPrefab.GetComponent<NetworkIdentity>().connectionToClient);
         moveBtn.onClick.AddListener(RpcMoveObject);*/

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
