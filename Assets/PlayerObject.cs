using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
//using UnityEngine.UI;

public class PlayerObject : NetworkBehaviour
{
   /* public GameObject gameObjectToInstantiate;

    private GameObject spawnedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();*/
    // public Button moveBtn;
    public GameObject PlayerUnitPrefab;
    //public GameObject myPlayerUnit;
    
    // Start is called before the first frame update
    void Start()
    {

        if (isLocalPlayer == false)
            return;
        

        CmdSpawnMyUnit();
        //CmdSpawn();
        // _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(index: 0).position;
            return true;
        }
        touchPosition = default;
        return false;
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
            return;
        }

       /* if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

       
            CmdSpawn();*/
        
        /* if (Input.GetKeyDown(KeyCode.Space))
         {
             PlayerUnitPrefab.transform.Translate(0, 1, 0);
         }

         moveBtn = GetComponent<Button>();
         //moveBtn.GetComponent<NetworkIdentity>().AssignClientAuthority(PlayerUnitPrefab.GetComponent<NetworkIdentity>().connectionToClient);
         moveBtn.onClick.AddListener(RpcMoveObject);*/

    }

  /*  [Command]
    public void CmdSpawn()
    {
    if (_arRaycastManager.Raycast(touchPosition, hits, trackableTypes: TrackableType.PlaneWithinPolygon))
    {
        var hitPose = hits[0].pose;

            spawnedObject = Instantiate(gameObjectToInstantiate, hitPose.position, hitPose.rotation);//asszem csak ez a sor
             //GameObject spawnedObject = Instantiate(gameObjectToInstantiate);
            NetworkServer.SpawnWithClientAuthority(spawnedObject, connectionToClient);
        }
    }*/
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
