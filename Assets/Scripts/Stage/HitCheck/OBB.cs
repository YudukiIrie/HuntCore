using UnityEngine;

namespace Stage.HitCheck
{
    /// <summary>
    /// 各オブジェクトOBB情報
    /// </summary>
    public class OBB
    {
        // OBBの種類
        public enum OBBType
        {
            Body,   // 体の部位
            Weapon, // 武器または攻撃の一部
            Guard,  // 防御状態
            None    // なし
        }
        public OBBType Type { get; private set; }

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

        // 可視化用オブジェクト
        public OBBVisualBox VisualBox { get; private set; }

        public OBB(Transform transform, Vector3 size)
        {
            Center = transform.position;
            AxisX  = transform.right;
            AxisY  = transform.up;
            AxisZ  = transform.forward;
            Radius = size * 0.5f;
            IsHit  = false;

            VisualBox = new OBBVisualBox(Center, transform.rotation, Radius);
        }

        public void UpdateInfo(Transform transform)
        {
            Center = transform.position;

            AxisX = transform.right;
            AxisY = transform.up;
            AxisZ = transform.forward;

            VisualBox.UpdateInfo(transform, IsHit);
        }

        public void Hit()
        {
            IsHit = true;
        }

        public void ResetHitInfo()
        {
            IsHit = false;
        }

        public void SetOBBType(OBBType type)
        {
            Type = type;
        }
    }
}
