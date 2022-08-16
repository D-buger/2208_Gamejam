using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavingText : MonoBehaviour
{
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private Gradient gradient;

    [SerializeField] private float height = 0.1f;
    [SerializeField] private float weight = 4;
    [SerializeField] private float xDistance = 0.5f;

    [SerializeField] private bool isSetColor = false;
    private void Awake()
    {
        if (!textComponent)
            textComponent = gameObject.GetComponent<TMP_Text>();
    }

    private void LateUpdate()
    {
        textComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
                continue;

            Vector3[] verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            TMP_MeshInfo meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];

            for (int j = 0; j < 4; j++)
            {
                int index = charInfo.vertexIndex + j;
                Vector3 orig = verts[charInfo.vertexIndex + j];
                meshInfo.vertices[index] = orig + new Vector3(0, Mathf.Sin(Timer.realTime * weight + orig.x * xDistance) * height, 0);

                if (isSetColor)
                {
                    Color32 color = gradient.Evaluate(Mathf.Abs(Mathf.Sin((Time.time * 2 + orig.x * 0.1f) * 0.05f)));
                    color.a = 255;
                    meshInfo.colors32[index] = color;
                }
            }

        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            TMP_MeshInfo meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            if (isSetColor)
                meshInfo.mesh.colors32 = meshInfo.colors32;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
