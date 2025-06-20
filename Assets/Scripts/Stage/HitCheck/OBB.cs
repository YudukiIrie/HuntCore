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

        // ヒット情報
        public struct HitInformation
        {
            public bool isHit;          // ヒットしたかどうか
            public OBBType targetType;  // 接触相手のOBBType

            public HitInformation(bool isHit, OBBType targetType)
            {
                this.isHit = isHit;
                this.targetType = targetType;
            }

            /// <summary>
            /// ヒット情報の取得
            /// </summary>
            /// <param name="type">相手のOBBType</param>
            public void SetHitInfo(OBBType type)
            {
                isHit = true;
                targetType = type;
            }

            /// <summary>
            /// ヒット情報のリセット
            /// </summary>
            public void ResetHitInfo()
            {
                isHit = false;
                targetType = OBBType.None;
            }
        }
        public HitInformation HitInfo;

        // 中心座標
        public Vector3 Center { get; private set; }

        // 分離軸
        public Vector3 AxisX { get; private set; }
        public Vector3 AxisY { get; private set; }
        public Vector3 AxisZ { get; private set; }

        // 中心からXYZ平面までの長さ(半径)
        public Vector3 Radius { get; private set; }

        // 可視化用オブジェクト
        public OBBVisualBox VisualBox { get; private set; }

        public OBB(Transform transform, Vector3 size, OBBType type)
        {
            Center = transform.position;
            AxisX  = transform.right;
            AxisY  = transform.up;
            AxisZ  = transform.forward;
            Radius = size * 0.5f;
            Type   = type;
            HitInfo = new HitInformation(false, OBBType.None);

            VisualBox = new OBBVisualBox(Center, transform.rotation, Radius);
        }

        public void UpdateInfo(Transform transform)
        {
            Center = transform.position;

            AxisX = transform.right;
            AxisY = transform.up;
            AxisZ = transform.forward;

            VisualBox.UpdateInfo(transform, HitInfo.isHit);
        }

        public void SetOBBType(OBBType type)
        {
            Type = type;
        }

        /// <summary>
        /// ヒット情報の取得
        /// </summary>
        /// <param name="other">相手OBB</param>
        public void SetHitInfo(OBB other)
        {
            HitInformation info = HitInfo;
            info.SetHitInfo(other.Type);
            HitInfo = info;
        }

        /// <summary>
        /// ヒット情報のリセット
        /// </summary>
        public void ResetHitInfo()
        {
            HitInformation info = HitInfo;
            info.ResetHitInfo();
            HitInfo = info;
        }
    }
}
