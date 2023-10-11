using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Jiggle : MonoBehaviour
{
    public float intensity = 1f;
    public float mass = 1f;
    public float stiffness = 1f;
    public float damping = 0.75f;

    private SpriteRenderer spriteRenderer;
    private Transform[] bones;
    private Vector3[] originalBones;
    private JellyVertex[] jellyVertices;
    private Vector3[] vertexArray;

    // Start is called before the first frame update
    void Start()
    {
        bones = this.GetComponent<SpriteSkin>().boneTransforms;
        originalBones = new Vector3[bones.Length];
        for (int i = 0; i < bones.Length; i++)
        {
            originalBones[i] = bones[i].position;
        }

        jellyVertices = new JellyVertex[bones.Length];
        for (int i = 0; i < bones.Length; i++)
        {
            jellyVertices[i] = new JellyVertex(i, bones[i].position);
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vertexArray = originalBones;
        for(int i = 0; i < jellyVertices.Length; i++)
        {
            Vector3 target = vertexArray[jellyVertices[i].id];
            float tempIntensity = (1 - (spriteRenderer.bounds.max.y - target.y) / spriteRenderer.bounds.size.y) * intensity;
            jellyVertices[i].Shake(target, mass, stiffness, damping);
            target = jellyVertices[i].position;
            vertexArray[jellyVertices[i].id] = Vector3.Lerp(vertexArray[jellyVertices[i].id], target, tempIntensity);
        }
        for(int i = 0; i < bones.Length; i++)
        {
            bones[i].position = vertexArray[i];
        }
    }

    public class JellyVertex
    {
        public int id;
        public Vector3 position;
        public Vector3 velocity;
        public Vector3 force;

        public JellyVertex(int id, Vector3 position){
            this.id = id;
            this.position = position;
        }

        public void Shake(Vector3 target, float mass, float stiffness, float damping)
        {
            force = (target - position) * stiffness;
            velocity = (velocity + force / mass) * damping;
            position += velocity;
            if ((velocity + force / mass).magnitude < 0.001f)
            {
                position = target;
            }
        }
    }
}
