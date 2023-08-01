Shader "PostProcessing/ScanEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScanDistance("Scan Distance",Float)=1
        _WorldScanOrigin("Scan Origin",Vector)=(0,0,0)
        _Thickness("Thickness",Float)=0.2
        _MaxVisualDistance("Max Visual Distance",Float)=2
        _ScanLightingWidth("Scan Lighting Width",Float)=1
        _Brightness("Brightness",Float)=1
        [HDR]_BasicColor("Basic Color", Color)=(1,1,1,1)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 viewPos:TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            float _ScanDistance;
            float3 _WorldScanOrigin;
            float _Thickness;
            float _MaxVisualDistance;
            float _ScanLightingWidth;
            float _Brightness;
            float4 _BasicColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float4 screenPos=ComputeScreenPos(o.vertex);
                float4 ndc=(screenPos/screenPos.w)*2-1;
                float3 clipPos=float3(ndc.x,ndc.y,ndc.z*-1)*_ProjectionParams.z;
                o.viewPos=mul(unity_CameraInvProjection,clipPos.xyzz);

                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float depth=SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,i.uv);
                depth=Linear01Depth(depth);
                float4 worldPos=mul(unity_CameraToWorld,float4(i.viewPos*depth,1.0));

                fixed4 output=(0,0,0,0);
                float worldDistance=distance(worldPos.xyz,_WorldScanOrigin);
                float scanCenterDistance = abs( distance(worldPos.xyz,_WorldScanOrigin) - _ScanDistance);

                if(worldDistance<_MaxVisualDistance){
                    output=tex2D(_MainTex,i.uv)*(1-worldDistance/_MaxVisualDistance)*_Brightness;
                }else if( scanCenterDistance< _Thickness)
                {
                    output=_BasicColor;
                }else if(scanCenterDistance<(_Thickness+_ScanLightingWidth)){
                    output=tex2D(_MainTex,i.uv)*(1-(scanCenterDistance-_Thickness)/_ScanLightingWidth);
                }
                else
                {
                    output=fixed4(0,0,0,0);
                }

                return output;
            }
            ENDCG
        }
    }
}
