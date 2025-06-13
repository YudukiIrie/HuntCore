using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.HitCheck
{
    /// <summary>
    /// �eOBB�����I�u�W�F�N�g���
    /// </summary>
    public class OBBVisualBox
    {
        // ���W���
        public Vector3 Position { get; private set; }

        //��]�l���
        public Quaternion Rotation { get; private set; }

        // �T�C�Y���
        public Vector3 Scale { get; private set; }

        // ���b�V�������_���[
        public MeshRenderer Renderer { get; private set; }

        public OBBVisualBox(Vector3 position, Quaternion rotation, Vector3 radius)
        {
            Position = position;
            Rotation = rotation;
            Scale = radius * 2;
        }

        public void UpdatePosition(Vector3 center)
        {
            Position = center;
        }

        public void UpdateRotation(Quaternion rotation)
        {
            Rotation = rotation;
        }

        public void SetMeshRenderer(MeshRenderer renderer)
        {
            Renderer = renderer;
        }
    }
}
