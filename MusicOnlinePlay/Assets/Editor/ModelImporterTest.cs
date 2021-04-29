using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ModelImporterTest : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        if (assetPath.Contains("@"))
        {
            ModelImporter modelImporter = assetImporter as ModelImporter;
            modelImporter.materialImportMode = ModelImporterMaterialImportMode.None;
        }
    }


    void OnPostprocessModel(GameObject g)
    {
        Apply(g.transform);
    }

    void Apply(Transform t)
    {
        if (t.name.ToLower().Contains("bi"))
            t.gameObject.AddComponent<MeshCollider>();

        // Recurse
        foreach (Transform child in t)
            Apply(child);
    }
}
