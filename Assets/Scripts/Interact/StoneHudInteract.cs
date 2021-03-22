using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHudInteract : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WorkerManager.Instance.StoneHutBuild();
    }

    // Update is called once per frame
    void Destroy()
    {
        WorkerManager.Instance.StoneHutDestroyed();
    }
}
