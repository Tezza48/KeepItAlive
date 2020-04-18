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
    Flourine = 9,
    Neon = 10,
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

    public static void Init()
    {
        activations = new Dictionary<Element, int>();

        activations[Element.Hydrogen] = 100;
        activations[Element.Helium] = 200;
        activations[Element.Lithium] = 300;
        activations[Element.Beryllium] = 350;

        activations[Element.Nitrogen] = 1000;
        activations[Element.Carbon] = 1200;
        activations[Element.Oxygen] = 3000;
    }
}