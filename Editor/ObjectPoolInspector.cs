using UnityEditor;
//[CustomEditor(typeof(ObjectPool))]
public class ObjectPoolInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        SerializedProperty obJectsToPoolprop = serializedObject.FindProperty("ObjectsToPool");
        int arrSize = obJectsToPoolprop.arraySize;
        for (int i = 0; i < arrSize; i++)
        {
            SerializedProperty pooledObject = obJectsToPoolprop.GetArrayElementAtIndex(i);
            SerializedProperty objectId = pooledObject.FindPropertyRelative("id");
            objectId.intValue = i;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
