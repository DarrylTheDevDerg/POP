Shader "Custom/SpriteHorizonUV"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _Curve ("Curve", Range(-1,1)) = 0.25
        _Stretch ("Edge Stretch", Range(0,2)) = 0.25
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Curve;
            float _Stretch;

            v2f vert(appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // center UVs
                float2 p = uv * 2.0 - 1.0;

                // horizon arc
                p.y += p.x * p.x * _Curve;

                // widen toward edges
                p.x *= 1.0 - abs(p.y) * _Stretch;

                // back to 0-1
                uv = p * 0.5 + 0.5;

                fixed4 col = tex2D(_MainTex, uv);

                // clip outside warped area
                clip(col.a - 0.001);

                return col * i.color;
            }

            ENDCG
        }
    }
}