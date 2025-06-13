using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.HitCheck
{
    /// <summary>
    /// 各OBB可視化オブジェクト情報
    /// </summary>
    public class OBBVisualBox
    {
        // 座標情報
        public Vector3 Position { get; private set; }

        //回転値情報
        public Quaternion Rotation { get; private set; }

        // サイズ情報
        public Vector3 Scale { get; private set; }

        // メッシュレンダラー
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
