using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDetector : MonoBehaviour
{
    public Color onMouseOverColor;
    private Color startColor;
    private GameObject torret;
    private Renderer rend;
    public Vector3 positionOffset;


    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if (BuildManager.bmInstance.GetTurretToBuild() == null)
        {
            Debug.Log("No se puede construir");
            return;
        }
        GameObject torretToBuild = BuildManager.bmInstance.GetTurretToBuild();
        if (torretToBuild != null)
        {
            torret = Instantiate(torretToBuild, transform.position + positionOffset, transform.rotation);
            BuildManager.bmInstance.TurretPlaced();
        }
        else
        {
            return;
        }
    }

    private void OnMouseEnter()
    {
        rend.material.color = onMouseOverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
