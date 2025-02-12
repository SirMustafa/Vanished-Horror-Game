using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    void Start()
    {
        // Bu script'i bir ebeveyn GameObject'e ekle
        // Ebeveyn altýndaki tüm MeshFilter'larý al
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        // Her bir mesh'i birleþtirmek için CombineInstance dizisi oluþtur
        CombineInstance[] combineInstances = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            // Mesh bilgisini ve yerel dönüþüm matrisini ayarla
            combineInstances[i].mesh = meshFilters[i].sharedMesh;
            combineInstances[i].transform = meshFilters[i].transform.localToWorldMatrix;

            // Alt nesneleri geçici olarak devre dýþý býrak
            meshFilters[i].gameObject.SetActive(false);
        }

        // Ebeveynin MeshFilter'ý yoksa ekle
        MeshFilter parentMeshFilter = GetComponent<MeshFilter>();
        if (parentMeshFilter == null)
            parentMeshFilter = gameObject.AddComponent<MeshFilter>();

        // Yeni mesh oluþtur ve combine iþlemini yap
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances, true, true);
        parentMeshFilter.mesh = combinedMesh;

        // Ebeveyn nesnenin MeshRenderer'ý yoksa ekle ve materyali ata
        MeshRenderer parentRenderer = GetComponent<MeshRenderer>();
        if (parentRenderer == null)
            parentRenderer = gameObject.AddComponent<MeshRenderer>();

        // (Gerekirse materyali elle atayabilirsin)

        // Ebeveyn nesneyi aktif hale getir
        gameObject.SetActive(true);
    }
}
