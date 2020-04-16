using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSwitch : MonoBehaviour, IElectricObject
{
    Renderer ObjectRenderer;
    public bool isRadioActivated = false;
    public Material ElectricMaterial;
    public Material NormalMaterial;

    public Collider originalBoxColl;
    public Collider radioRangeCollider;
    public void Start()
    {
        this.ObjectRenderer = GetComponent<Renderer>();
        this.originalBoxColl.enabled = true;
        this.radioRangeCollider.enabled = false;
    }
    public void Activate()
    {
        if (!isRadioActivated)
        {
            isRadioActivated = true;
            this.originalBoxColl.enabled = false;
            this.radioRangeCollider.enabled = true;
            StartCoroutine(RadioActivated());
        }
    }

    IEnumerator RadioActivated()
    {

        yield return new WaitForSeconds(3.0f);
        this.originalBoxColl.enabled = true;
        this.radioRangeCollider.enabled = false;
    }

    public Vector3 GetSelectionPosition()
    {
        return this.transform.position;
    }

    public void Highlight(bool isActive)
    {
        if (isActive)
        {
            ObjectRenderer.material = ElectricMaterial;
        }
        else
        {
            ObjectRenderer.material = NormalMaterial;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guard"))
        {
            Debug.Log("Detect sound");
            Guard guard = other.gameObject.GetComponent<Guard>();
            guard.action = new GuardCheck(guard, this.gameObject);
        }
    }

}
