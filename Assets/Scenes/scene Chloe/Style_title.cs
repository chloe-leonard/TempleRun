using UnityEngine;
using TMPro;

public class CurvedText : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
    public float curveStrength = 10f;

    void Start()
    {
        WarpText();
    }

    void WarpText()
    {
        textMeshPro.ForceMeshUpdate();
        var textInfo = textMeshPro.textInfo;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;

            var verts = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].vertices;
            for (int j = 0; j < 4; j++)
            {
                Vector3 orig = verts[textInfo.characterInfo[i].vertexIndex + j];
                float offsetY = curve.Evaluate(orig.x / textMeshPro.rectTransform.rect.width) * curveStrength;
                verts[textInfo.characterInfo[i].vertexIndex + j] = new Vector3(orig.x, orig.y + offsetY, orig.z);
            }
        }
        textMeshPro.UpdateVertexData();
    }
}
