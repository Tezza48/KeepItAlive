using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipActor : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Light engineLight;
    public GameObject shootyThing;
    public float hitpoints;

    public ElementMakeup[] initialInventory;
    public ElementInventory inventory;

    public Loot lootPrefab;
    
    [Serializable]
    public struct GuiSettings
    {
        public bool isShowing;
        public Rect windowRect;
    }

    public GuiSettings guiSettings = new GuiSettings
    {
        isShowing = false
    };

    // Start is called before the first frame update
    void Awake()
    {
        inventory = new ElementInventory(initialInventory);

        rigid = GetComponent<Rigidbody2D>();
        hitpoints = (hitpoints > 0.0f) ? hitpoints : 5.0f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        engineLight.intensity = Mathf.Min(1, rigid.velocity.magnitude / 10.0f);

        if (shootyThing.activeSelf)
        {
            var collider = shootyThing.GetComponent<BoxCollider2D>();
            var overlaps = new List<Collider2D>();
            Physics2D.OverlapCollider(collider, new ContactFilter2D(), overlaps);
            foreach (var coll in overlaps)
            {
                var ship = coll.GetComponent<SpaceshipActor>();
                if (ship != null)
                {
                    ship.Damage(Time.deltaTime);
                }
            }
        }
    }

    public void FixedUpdateRotation(float amount)
    {
        rigid.rotation -= amount * Time.deltaTime * 300.0f;
    }

    public void FixedUpdateThrust(float amount)
    {
        rigid.AddForce(transform.up * amount * Time.fixedDeltaTime * 1000.0f);
    }

    public void Shoot()
    {
        if (shootyThing.activeSelf)
        {
            return;
        }

        shootyThing.SetActive(true);
        StartCoroutine(ShootTimeout(1.0f));
    }

    private IEnumerator ShootTimeout(float time)
    {
        yield return new WaitForSeconds(time);

        shootyThing.SetActive(false);
    }

    public void Damage(float damage)
    {
        hitpoints = Mathf.Max(hitpoints - damage, 0);

        if (hitpoints == 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        var loot = Instantiate(lootPrefab, transform.position, transform.rotation);
        loot.inventory = inventory;

        Destroy(gameObject);
    }

    private void OnGUI()
    {
        if (guiSettings.isShowing)
        {
            guiSettings.windowRect.x = Screen.width - guiSettings.windowRect.width;
            guiSettings.windowRect = GUILayout.Window(0, guiSettings.windowRect, WindowFunction, "Inventory");
        }
    }

    public void WindowFunction(int windowId)
    {
        GUILayout.BeginVertical();

        foreach (var elem in inventory)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(elem.Key.ToString() + " " + ((int)elem.Key).ToString());
            GUILayout.Label(elem.Value.ToString());
            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
    }
}
