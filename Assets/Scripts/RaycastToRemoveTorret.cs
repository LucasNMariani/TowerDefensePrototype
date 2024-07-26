using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastToRemoveTorret : MonoBehaviour
{
    [SerializeField]
    private LayerMask raycastLayerTarget;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastToTorret(Input.mousePosition);
        }
    }

    void RaycastToTorret(Vector3 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastLayerTarget))
        {
            if (hit.collider.GetComponent<TowerController>())
            {
                var targetTorret = hit.transform.gameObject.GetComponent<TowerController>();
                if (GameManager.instance.CanRemoveTurret == true)
                {
                    GameManager.instance.CanRemoveTurret = false;
                    targetTorret.DestroyTorret();
                }
            }
        }
    }
}
