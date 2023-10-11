using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class JellyConstraint : MonoBehaviour
{
    public float maxRadius = 1f;

    private Transform[] bones;
    private Transform rootBone;
    private int rootBoneIndex;
    private Vector3[] boneVector;
    private float[] boneOriginalDiff;
    private Vector3[] tempBoneVector;

    // Start is called before the first frame update
    void Start()
    {
        bones = this.GetComponent<SpriteSkin>().boneTransforms;
        rootBone = this.GetComponent<SpriteSkin>().rootBone;
        for(int i = 0; i < bones.Length; i++)
        {
            if(bones[i] == rootBone)
            {
                rootBoneIndex = i;
                break;
            }
        }

        boneVector = new Vector3[bones.Length];
        for(int i = 0; i < bones.Length; i++)
        {
            boneVector[i] = bones[i].position - bones[rootBoneIndex].position;
        }

        boneOriginalDiff = new float[bones.Length];
        for(int i = 0; i < bones.Length; i++)
        {
            boneOriginalDiff[i] = boneVector[i].magnitude;
        }

        tempBoneVector = new Vector3[bones.Length];
    }

    // TODO: stop joint when pulling back
    // Update is called once per frame
    void Update()
    {
        //if some bone go over the max radius, pull it back
        for(int i = 0; i < bones.Length; i++)
        {
            float diff = (bones[i].position - rootBone.position).magnitude;
            if(diff - boneOriginalDiff[i] > maxRadius)
            {
                print("pull back bone " + i);
                Vector3 velocity = Vector3.zero;
                Vector3 target = rootBone.position + boneVector[i];
                tempBoneVector[i] = target;
            }else{
                tempBoneVector[i] = bones[i].position;
            }
        }
        for(int i = 0; i < bones.Length; i++)
        {
            bones[i].position = tempBoneVector[i];
        }
    }
}
