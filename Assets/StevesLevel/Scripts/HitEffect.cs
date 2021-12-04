using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script will control the transparency of the attached plane to simulate a red hit effect. the effect will be triggered
//whenever health is lost in the player script. this script will be attached to the player. 
public class HitEffect : MonoBehaviour
{
    public GameObject hitEffectPlane;
    public float alpha;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAlpha(hitEffectPlane.GetComponent<Renderer>().material, alpha);
    }

    void ChangeAlpha(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    }
}
