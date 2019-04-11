// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|diff-6276-OUT,spec-775-OUT,gloss-9812-OUT,normal-7279-RGB;n:type:ShaderForge.SFN_Tex2d,id:1638,x:31219,y:32005,ptovrint:False,ptlb:BC_Texture,ptin:_BC_Texture,varname:node_1638,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:167222923051c6343a83263e3f15604d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7437,x:31439,y:31716,ptovrint:False,ptlb:BC_BlendTex,ptin:_BC_BlendTex,varname:node_7437,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0175b4f306ef10144b877a91147952cf,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4092,x:31219,y:32203,ptovrint:False,ptlb:Mask_Texture,ptin:_Mask_Texture,varname:node_4092,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1fa21822655213545a64849f57a7b248,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:586,x:31817,y:32374,ptovrint:False,ptlb:BC_Tu,ptin:_BC_Tu,varname:node_586,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5dc8a225dd75987468ec68d1938a3bc6,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4779,x:33092,y:32486,ptovrint:False,ptlb:node_4779,ptin:_node_4779,varname:node_4779,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f8e2244331d5cde4a9ad59006e022e81,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Slider,id:775,x:32062,y:32760,ptovrint:False,ptlb:Spec_Intensity,ptin:_Spec_Intensity,varname:node_775,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2,max:1;n:type:ShaderForge.SFN_Slider,id:9812,x:32062,y:32894,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_9812,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;n:type:ShaderForge.SFN_Multiply,id:5035,x:31439,y:31896,varname:node_5035,prsc:2|A-1048-RGB,B-1638-RGB;n:type:ShaderForge.SFN_Tex2d,id:7279,x:32129,y:33061,ptovrint:False,ptlb:N_Texture,ptin:_N_Texture,varname:node_7279,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f4c5ec761598d0748b5161c5c22eb1b4,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Color,id:1048,x:31219,y:31767,ptovrint:False,ptlb:BC_Tint,ptin:_BC_Tint,varname:node_1048,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:5797,x:31726,y:32032,varname:node_5797,prsc:2|A-7437-RGB,B-5035-OUT,T-8000-OUT;n:type:ShaderForge.SFN_Power,id:8000,x:31622,y:32209,varname:node_8000,prsc:2|VAL-4092-R,EXP-4787-OUT;n:type:ShaderForge.SFN_Slider,id:4787,x:31259,y:32382,ptovrint:False,ptlb:MaskContrast,ptin:_MaskContrast,varname:node_4787,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:5;n:type:ShaderForge.SFN_Multiply,id:2680,x:31996,y:32280,varname:node_2680,prsc:2|A-3002-RGB,B-586-RGB;n:type:ShaderForge.SFN_Color,id:3002,x:31817,y:32197,ptovrint:False,ptlb:BC_TuColor,ptin:_BC_TuColor,varname:node_3002,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:6276,x:32411,y:32114,varname:node_6276,prsc:2|A-5797-OUT,B-2680-OUT,T-3948-OUT;n:type:ShaderForge.SFN_Tex2d,id:7698,x:32167,y:32388,ptovrint:False,ptlb:Mask_Texture_tu,ptin:_Mask_Texture_tu,varname:_Mask_Texture_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1fa21822655213545a64849f57a7b248,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Power,id:3948,x:32381,y:32375,varname:node_3948,prsc:2|VAL-7698-RGB,EXP-675-OUT;n:type:ShaderForge.SFN_Vector1,id:675,x:32242,y:32558,varname:node_675,prsc:2,v1:5;proporder:1638-775-9812-7279-1048-7437-4092-4787-586-3002-7698;pass:END;sub:END;*/

