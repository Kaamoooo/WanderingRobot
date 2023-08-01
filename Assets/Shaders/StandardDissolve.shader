Shader "Custom/StandardDissolve" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _DissolveTex ("Dissolve Texture", 2D) = "white" {}
        _DissolveAmount ("Dissolve Amount", Range(0.0, 1.0)) = 0.0
        _NormalMap ("Normal Map", 2D)="white"{}
        _BasicColor("Basic Color", Color) = (1,1,1,1)

        _SpecStrength("Specular Strength" , Range(0,1)) = 0.5
        _DiffuseStrength("Diffuse Strength" , Range(0,1)) = 0.5
        _AmbientStrength("Ambient Strength" , Range(0,1)) = 0.5
        _AmbientColor("Ambient Color" , Color) = (0,0,0,0)
    }

    SubShader {

        Pass {
            Tags { "RenderType"="Opaque" "LightMode"="ForwardBase"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #define Base_Lighting

            #include "StandardDissolve.cginc"
           

            ENDCG
            }
        
        
        pass{
            Tags {"LightMode"="ForwardAdd"}
            Blend One OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd

            #include "StandardDissolve.cginc"
            
            ENDCG

        }
    }
}