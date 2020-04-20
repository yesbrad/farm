using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

    [CustomNodeEditor(typeof(Start))]
    public class StartEditor : NodeEditor {

        public override void OnBodyGUI() {
            serializedObject.Update();

            Start node = target as Start;

            //EditorGUILayout.PropertyField(serializedObject.FindProperty("id"), GUIContent.none);

            //if (node.HasPort("input")) 
            //{
                GUILayout.BeginHorizontal();
                //NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
                NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("output"), GUILayout.MinWidth(0));
                GUILayout.EndHorizontal();
            //} 
            //else 
            //{
                //NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"));
            //}
            GUIStyle yee = new GUIStyle();
            yee.fontSize = 25;
            yee.normal.textColor = Color.white;
            
            //GUILayout.Space(-30);
            EditorGUI.LabelField(new Rect(0,100,100,100 ), serializedObject.FindProperty("eventIDs").enumDisplayNames[serializedObject.FindProperty("eventIDs").enumValueIndex], yee  );
			NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("eventIDs"));
            //NodeEditorGUILayout.InstancePortList("answers", typeof(OceanNode), serializedObject, NodePort.IO.Output, Node.ConnectionType.Override);

            serializedObject.ApplyModifiedProperties();
        }

        public override int GetWidth() {
            return 200;
        }

        public override GUIStyle GetBodyStyle()
		{
			return base.GetBodyStyle();
		}

		//public override Color GetTint() {
		//    Chat node = target as Chat;
		//    if (node.character == null) return base.GetTint();
		//    else {
		//        Color col = node.character.color;
		//        col.a = 1;
		//        return col;
		//    }
		//}
	}
