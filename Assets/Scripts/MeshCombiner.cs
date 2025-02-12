using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    void Start()
    {
        // Bu script'i bir ebeveyn GameObject'e ekle
        // Ebeveyn alt�ndaki t�m MeshFilter'lar� al
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        // Her bir mesh'i birle�tirmek i�in CombineInstance dizisi olu�tur
        CombineInstance[] combineInstances = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            // Mesh bilgisini ve yerel d�n���m matrisini ayarla
            combineInstances[i].mesh = meshFilters[i].sharedMesh;
            combineInstances[i].transform = meshFilters[i].transform.localToWorldMatrix;

            // Alt nesneleri ge�ici olarak devre d��� b�rak
            meshFilters[i].gameObject.SetActive(false);
        }

        // Ebeveynin MeshFilter'� yoksa ekle
        MeshFilter parentMeshFilter = GetComponent<MeshFilter>();
        if (parentMeshFilter == null)
            parentMeshFilter = gameObject.AddComponent<MeshFilter>();

        // Yeni mesh olu�tur ve combine i�lemini yap
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances, true, true);
        parentMeshFilter.mesh = combinedMesh;

        // Ebeveyn nesnenin MeshRenderer'� yoksa ekle ve materyali ata
        MeshRenderer parentRenderer = GetComponent<MeshRenderer>();
        if (parentRenderer == null)
            parentRenderer = gameObject.AddComponent<MeshRenderer>();

        // (Gerekirse materyali elle atayabilirsin)

        // Ebeveyn nesneyi aktif hale getir
        gameObject.SetActive(true);
    }
}
