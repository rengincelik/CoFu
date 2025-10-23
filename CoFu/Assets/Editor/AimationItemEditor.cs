using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ItemAnimation))]
public class ItemAnimationDrawer : PropertyDrawer
{
    private const float LineSpacing = 2f; // satırlar arası boşluk

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        property.isExpanded = EditorGUI.Foldout(
            new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
            property.isExpanded, label, true);

        if (property.isExpanded)
        {
            EditorGUI.indentLevel++;
            float y = position.y + EditorGUIUtility.singleLineHeight + LineSpacing;

            // animationType ve duration her zaman göster
            y = DrawField(property, "animationType", position, y);
            y = DrawField(property, "duration", position, y);

            AnimationType type = (AnimationType)property.FindPropertyRelative("animationType").enumValueIndex;

            // Sadece ilgili alanları göster
            switch (type)
            {
                case AnimationType.Scale:
                case AnimationType.Slide:
                case AnimationType.Rotate:
                    y = DrawVector3MiniField(property, "from", position, y);
                    y = DrawVector3MiniField(property, "to", position, y);
                    break;

                case AnimationType.Punch:
                    y = DrawField(property, "punchStrength", position, y);
                    y = DrawField(property, "punchVibrato", position, y);
                    break;

                case AnimationType.Fade:
                    y = DrawField(property, "toAlpha", position, y);
                    break;

                case AnimationType.Bounce:
                case AnimationType.None:
                    EditorGUI.LabelField(new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight),
                        "No additional parameters");
                    y += EditorGUIUtility.singleLineHeight + LineSpacing;
                    break;
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    private float DrawField(SerializedProperty property, string name, Rect position, float y)
    {
        SerializedProperty p = property.FindPropertyRelative(name);
        EditorGUI.PropertyField(new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight), p);
        return y + EditorGUIUtility.singleLineHeight + LineSpacing;
    }

    private float DrawVector3MiniField(SerializedProperty property, string name, Rect position, float y)
    {
        SerializedProperty p = property.FindPropertyRelative(name);
        Rect labelRect = new Rect(position.x, y, 60, EditorGUIUtility.singleLineHeight);
        Rect fieldRect = new Rect(position.x + 60, y, position.width - 60, EditorGUIUtility.singleLineHeight);

        EditorGUI.LabelField(labelRect, name);

        float[] values = new float[3] { p.vector3Value.x, p.vector3Value.y, p.vector3Value.z };
        EditorGUI.MultiFloatField(fieldRect,
            new GUIContent[] { new GUIContent("X"), new GUIContent("Y"), new GUIContent("Z") },
            values);
        p.vector3Value = new Vector3(values[0], values[1], values[2]);

        return y + EditorGUIUtility.singleLineHeight + LineSpacing;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!property.isExpanded)
            return EditorGUIUtility.singleLineHeight;

        float lineHeight = EditorGUIUtility.singleLineHeight + LineSpacing;
        float height = lineHeight * 2; // animationType + duration

        AnimationType type = (AnimationType)property.FindPropertyRelative("animationType").enumValueIndex;

        switch (type)
        {
            case AnimationType.Scale:
            case AnimationType.Slide:
            case AnimationType.Rotate:
                height += lineHeight * 2; // from + to
                break;

            case AnimationType.Punch:
                height += lineHeight * 2; // punchStrength + punchVibrato
                break;

            case AnimationType.Fade:
                height += lineHeight; // toAlpha
                break;

            case AnimationType.Bounce:
            case AnimationType.None:
                height += lineHeight; // No additional parameters
                break;
        }

        return height;
    }
}

// -------------------------

[CustomPropertyDrawer(typeof(AnimationSequenceItem))]
public class AnimationSequenceItemDrawer : PropertyDrawer
{
    private const float LineSpacing = 2f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        float y = position.y;

        SerializedProperty goProp = property.FindPropertyRelative("gameObject");
        EditorGUI.PropertyField(new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight), goProp);
        y += EditorGUIUtility.singleLineHeight + LineSpacing;

        SerializedProperty delayProp = property.FindPropertyRelative("delay");
        EditorGUI.PropertyField(new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight), delayProp);
        y += EditorGUIUtility.singleLineHeight + LineSpacing;

        SerializedProperty animProp = property.FindPropertyRelative("animation");
        float animHeight = EditorGUI.GetPropertyHeight(animProp, true);
        EditorGUI.PropertyField(new Rect(position.x, y, position.width, animHeight), animProp, true);
        


        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight + LineSpacing; // gameObject
        height += EditorGUIUtility.singleLineHeight; // delay

        SerializedProperty animProp = property.FindPropertyRelative("animation");

        height += EditorGUI.GetPropertyHeight(animProp, true) + LineSpacing; // animation
        height += EditorGUIUtility.singleLineHeight; // delay
        return height;
    }
}
