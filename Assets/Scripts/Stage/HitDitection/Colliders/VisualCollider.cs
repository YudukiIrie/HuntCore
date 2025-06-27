using UnityEngine;

namespace Stage.HitCheck
{
    /// <summary>
    /// 可視化コライダー
    /// </summary>
    public class VisualCollider
    {
        // 可視化ゲームオブジェクト
        protected GameObject _visualGO;

        // サイズ
        //protected Vector3 _scale;

        // メッシュレンダラー
        protected MeshRenderer _renderer;

        // マテリアル
        protected Material _noHitImage;
        protected Material _hitImage;

        public VisualCollider(GameObject go, Vector3 scale, Material noHit, Material hit)
        {
            _visualGO = go;
            _visualGO.transform.localScale = scale;

            _renderer = go.GetComponent<MeshRenderer>();
            _noHitImage = noHit;
            _hitImage   = hit;
        }

        /// <summary>
        /// 情報の更新
        /// </summary>
        /// <param name="transform">更新元Transform</param>
        /// <param name="wasHit">ヒットの有無</param>
        public void UpdateInfo(Transform transform, bool wasHit)
        {
            if (_visualGO != null)
            {
                // === Transform関連 ===
                _visualGO.transform.position = transform.position;
                _visualGO.transform.rotation = transform.rotation;

                // === マテリアル ===
                Material[] mats = _renderer.materials;
                mats[0] = wasHit ? _hitImage : _noHitImage;
                _renderer.materials = mats;
            }
        }
    }
}