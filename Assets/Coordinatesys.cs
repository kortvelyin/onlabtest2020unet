using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Coordinatesys : MonoBehaviour
{/// The position relative to the shared world anchor.
    /*[SyncVar]
    private Vector3 localPosition;

    ///The transform of the shared world anchor
    Transform sharedAnchorTrans;

    /// The rotation relative to the shared world anchor.
    [SyncVar]
    private Quaternion localRotation;

    // Start is called before the first frame update
    void Start()
    {
        //Get the instance of your anchor in the world this will be different
        sharedAnchorTrans = SharedCollection.Instance.gameObject.transform;

        // I set this to be the child of an object with an anchor so that I do not have to
        // have one on this object because it takes up a lot of processing power to have 
        // multiple anchors.
        transform.SetParent(SharedCollection.Instance.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (receivedAuthority)
        {
            Vector3 objDir = transform.forward;
            Vector3 objPos = transform.position + objDir * .01f;

            localPosition = sharedAnchorTrans.InverseTransformPoint(objPos);
            // localRotation = transform.localRotation;
            CmdTransform(localPosition);
        }
        else if (!receivedAuthority)
        {
            transform.localPosition = localPosition;
            // transform.localRotation = localRotation;
        }
    }
    public override void OnStartAuthority()
    {
        receivedAuthority = true;
    }

    public override void OnStopAuthority()
    {
        receivedAuthority = false;
    }

    [Command]
    public void CmdTransform(Vector3 position)
    {
        if (!isLocalPlayer)
        {
            localPosition = position;
            //localRotation = rotation;
        }
    }*/
}
