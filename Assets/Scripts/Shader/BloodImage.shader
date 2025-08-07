Shader "Custom/BloodImage"  // Unity��ł̕\����
{
    Properties  // �G�f�B�^��ŕύX�\�ȃp�����[�^
    {
        // �����Ԃ��S�̂̐F
        _Color("Tint Color", Color) = (1, 0, 0, 1)

        // �G�b�W�̏_�炩��(�l���傫���قǒ[���ڂ₯��)
        _Softness("Edge Softness", Range(0, 5)) = 2
    }

    SubShader   // ���ۂ̕`�揈��
    {
        // �`�揇�𓧖��n�ɐݒ�(�������Ƃ��ĕ`�悷��Ƃ����w��)
        // �{����Opaque(�s����), Transparent(����), Overlay(UI)�̏��ɕ`��
        Tags{"Queue"="Transparent" "RenderType"="Transparent"}

        // ���u�����h���w��(SrcAlpha: �����̃A���t�@, OneMinusSrcAlpha: �w�i�̔���)
        // ���u�����h: �F�𔼓����ō�������(��: ��0.5 + �w�i(�O���[)0.5 = �����񂾐ԐF)
        Blend SrcAlpha OneMinusSrcAlpha

        // ���ʕ`��(�r���{�[�h�͗��\�������Ȃ��̂ŕK�v)
        // �r���{�[�h: ��ɃJ�����̕��������l�p����
        Cull off

        // �[�x�o�b�t�@�ɏ������܂Ȃ�(���߃I�u�W�F�N�g���m��Z������h��)
        ZWrite off

        // ���̉e�����󂯂Ȃ�
        Lighting off

        Pass    // ���̕`�揈��
        {
            CGPROGRAM

            // ���_�V�F�[�_�[�Ƃ���vert�֐����g��
            // vert�֐���appdata�^�̒��_�f�[�^���󂯎��Av2f�^�̃f�[�^��Ԃ�
            #pragma vertex vert

            // �t���O�����g�V�F�[�_�[�Ƃ���frag�֐����g��
            // �t���O�����g�V�F�[�_�[: �s�N�Z���V�F�[�_�[�Ƃ������A�s�N�Z���̍ŏI�I�ȐF�����߂�
            // vert�֐�����n���ꂽ���(v2f)���g����1�s�N�Z�����Ƃ̐F��Ԃ�
            #pragma fragment frag

            // Properties�Ő錾�����ϐ���HLSL���ł��󂯎��
            fixed4 _Color;
            float _Softness;

            // Unity���l��n�����߂̃f�[�^
            // �p�[�e�B�N���̏ꍇParticleSystem����l���ݒ肳���
            struct appdata
            {
                float4 vertex : POSITION;   // ���_�̈ʒu
                float2 uv : TEXCOORD0;      // uv���W(0�`1)
                fixed4 color : COLOR;       // �p�[�e�B�N���������_�J���[
            };

            // uv: �r���{�[�h��Ƀe�N�X�`����\��ۂ�2D���W
            // U: X���W, V: Y���W
            // frag�֐��ł��̒l�����ɐF�A�����x�����߂�

            // vert�֐���appdata��ϊ�����frag�֐��ɓn�����߂̃f�[�^
            struct v2f
            {
                float4 pos : SV_POSITION;   // �ԊҌ�̒��_�ʒu(�X�N���[����ԏ�ł̈ʒu)
                float2 uv : TEXCOORD0;      // uv���t���O�����g�V�F�[�_�[�ɓn��
                fixed4 color : COLOR;       // ���_�J���[��n��
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex); // ���_�ʒu���J�������_�ɕϊ�
                o.uv = v.uv;
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // uv�̒��S����̋������v�Z(���S: 0.5, 0.5)
                float2 center = float2(0.5, 0.5);
                float2 dist = distance(i.uv, center);

                // �����ɉ����Ē��S�͔Z���A�[�͔���
                float alpha = saturate(1.0 - dist * _Softness);

                fixed4 col = i.color;
                col.a *= alpha;

                // RGB�͂��̂܂܂ŁA�A���t�@�̂ݒ��S���[�ŃO���f�[�V����
                return col;
            }
            ENDCG
        }
    }
}
