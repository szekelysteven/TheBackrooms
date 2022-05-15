using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script will control the transparency of the attached plane to simulate a red hit effect. the effect will be triggered
//whenever health is lost in the player script. this script will be attached to the player. 
public class HitEffect : MonoBehaviour
{
    public GameObject hitEffectPlane;
    public float alpha;
    public float red;
    private float currentTime;
    private float timePoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        ChangeAlpha(hitEffectPlane.GetComponent<Renderer>().material, alpha, red);
        while ((timePoint < currentTime) && (alpha >= 0))
        {
           
            alpha -= .2F;
            red = 0;
        }
    }

    void ChangeAlpha(Material mat, float alphaVal, float redVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(redVal, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    }

    public void SimulateHit()
    {
        alpha += .3F;
        timePoint = currentTime + .3f;
        red = 255;
    }
}
