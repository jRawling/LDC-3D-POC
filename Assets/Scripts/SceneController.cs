using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public DamageMenuController damageMenuController;
    public Material highlightMaterial;
    public Material unassignedDamageMaterial;
    public Material damageType1Material;
    public Material damageType2Material;
    public Material damageType3Material;

    public GameObject markerObjects;
    public GameObject markerPrefab;
    public Text text;
    Damage currentDamage;
    Dictionary<Guid, Damage> damage = new Dictionary<Guid, Damage>();

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (hitInfo.transform.tag == "Marker")
                {
                    Damage foundDamage = null;
                    damage.TryGetValue(new Guid(hitInfo.transform.name), out foundDamage);
                    if (foundDamage != null)
                    {
                        Select(foundDamage);
                    }
                    return;
                }
                else
                {
                    Select(hitInfo);
                }
                //   Debug.Log("Hit " + hitInfo.transform.gameObject.name + " at: " + hitInfo.point.x + ", " + hitInfo.point.y + ", " + hitInfo.point.z);
                //  Debug.Log(Camera.main.transform.forward.x + ", " + Camera.main.transform.forward.y + ", " + Camera.main.transform.forward.z);
            }
        }
    }

    void Select(Damage damage)
    {
        Deselect();
        currentDamage = damage;
        damageMenuController.SetActive(true);
        SetTypeButton(damage.damageType);
    }

    private void SetTypeButton(DamageType damageType)
    {
        var panel = damageMenuController.transform.FindChild("Panel");

        switch(damageType)
        {
            case DamageType.Type1:
                Button type1 = panel.transform.FindChild("Type1").gameObject.GetComponent<Button>();
                type1.Select();
                return;
            case DamageType.Type2:
                Button type2 = panel.transform.FindChild("Type2").gameObject.GetComponent<Button>();
                type2.Select();
                return;
            case DamageType.Type3:
                Button type3 = panel.transform.FindChild("Type3").gameObject.GetComponent<Button>();
                type3.Select();
                return;
        }
    }

    void Select(RaycastHit hit)
    {
        Deselect();
        text.text = hit.transform.name;
        GameObject marker = Instantiate(markerPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
        marker.transform.parent = markerObjects.transform;
        currentDamage = new Damage(marker, hit.transform.gameObject, DamageType.Unassigned);
        currentDamage.SetDamageType(DamageType.Unassigned, (unassignedDamageMaterial));
        currentDamage.SetPartMaterial(highlightMaterial);
        damageMenuController.SetActive(true);
    }

    public void Deselect()
    {
        if (currentDamage != null)
        {
            currentDamage.ResetPartMaterial();
            if (currentDamage.damageType == DamageType.Unassigned)
            {
                Destroy(currentDamage.marker);
            }
            else
            {
                if (!damage.ContainsKey(currentDamage.id))
                {
                    damage.Add(currentDamage.id, currentDamage);
                }
            }

            currentDamage = null;
        }

        damageMenuController.SetActive(false);
    }

    public void SetCurrentDamageType1()
    {
        currentDamage.SetDamageType(DamageType.Type1, damageType1Material);
    }

    public void SetCurrentDamageType2()
    {
        currentDamage.SetDamageType(DamageType.Type2, damageType2Material);
    }

    public void SetCurrentDamageType3()
    {
        currentDamage.SetDamageType(DamageType.Type2, damageType3Material);
    }

    internal class Damage
    {
        public Guid id;
        public GameObject marker;
        public GameObject part;
        public DamageType damageType;
        private Material defaultPartMaterial;

        public Damage(GameObject marker, GameObject part, DamageType damageType)
        {
            this.id = Guid.NewGuid();
            this.marker = marker;
            this.marker.name = id.ToString();
            this.part = part;
            this.damageType = damageType;
            defaultPartMaterial = GetPartRenderer().material;
        }

        public void SetDamageType(DamageType damageType, Material material)
        {
            this.damageType = damageType;
            this.SetMarkerMaterial(material);
        }

        private void SetMarkerMaterial(Material material)
        {
            Renderer markerRenderer = marker.GetComponent<Renderer>();
            markerRenderer.material = material;
        }

        public void SetPartMaterial(Material material)
        {
            GetPartRenderer().material = material;
        }

        public void ResetPartMaterial()
        {
            GetPartRenderer().material = defaultPartMaterial;
        }

        private Renderer GetPartRenderer()
        {
            return part.GetComponent<Renderer>();
        }
    }

    internal enum DamageType
    {
        Type1 = 0,
        Type2 = 1,
        Type3 = 2,
        Unassigned = 3
    }
}