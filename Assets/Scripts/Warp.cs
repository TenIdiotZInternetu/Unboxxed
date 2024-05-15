using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public void WarpHere(GameObject obj) {
        obj.transform.position = transform.position;
    }
}
