using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public ElementMakeup[] initialConditions;
    public ElementInventory composition;

    float tTime = 0.0f;

    int avgReactions;
    float lastReactionCheck;
    List<int> reactions;

    // Start is called before the first frame update
    void Start()
    {
        Elements.Init();

        composition = new ElementInventory(initialConditions);

        reactions = new List<int>();
    }

    private void Update()
    {
        if (Time.time > lastReactionCheck + 1.0)
        {
            var total = 0;
            foreach (var value in reactions)
            {
                total += value;
            }

            avgReactions = total / reactions.Count;
            reactions.Clear();

            lastReactionCheck = Time.time;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var mass = GetTotalMass();
        var scale = mass / 100.0f;
        transform.localScale = new Vector3(scale, scale, scale);

        var numReactions = 0;

        // Every second
        //if (tTime > Time.time)
        //{
        //    return;
        //}

        tTime = Time.time + 1.0f;
        float fusionChance = 0.005f;

        var deltas = new ElementInventory();

        foreach (var comp in composition)
        {
            var element = comp.Key;
            var count = comp.Value;

            if (Elements.activations[element] < mass)
            {
                var selfFusionProduct = (int)element * 2;
                if (Enum.IsDefined(typeof(Element), selfFusionProduct) && UnityEngine.Random.value < fusionChance / Time.fixedDeltaTime)
                {
                    var product = (Element)selfFusionProduct;

                    var numToFuse = Mathf.RoundToInt(count * fusionChance);

                    if (!deltas.ContainsKey(element)) deltas[element] = 0;
                    if (!deltas.ContainsKey(product)) deltas[product] = 0;

                    deltas[element] -= numToFuse * 2;
                    deltas[product] += numToFuse;

                    numReactions += numToFuse;
                }

                var hChance = 0.5f;

                var hFusionProduct = (int)element + 1;
                if (Enum.IsDefined(typeof(Element), hFusionProduct) && UnityEngine.Random.value < (fusionChance * hChance) / Time.fixedDeltaTime)
                {
                    var product = (Element)hFusionProduct;

                    var numToFuse = Mathf.CeilToInt(count * fusionChance * hChance);

                    if (!deltas.ContainsKey(element)) deltas[element] = 0;
                    if (!deltas.ContainsKey(product)) deltas[product] = 0;

                    deltas[element] -= numToFuse * 2;
                    deltas[product] += numToFuse;

                    numReactions += numToFuse;
                }

            }
        }

        AddElements(deltas);

        reactions.Add(numReactions);
    }

    public void AddElements(ElementInventory inventory)
    {
        foreach (var elem in inventory)
        {
            var name = elem.Key;

            if (!composition.ContainsKey(name)) composition[name] = 0;
            composition[name] = Math.Max(composition[name] + elem.Value, 0);
        }
    }

    public int GetTotalMass()
    {
        int mass = 0;
        foreach(var element in composition)
        {
            mass += (int)element.Key * element.Value;
        }

        return mass;
    }

    #region GUI
    struct GuiSettings
    {
        public Vector2 scrollPos;
    };

    GuiSettings guiSettings;

    private void GUIWindowFunc(int windowId)
    {
        guiSettings.scrollPos = GUILayout.BeginScrollView(guiSettings.scrollPos);
        GUILayout.Label("Total mass: " + GetTotalMass());

        GUILayout.Label("Reactions: " + avgReactions);

        GUILayout.Label("Composition");
        GUILayout.BeginVertical();
        foreach(var elem in composition)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(elem.Key.ToString() + " " + ((int)elem.Key).ToString());
            GUILayout.Label(elem.Value.ToString());
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
    }

    private void OnGUI()
    {
        Rect r = new Rect(0, 0, 200, 400);
        GUILayout.Window(1, r, GUIWindowFunc, "Star Info");
    }
    #endregion
}