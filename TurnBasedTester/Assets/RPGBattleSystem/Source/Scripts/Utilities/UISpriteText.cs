using UnityEngine;
using UnityEngine.UI;
using System;

[ExecuteInEditMode]
[RequireComponent(typeof(GridLayoutGroup))]
public class UISpriteText : MonoBehaviour
{
    [SerializeField]
    private bool capsLock = true;
    [SerializeField]
    private string _intialText;
    [SerializeField]
    private UISpriteFont _spriteFont;
    [SerializeField]
    private bool m_resizeGridToFitText;
    [SerializeField]
    private Color m_color = Color.white;

    private GridLayoutGroup _gridLayoutGroup;

    private string _text;

    public string text
    {
        get
        {
            return _text;
        }

        set
        {
            _text = value;

            SetText(_text);
        }
    }

    public void InitText()
    {
        text = _intialText;
    }

    private void Initialize()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    private void SetText(string p_text)
    {
        if (string.IsNullOrEmpty(p_text))
            return;

        if (_gridLayoutGroup == null)
            Initialize();

        if (capsLock)
            p_text = p_text.ToUpper();

        char[] array = p_text.ToCharArray();

        if (_gridLayoutGroup.startCorner == GridLayoutGroup.Corner.UpperRight ||
            _gridLayoutGroup.startCorner == GridLayoutGroup.Corner.LowerRight)
            Array.Reverse(array);

        p_text = new string(array);

        RemoveExcessiveChildren(array.Length);
        for (int i = 0; i < p_text.Length; i++)
        {
            char c = p_text[i];

            if (c.Equals('_'))
                c = ' ';

            Sprite sprite = _spriteFont.GetSprite(c.ToString());
            SetSprite(i, sprite);
        }

        if (m_resizeGridToFitText)
        {
            LayoutElement l_element = GetComponent<LayoutElement>();

            if (l_element != null)
            {
                l_element.preferredWidth = _gridLayoutGroup.cellSize.x * _text.Length;
            }
            else
            {
                RectTransform l_rect = transform as RectTransform;
                l_rect.sizeDelta = new Vector2(_gridLayoutGroup.cellSize.x * _text.Length,
                                               l_rect.sizeDelta.y);
            }

        }
    }

    public void setColor(Color p_color)
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Image>().color = p_color;
        }
    }

    private void SetSprite(int childIndex, Sprite sprite)
    {
        if (childIndex >= transform.childCount)
        {
            CreateChild();
        }

        Transform child = transform.GetChild(childIndex);

        if (child == null)
            return;

        child.gameObject.SetActive(true);

        Image image = child.GetComponent<Image>();

        if (image == null)
            return;

        image.sprite = sprite;
        image.color = m_color;
    }

    private void RemoveExcessiveChildren(int maxCount)
    {
        if (transform.childCount <= maxCount)
            return;

        for (int i = maxCount; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void CreateChild()
    {
        GameObject go = new GameObject("child", typeof(Image));
        go.transform.SetParent(transform, false);
        go.GetComponent<Image>().raycastTarget = false;
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            if (_intialText != text)
            {
                text = _intialText;
            }
        }

    }

}