Shader "Shader Forge/SD_CaoDi" {
    Properties {
        _BC_Texture ("BC_Texture", 2D) = "white" {}
        _Spec_Intensity ("Spec_Intensity", Range(0, 1)) = 0.2
        _Gloss ("Gloss", Range(0, 1)) = 0.1
        _N_Texture ("N_Texture", 2D) = "bump" {}
        _BC_Tint ("BC_Tint", Color) = (1,1,1,1)
        _BC_BlendTex ("BC_BlendTex", 2D) = "white" {}
        _Mask_Texture ("Mask_Texture", 2D) = "white" {}
        _MaskContrast ("MaskContrast", Range(0, 5)) = 2
        _BC_Tu ("BC_Tu", 2D) = "white" {}
        _BC_TuColor ("BC_TuColor", Color) = (0.5,0.5,0.5,1)
        _Mask_Texture_tu ("Mask_Texture_tu", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _BC_Texture; uniform float4 _BC_Texture_ST;
            uniform sampler2D _BC_BlendTex; uniform float4 _BC_BlendTex_ST;
            uniform sampler2D _Mask_Texture; uniform float4 _Mask_Texture_ST;
            uniform sampler2D _BC_Tu; uniform float4 _BC_Tu_ST;
            uniform float _Spec_Intensity;
            uniform float _Gloss;
            uniform sampler2D _N_Texture; uniform float4 _N_Texture_ST;
            uniform float4 _BC_Tint;
            uniform float _MaskContrast;
            uniform float4 _BC_TuColor;
            uniform sampler2D _Mask_Texture_tu; uniform float4 _Mask_Texture_tu_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _N_Texture_var = UnpackNormal(tex2D(_N_Texture,TRANSFORM_TEX(i.uv0, _N_Texture)));
                float3 normalLocal = _N_Texture_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Spec_Intensity,_Spec_Intensity,_Spec_Intensity);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _BC_BlendTex_var = tex2D(_BC_BlendTex,TRANSFORM_TEX(i.uv0, _BC_BlendTex));
                float4 _BC_Texture_var = tex2D(_BC_Texture,TRANSFORM_TEX(i.uv0, _BC_Texture));
                float4 _Mask_Texture_var = tex2D(_Mask_Texture,TRANSFORM_TEX(i.uv0, _Mask_Texture));
                float4 _BC_Tu_var = tex2D(_BC_Tu,TRANSFORM_TEX(i.uv0, _BC_Tu));
                float4 _Mask_Texture_tu_var = tex2D(_Mask_Texture_tu,TRANSFORM_TEX(i.uv0, _Mask_Texture_tu));
                float3 diffuseColor = lerp(lerp(_BC_BlendTex_var.rgb,(_BC_Tint.rgb*_BC_Texture_var.rgb),pow(_Mask_Texture_var.r,_MaskContrast)),(_BC_TuColor.rgb*_BC_Tu_var.rgb),pow(_Mask_Texture_tu_var.rgb,5.0));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _BC_Texture; uniform float4 _BC_Texture_ST;
            uniform sampler2D _BC_BlendTex; uniform float4 _BC_BlendTex_ST;
            uniform sampler2D _Mask_Texture; uniform float4 _Mask_Texture_ST;
            uniform sampler2D _BC_Tu; uniform float4 _BC_Tu_ST;
            uniform float _Spec_Intensity;
            uniform float _Gloss;
            uniform sampler2D _N_Texture; uniform float4 _N_Texture_ST;
            uniform float4 _BC_Tint;
            uniform float _MaskContrast;
            uniform float4 _BC_TuColor;
            uniform sampler2D _Mask_Texture_tu; uniform float4 _Mask_Texture_tu_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _N_Texture_var = UnpackNormal(tex2D(_N_Texture,TRANSFORM_TEX(i.uv0, _N_Texture)));
                float3 normalLocal = _N_Texture_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Spec_Intensity,_Spec_Intensity,_Spec_Intensity);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _BC_BlendTex_var = tex2D(_BC_BlendTex,TRANSFORM_TEX(i.uv0, _BC_BlendTex));
                float4 _BC_Texture_var = tex2D(_BC_Texture,TRANSFORM_TEX(i.uv0, _BC_Texture));
                float4 _Mask_Texture_var = tex2D(_Mask_Texture,TRANSFORM_TEX(i.uv0, _Mask_Texture));
                float4 _BC_Tu_var = tex2D(_BC_Tu,TRANSFORM_TEX(i.uv0, _BC_Tu));
                float4 _Mask_Texture_tu_var = tex2D(_Mask_Texture_tu,TRANSFORM_TEX(i.uv0, _Mask_Texture_tu));
                float3 diffuseColor = lerp(lerp(_BC_BlendTex_var.rgb,(_BC_Tint.rgb*_BC_Texture_var.rgb),pow(_Mask_Texture_var.r,_MaskContrast)),(_BC_TuColor.rgb*_BC_Tu_var.rgb),pow(_Mask_Texture_tu_var.rgb,5.0));
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
