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

        // 可視化ゲームオブジェクト
        GameObject _visualBox;

        // メッシュレンダラー
        public MeshRenderer Renderer { get; private set; }

        // マテリアル
        Material _noHitImage;
        Material _hitImage;

        public OBBVisualBox(Vector3 position, Quaternion rotation, Vector3 radius)
        {
            Position = position;
            Rotation = rotation;
            Scale = radius * 2;
        }

        /// <summary>
        /// 可視化ゲームオブジェクトと
        /// 描画関連情報の登録
        /// </summary>
        /// <param name="go">可視化ゲームオブジェクト</param>
        /// <param name="noHit">非接触マテリアル</param>
        /// <param name="hit">接触マテリアル</param>
        public void SetGameObjectInfo(GameObject go, Material noHit, Material hit)
        {
            // === ゲームオブジェクト ===
            _visualBox ??= go;
            _visualBox.transform.position = Position;
            _visualBox.transform.rotation = Rotation;
            _visualBox.transform.localScale = Scale;

            // === 描画関連 ===
            Renderer = _visualBox.GetComponent<MeshRenderer>();
            _noHitImage ??= noHit;
            _hitImage ??= hit;
        }

        /// <summary>
        /// 情報の更新
        /// </summary>
        /// <param name="transform">更新元Transform</param>
        /// <param name="isHit">ヒットの有無</param>
        public void UpdateInfo(Transform transform, bool isHit)
        {
            if (_visualBox != null)
            {
                // === Transform関連 ===
                _visualBox.transform.position = transform.position;
                _visualBox.transform.rotation = transform.rotation;

                // === マテリアル ===
                Material[] mats = Renderer.materials;
                mats[0] = isHit ? _hitImage : _noHitImage;
                Renderer.materials = mats;
            }
        }
    }
}
