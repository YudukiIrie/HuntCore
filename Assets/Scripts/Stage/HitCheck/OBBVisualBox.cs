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

        // �����Q�[���I�u�W�F�N�g
        GameObject _visualBox;

        // ���b�V�������_���[
        public MeshRenderer Renderer { get; private set; }

        // �}�e���A��
        Material _noHitImage;
        Material _hitImage;

        public OBBVisualBox(Vector3 position, Quaternion rotation, Vector3 radius)
        {
            Position = position;
            Rotation = rotation;
            Scale = radius * 2;
        }

        /// <summary>
        /// �����Q�[���I�u�W�F�N�g��
        /// �`��֘A���̓o�^
        /// </summary>
        /// <param name="go">�����Q�[���I�u�W�F�N�g</param>
        /// <param name="noHit">��ڐG�}�e���A��</param>
        /// <param name="hit">�ڐG�}�e���A��</param>
        public void SetGameObjectInfo(GameObject go, Material noHit, Material hit)
        {
            // === �Q�[���I�u�W�F�N�g ===
            _visualBox ??= go;
            _visualBox.transform.position = Position;
            _visualBox.transform.rotation = Rotation;
            _visualBox.transform.localScale = Scale;

            // === �`��֘A ===
            Renderer = _visualBox.GetComponent<MeshRenderer>();
            _noHitImage ??= noHit;
            _hitImage ??= hit;
        }

        /// <summary>
        /// ���̍X�V
        /// </summary>
        /// <param name="transform">�X�V��Transform</param>
        /// <param name="isHit">�q�b�g�̗L��</param>
        public void UpdateInfo(Transform transform, bool isHit)
        {
            if (_visualBox != null)
            {
                // === Transform�֘A ===
                _visualBox.transform.position = transform.position;
                _visualBox.transform.rotation = transform.rotation;

                // === �}�e���A�� ===
                Material[] mats = Renderer.materials;
                mats[0] = isHit ? _hitImage : _noHitImage;
                Renderer.materials = mats;
            }
        }
    }
}
