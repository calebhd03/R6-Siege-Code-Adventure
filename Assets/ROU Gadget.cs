using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROUGadget : MonoBehaviour, Gadget
{
    public GameObject ROUThrowable;
    public GameObject gadgetSpawnLocation;

    void Gadget.Play()
    {
        Debug.Log("Play");
        ThrowROU();
    }

    void ThrowROU()
    {
        Debug.Log("Throw ROU");
        Instantiate(ROUThrowable, gadgetSpawnLocation.transform.position, gadgetSpawnLocation.transform.rotation);
    }
}
