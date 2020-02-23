
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AttackData))]
public class AttackDataEditor : Editor
{
    private AttackData m_attackData;

    private void OnEnable()
    {
        m_attackData = target as AttackData;
    }

    public override void OnInspectorGUI()
    {
        m_attackData.name = EditorGUILayout.TextField("Name", m_attackData.name);
        m_attackData.description = EditorGUILayout.TextField("Description", m_attackData.description);
        m_attackData.portrait = (Sprite)EditorGUILayout.ObjectField("Portrait", m_attackData.portrait, typeof(Sprite), false);
        m_attackData.cost = EditorGUILayout.IntField("Cost", m_attackData.cost);
        m_attackData.element = (Element)EditorGUILayout.EnumPopup("Element", m_attackData.element);
        m_attackData.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", m_attackData.prefab, typeof(GameObject), false);
        m_attackData.moveType = (AttackMoveType)EditorGUILayout.EnumPopup("Move Type", m_attackData.moveType);

        if (m_attackData.moveType == AttackMoveType.PROJECTILE)
        {
            EditorGUI.indentLevel++;
            m_attackData.moveSpeed = EditorGUILayout.FloatField("Move Speed", m_attackData.moveSpeed);
            EditorGUI.indentLevel--;
        }

        m_attackData.attackType = (AttackType)EditorGUILayout.EnumPopup("Attack Type", m_attackData.attackType);

        switch (m_attackData.attackType)
        {
            case AttackType.MELEE:
                drawMeleeInspector();
                break;

            case AttackType.RANGED:
                drawRangedInspector();
                break;
        }

        string displayDescription = string.Format("DESCRIPTION DISPLAYED IN GAME:\n{0}", m_attackData.getDisplayDescription());
        EditorGUILayout.HelpBox(displayDescription, MessageType.Info);

        EditorUtility.SetDirty(m_attackData);
    }

    private void drawMeleeInspector()
    {
        m_attackData.power = EditorGUILayout.IntField("Power", m_attackData.power);
        m_attackData.count = EditorGUILayout.IntField("Count", m_attackData.count);
        m_attackData.hitTarget = (HitTarget)EditorGUILayout.EnumPopup("Hit Targets", m_attackData.hitTarget);

        if (m_attackData.count > 1)
        {
            EditorGUI.indentLevel++;
            m_attackData.delayBetweenAttacks = EditorGUILayout.FloatField("Delay Between Attacks", m_attackData.delayBetweenAttacks);
            EditorGUI.indentLevel--;
        }
    }

    private void drawRangedInspector()
    {
        m_attackData.power = EditorGUILayout.IntField("Power", m_attackData.power);
        m_attackData.count = EditorGUILayout.IntField("Count", m_attackData.count);
        m_attackData.hitTarget = (HitTarget)EditorGUILayout.EnumPopup("Hit Targets", m_attackData.hitTarget);

        if (m_attackData.count > 1)
        {
            EditorGUI.indentLevel++;
            m_attackData.delayBetweenAttacks = EditorGUILayout.FloatField("Delay Between Attacks", m_attackData.delayBetweenAttacks);
            EditorGUI.indentLevel--;
        }
    }
}
