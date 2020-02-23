using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ElementConfig : ScriptableObject
{
    public List<ElementData> elements;

    public ElementData getElementData(Element p_element)
    {
        return elements.Find(x => x.element == p_element);
    }
}
