using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UISpriteFont : ScriptableObject
{
    public List<Sprite> letterSprites;

    public string letterCode =
        "ABCDEFGHIJKLM" +
        "abcdefghijklm" +
        "NOPQRSTUVWXYZ" +
        "nopqrstuvwxyz-'" +
        ".,?!(/" +
        "0123456789^;>< ";

    private Dictionary<string, Sprite> _letterMap;

    private void Initialize()
    {
        _letterMap = new Dictionary<string, Sprite>();

        for (int i = 0; i < letterCode.Length; i++)
        {
            _letterMap.Add(letterCode[i].ToString(), letterSprites[i]);
        }
    }

    public Sprite GetSprite(string character)
    {
        if (_letterMap == null)
            Initialize();

        if (!_letterMap.ContainsKey(character))
        {
            Debug.LogWarningFormat("string {0} is not availible as sprite. CHARACTER COUNT {1} SPRITE COUNT {2} ", character, letterCode.Length, _letterMap.Count);
            return null;
        }


        return _letterMap[character];

    }
}
