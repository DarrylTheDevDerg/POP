Shader "Custom/SpriteHorizonCurve"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Curve ("Curve", Range(-2,2)) = 0.5
        _Squash ("Vertical Squash", Range(0,1)) = 0.15
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
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
                float4 vertex : SV_POSITION;
                float2 uv     : TEXCOORD0;
                float4 color  : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Curve;
            float _Squash;

            v2f vert(appdata v)
            {
                v2f o;

                float3 pos = v.vertex.xyz;

                // normalized local x (-1 -> 1)
                float x = v.uv.x * 2.0 - 1.0;

                // arc shape
                float curveOffset = -(x * x) * _Curve;

                // squash edges downward slightly
                pos.y += curveOffset;

                // optional horizon stretch
                float stretch = 1.0 + abs(x) * _Squash;
                pos.x *= stretch;

                o.vertex = UnityObjectToClipPos(float4(pos,1));
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv) * i.color;
            }

            ENDCG
        }
    }
}