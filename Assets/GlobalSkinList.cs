using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSkinList : MonoBehaviour
{
    public int totalSkins = 3;
    public List<RuntimeAnimatorController> headSkins, bodySkins, legSkins;
    public List<string> headNames, bodyNames, legNames;


    void Start()
    {

    }
}
