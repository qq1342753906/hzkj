Shader "Custom/OutlineShader2"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth("Outline Width", Range(0, 0.1)) = 0.01
    }
    
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineWidth;
            
            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            half4 frag(v2f i) : SV_Target
            {
                // Sample the main texture
                half4 texColor = tex2D(_MainTex, i.uv);
                
                // Calculate outline based on the difference in texture sampling
                half4 outline = texColor - tex2D(_MainTex, i.uv + _OutlineWidth);
                
                // Apply the outline color
                outline.rgb = _OutlineColor.rgb * outline.a;
                outline.a = texColor.a;
                
                return outline;
            }
            ENDCG
        }
    }
}
