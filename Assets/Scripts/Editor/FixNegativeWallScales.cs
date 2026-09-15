using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class FixNegativeWallScales : Editor
{
    private static readonly HashSet<string> TargetWallNames = new HashSet<string>
    {
        "TTT_wall",
        "Left_wall",
        "NCC_wall",
        "DDP_wall",
        "NXK_wall",
        "DVQ_wall",
        "HQN_wall",
        "LDC_wall",
        "PDL_wall"
    };

    [MenuItem("Tools/Fix Negative Scale Walls")]
    public static void FixWalls()
    {
        int fixedWalls = 0;
        int fixedChildren = 0;

        Transform[] allTransforms = Object.FindObjectsByType<Transform>(FindObjectsSortMode.None);

        foreach (Transform t in allTransforms)
        {
            bool isTargetWall = TargetWallNames.Contains(t.name) || (t.name.ToLower().Contains("wall") && t.localScale.z < 0);

            if (isTargetWall)
            {
                bool modified = false;

                if (t.localScale.z < 0)
                {
                    Undo.RecordObject(t, "Fix Wall Scale");
                    Vector3 wallScale = t.localScale;
                    wallScale.z = Mathf.Abs(wallScale.z);
                    t.localScale = wallScale;
                    fixedWalls++;
                    modified = true;
                }

                // Check and fix all children (Quad, vi_name, en_name, main_content, etc.)
                for (int i = 0; i < t.childCount; i++)
                {
                    Transform child = t.GetChild(i);
                    bool childModified = false;
                    Vector3 childPos = child.localPosition;
                    Vector3 childScale = child.localScale;

                    // Ensure child local position Z is positive (in front of the wall)
                    if (childPos.z < 0)
                    {
                        childPos.z = Mathf.Abs(childPos.z);
                        childModified = true;
                    }

                    // Ensure child local scale Z is positive
                    if (childScale.z < 0)
                    {
                        childScale.z = Mathf.Abs(childScale.z);
                        childModified = true;
                    }

                    // Ensure rotation is facing forward (0, 180, 0)
                    // (All quads and TMP in this scene face the camera with Euler Y = 180)
                    if (Mathf.Abs(Mathf.DeltaAngle(child.localEulerAngles.y, 180f)) > 0.01f ||
                        Mathf.Abs(child.localEulerAngles.x) > 0.01f ||
                        Mathf.Abs(child.localEulerAngles.z) > 0.01f)
                    {
                        Undo.RecordObject(child, "Fix Child Rotation");
                        child.localEulerAngles = new Vector3(0, 180, 0);
                        childModified = true;
                    }

                    if (childModified)
                    {
                        Undo.RecordObject(child, "Fix Child Transform");
                        child.localPosition = childPos;
                        child.localScale = childScale;
                        EditorUtility.SetDirty(child.gameObject);
                        fixedChildren++;
                        modified = true;
                    }
                }

                if (modified)
                {
                    EditorUtility.SetDirty(t.gameObject);
                }
            }
        }

        if (fixedWalls > 0 || fixedChildren > 0)
        {
            var activeScene = EditorSceneManager.GetActiveScene();
            EditorSceneManager.MarkSceneDirty(activeScene);
            Debug.Log($"[FixNegativeWallScales] Sửa thành công {fixedWalls} object wall và {fixedChildren} object con (Quad, vi_name, en_name, main_content)! Nhớ lưu Scene (Ctrl+S).");
            EditorUtility.DisplayDialog("Hoàn tất sửa Scale",
                $"Đã sửa thành công:\n- {fixedWalls} wall object (đưa Scale Z về dương)\n- {fixedChildren} object con (đưa Scale Z & Position Z về dương để không bị lật ngược hoặc chìm trong tường)\n\nVui lòng nhấn Ctrl+S để lưu Scene.",
                "OK");
        }
        else
        {
            Debug.Log("[FixNegativeWallScales] Không tìm thấy object wall nào bị negative scale trong Scene hiện tại.");
            EditorUtility.DisplayDialog("Thông báo", "Không tìm thấy object wall hoặc component nào bị negative scale trong Scene hiện tại.", "OK");
        }
    }
}
