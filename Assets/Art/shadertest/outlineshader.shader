Shader "Custom/OutlineShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {} // ����ͼ
        _OutlineColor ("Outline Color", Color) = (1, 1, 1, 1) // �����ɫ
        _OutlineWidth ("Outline Width", Range(0.1, 5.0)) = 0.5 // ��߿��
        _OutlinePixelSize ("Outline Pixel Size", Range(0.1, 5.0)) = 1.0 // ������ش�С
    }

    SubShader {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // ����ṹ�壬��������λ�á�UV���ꡢ������ɫ
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            // ����ṹ�壬����UV���ꡢ�ü��ռ䶥��λ�á���ɫ
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex; // ����ͼ
            float4 _MainTex_ST;
            float4 _OutlineColor; // �����ɫ
            float _OutlineWidth; // ��߿��
            float _OutlinePixelSize; // ������ش�С

            // ������ɫ��
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // ������λ��ת�����ü��ռ�
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // ������ͼ����ת��
                o.color = v.color * _OutlineColor; // ���������ɫ
                return o;
            }

            // ƬԪ��ɫ��
            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv) * i.color; // ������ͼ����ɫ������ɫ
                float outlineAlpha = step(1, col.a); // ������ɫ��͸�����ж��Ƿ��ڱ�Ե
                float2 offset = float2(_OutlineWidth * outlineAlpha * _OutlinePixelSize, 0); // ����ƫ����
                col.rgb += tex2D(_MainTex, i.uv - offset) * i.color - col.rgb; // ��ԭ����ɫ��������������ɫ
                col.a *= _OutlineWidth * outlineAlpha * _OutlinePixelSize; // ����͸����
                return col;
            }
            ENDCG
        }
    }
}
