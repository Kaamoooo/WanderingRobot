Shader "Unlit/ParticleFlame"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DissolveTex("Dissolve Texture", 2D) = "white"{}
        _DissolveAmt("Dissolve Amount",Float)=1
        [HDR]_Color("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True"}
        ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha

        LOD 100

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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _DissolveTex;
            float _DissolveAmt;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 texCol = pow( tex2D(_MainTex,i.uv), _DissolveAmt);
                float alpha=texCol.a * i.color.a;
                if(alpha<0.5) discard;

                float4 ret = tex2D(_DissolveTex,texCol.xy);
                ret *= _Color;
                ret *= i.color;
                return float4(ret.rgb,alpha);
            }
            ENDCG
        }
    }
}
