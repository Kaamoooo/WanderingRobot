#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"

sampler2D _DissolveTex;
float _DissolveAmount;
fixed4 _BasicColor;

float _SpecStrength;
float _DiffuseStrength;
float _AmbientStrength;

float4 _AmbientColor;

struct appdata {
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
    float3 normal : NORMAL;
};

struct v2f {
    float2 uv : TEXCOORD0;
    float3 worldPos : TEXCOORD1;
    float3 worldNormal : TEXCOORD2;
    float4 pos : SV_POSITION;
    UNITY_LIGHTING_COORDS(3,4)

};

            // Vertex shader
v2f vert (appdata v) {
    v2f o;
    o.uv = v.uv;
    o.pos = UnityObjectToClipPos(v.vertex);

    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
    o.worldNormal = UnityObjectToWorldNormal(v.normal);
    TRANSFER_VERTEX_TO_FRAGMENT(o)
    return o;
}

// Fragment shader
float4 frag (v2f i) : SV_Target {



     float4 dissolveTex = tex2D(_DissolveTex, i.uv);
    float dissolve = dissolveTex.g;
    if (dissolve < _DissolveAmount) {
        discard;
    }
               
    float3 N = normalize(i.worldNormal);
    float3 L = normalize(UnityWorldSpaceLightDir(i.worldPos));
    float3 V = normalize(UnityWorldSpaceViewDir(i.worldPos));   
    float3 H = normalize(L + V);

    float attenuation = LIGHT_ATTENUATION(i);

    float4 diffuse = _BasicColor * _DiffuseStrength * saturate(dot(N, L));

    float4 spec = _LightColor0 * _SpecStrength * saturate(dot(H,N));
    float4 amb = _AmbientColor * _AmbientStrength;
    
    return diffuse + spec + amb;
}
