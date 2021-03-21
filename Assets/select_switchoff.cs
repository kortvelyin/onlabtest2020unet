using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select_switchoff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ToggleSelectState()
    {
        GetComponent<select_switchoff>().enabled = !GetComponent<select_switchoff>().enabled;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
