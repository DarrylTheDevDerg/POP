Shader "Custom/CurvedSprite"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CurveX ("Curve X", Float) = 0.2
        _CurveY ("Curve Y", Float) = 0.0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
                float4 color  : COLOR;
            };

            struct v2f
            {
                float2 uv     : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color  : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _CurveX;
            float _CurveY;

            v2f vert (appdata v)
            {
                v2f o;

                float3 pos = v.vertex.xyz;

                // Curve horizontally
                pos.y += (pos.x * pos.x) * _CurveX;

                // Curve vertically
                pos.x += (pos.y * pos.y) * _CurveY;

                o.vertex = UnityObjectToClipPos(float4(pos, 1.0));
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                return col;
            }
            ENDCG
        }
    }
}