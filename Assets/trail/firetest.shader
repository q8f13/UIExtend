// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Silhouette/Procedural Geometry Silhouette" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Outline ("Outline", Range(0,1)) = 0.1
        _OutlineColor ("OutlineColor", Color) = (1,1,1,1)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass {
            Tags { "LightMode"="ForwardBase" } 

            Cull Back   
            Lighting On  

            CGPROGRAM
            #include "UnityCG.cginc"
            #include "Lighting.cginc"  
            #include "AutoLight.cginc" 

            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;

            struct v2f {
                float4 pos : SV_POSITION;   
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata_full i) {
                v2f o;
                o.pos= UnityObjectToClipPos(i.vertex);
                o.uv = i.texcoord;

                TRANSFER_VERTEX_TO_FRAGMENT(o); 

                return o;
            }

            fixed4 frag(v2f i) : COLOR {
                fixed3 col = tex2D(_MainTex, i.uv).rgb; 

                fixed4 fragColor;
                fragColor.rgb = col;
                fragColor.a = 1.0;

                return fragColor;
            }

            ENDCG
        }

        Pass {
            Tags { "LightMode"="ForwardBase" } 

            Cull Front   
            Lighting Off

            CGPROGRAM
            #include "UnityCG.cginc"
            #include "Lighting.cginc"  
            #include "AutoLight.cginc" 

            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float _Outline;
            fixed4 _OutlineColor;

            struct v2f {
                float4 pos : SV_POSITION;   
                float2 uv : TEXCOORD0;
            };

            void ZBiasMethod(appdata_full i, inout v2f o) {
                float4 viewPos = mul(UNITY_MATRIX_MV, i.vertex);
                viewPos.z += _Outline;

                o.pos = mul(UNITY_MATRIX_P, viewPos);
            }

            void VertexNormalMethod0(appdata_full i, inout v2f o) {
                o.pos = UnityObjectToClipPos(i.vertex);

                float3 normal = mul ((float3x3)UNITY_MATRIX_IT_MV, i.normal);
                float2 offset = TransformViewToProjection(normal.xy);

                // Only modify the xy components
                // Multiply o.pos.z, as a result the width of your outline will depend on the distance from viewer
                o.pos.xy += offset * o.pos.z * _Outline;
            }

            void VertexNormalMethod1(appdata_full i, inout v2f o) {
                float4 viewPos = mul(UNITY_MATRIX_MV, i.vertex);

                float3 normal = mul( (float3x3)UNITY_MATRIX_IT_MV, i.normal);
                // This is a tricky operation
                // The value of z avoid the expended backfaces to intersect with frontfaces
                // When z = 0.0,  it is equal to VertexNormalMethod0
                normal.z = -1.0;  
                viewPos = viewPos + float4(normalize(normal),0) * _Outline;  

                o.pos = mul(UNITY_MATRIX_P, viewPos);
            }

            v2f vert(appdata_full i) {
                v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f, o);

				// ZBiasMethod(i, o);
             VertexNormalMethod0(i, o);
                // VertexNormalMethod1(i, o);

                o.uv = i.texcoord;

                TRANSFER_VERTEX_TO_FRAGMENT(o); 

                return o;
            }

            fixed4 frag(v2f i) : COLOR {
                return _OutlineColor;  
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}