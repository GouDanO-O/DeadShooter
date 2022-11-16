using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshUpdate : MonoBehaviour
{
    private SkinnedMeshRenderer skinMeshUpdate;

    private MeshCollider skinMeshCollision;
    // Start is called before the first frame update
    void Start()
    {
        skinMeshUpdate = GetComponent<SkinnedMeshRenderer>();
        skinMeshCollision = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float timer= 0f;
        timer += Time.deltaTime;
        if(timer>2f)
        {
            CollisionMeshUpdate();
            timer = 0;
        }
    }
    private void CollisionMeshUpdate()
    {
        Mesh collisionMesh = new Mesh();
        skinMeshUpdate.BakeMesh(collisionMesh);

        skinMeshCollision.sharedMesh = null;
        skinMeshCollision.sharedMesh = collisionMesh;
    }
}
