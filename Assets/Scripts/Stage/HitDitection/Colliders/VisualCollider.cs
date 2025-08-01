using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// �����R���C�_�[
    /// </summary>
    public class VisualCollider
    {
        // �����Q�[���I�u�W�F�N�g
       GameObject _visualGO;

        // ���b�V�������_���[
        MeshRenderer _renderer;

        // �}�e���A��
        Material _noHitImage;
        Material _hitImage;

        public VisualCollider(GameObject go, Vector3 scale, Material noHit, Material hit)
        {
            _visualGO = go;
            _visualGO.transform.localScale = scale;

            _renderer = go.GetComponent<MeshRenderer>();
            _noHitImage = noHit;
            _hitImage   = hit;
        }

        /// <summary>
        /// ���̍X�V
        /// </summary>
        /// <param name="transform">�X�V��Transform</param>
        /// <param name="wasHit">�q�b�g�̗L��</param>
        public void UpdateInfo(Transform transform, bool wasHit)
        {
            if (_visualGO != null)
            {
                // === Transform�֘A ===
                _visualGO.transform.position = transform.position;
                _visualGO.transform.rotation = transform.rotation;

                // === �}�e���A�� ===
                Material[] mats = _renderer.sharedMaterials;
                mats[0] = wasHit ? _hitImage : _noHitImage;
                _renderer.sharedMaterials = mats;
                
            }
        }
    }
}