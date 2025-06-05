using UnityEngine;

namespace Stage.HitCheck
{
    /// <summary>
    /// 各オブジェクトOBB情報
    /// </summary>
    public class OBB
    {
        // 中心座標
        public Vector3 Center { get; private set; }

        // 分離軸
        public Vector3 AxisX { get; private set; }
        public Vector3 AxisY { get; private set; }
        public Vector3 AxisZ { get; private set; }

        // 中心からXYZ平面までの長さ(半径)
        public Vector3 Radius { get; private set; }

        // ヒットの有無
        public bool IsHit {  get; private set; }

        public OBB(Transform transform, Vector3 size)
        {
            Center = transform.position;
            AxisX  = transform.right;
            AxisY  = transform.up;
            AxisZ  = transform.forward;
            Radius = size * 0.5f;
            IsHit  = false;
        }

        public void UpdateCenter(Vector3 center)
        {
            Center = center;
        }

        public void UpdateAxes(Transform transform)
        {
            AxisX = transform.right;
            AxisY = transform.up;
            AxisZ = transform.forward;
        }

        public void Hit()
        {
            IsHit = true;
        }

        public void ResetHitInfo()
        {
            IsHit = false;
        }
    }
}
