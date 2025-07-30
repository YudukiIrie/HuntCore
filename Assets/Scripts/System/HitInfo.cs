using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���I�u�W�F�N�g�p�q�b�g���N���X
/// </summary>
public class HitInfo : MonoBehaviour
{
    // ��U���̗L��
    public bool WasHit { get; private set; } = false;

    /// <summary>
    /// �q�b�g���̎󂯎��
    /// </summary>
    public virtual void ReceiveHit() { WasHit = true; }

    /// <summary>
    /// �q�b�g���̃��Z�b�g
    /// </summary>
    public virtual void ResetHitReceived() {  WasHit = false; }
}
