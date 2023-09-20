Shader "Custom/OutlineShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {} // 主贴图
        _OutlineColor ("Outline Color", Color) = (1, 1, 1, 1) // 描边颜色
        _OutlineWidth ("Outline Width", Range(0.1, 5.0)) = 0.5 // 描边宽度
        _OutlinePixelSize ("Outline Pixel Size", Range(0.1, 5.0)) = 1.0 // 描边像素大小
    }

    SubShader {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // 输入结构体，包含顶点位置、UV坐标、顶点颜色
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            // 输出结构体，包含UV坐标、裁剪空间顶点位置、颜色
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex; // 主贴图
            float4 _MainTex_ST;
            float4 _OutlineColor; // 描边颜色
            float _OutlineWidth; // 描边宽度
            float _OutlinePixelSize; // 描边像素大小

            // 顶点着色器
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // 将顶点位置转换到裁剪空间
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // 根据贴图坐标转换
                o.color = v.color * _OutlineColor; // 乘以描边颜色
                return o;
            }

            // 片元着色器
            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv) * i.color; // 根据贴图和颜色计算颜色
                float outlineAlpha = step(1, col.a); // 根据颜色的透明度判断是否在边缘
                float2 offset = float2(_OutlineWidth * outlineAlpha * _OutlinePixelSize, 0); // 计算偏移量
                col.rgb += tex2D(_MainTex, i.uv - offset) * i.color - col.rgb; // 在原来颜色基础上添加描边颜色
                col.a *= _OutlineWidth * outlineAlpha * _OutlinePixelSize; // 调整透明度
                return col;
            }
            ENDCG
        }
    }
}
