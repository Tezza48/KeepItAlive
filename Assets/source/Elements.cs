using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum Element
{
    Hydrogen = 1,
    Helium = 2,
    Lithium = 3,
    Beryllium = 4,
    // Boron
    Nitrogen = 6,
    Carbon = 7,
    Oxygen = 8,

    Magnesium = 12,
    Aluminium = 13,
    Silicon = 14,
    Sulfur = 16,


    Chromium = 24,
    Iron = 26,
    Cobalt = 27,
}

public class ElementInventory : SortedDictionary<Element, int>
{
    public ElementInventory() : base()
    {
        
    }

    public ElementInventory(ElementMakeup[] makeup) : base()
    {
        foreach (var element in makeup)
        {
            this[element.element] = element.amount;
        }
    }
}

[Serializable]
public struct ElementMakeup
{
    public Element element;
    public int amount;
}

public static class Elements
{
    public static Dictionary<Element, int> activations;
    public static Element[] amunitions;

    public static void Init()
    {
        activations = new Dictionary<Element, int>();

        activations[Element.Hydrogen] = 200;
        activations[Element.Helium] = 200;
        activations[Element.Lithium] = 4000;
        activations[Element.Beryllium] = 2500;

        activations[Element.Nitrogen] = 10000;
        activations[Element.Carbon] = 45000;
        activations[Element.Oxygen] = 50000;

        activations[Element.Magnesium] = 100000;
        activations[Element.Aluminium] = 150000;
        activations[Element.Silicon] = 200000;
        activations[Element.Sulfur] = 250000;
        activations[Element.Chromium] = 300000;
        activations[Element.Iron] = 500000;
        activations[Element.Cobalt] = 700000;

        amunitions = new Element[]
        {
            Element.Lithium,
            Element.Nitrogen,
            Element.Aluminium,
            Element.Cobalt,
        };
    }
}