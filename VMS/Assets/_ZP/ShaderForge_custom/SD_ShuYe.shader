// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|diff-9218-OUT,spec-3645-OUT,gloss-8355-OUT,normal-3800-OUT,transm-7-RGB,lwrap-6811-RGB,clip-4130-A,voffset-6776-OUT;n:type:ShaderForge.SFN_Tex2d,id:4130,x:31436,y:32213,ptovrint:False,ptlb:Leaf_Tex,ptin:_Leaf_Tex,varname:node_4130,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:cb067e8375db41b40be740e7d977c1f5,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9218,x:31666,y:32099,varname:node_9218,prsc:2|A-4631-RGB,B-4130-RGB;n:type:ShaderForge.SFN_Color,id:4631,x:31436,y:32010,ptovrint:False,ptlb:Leaf_Color,ptin:_Leaf_Color,varname:node_4631,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2327638,c2:0.3455882,c3:0.1245134,c4:1;n:type:ShaderForge.SFN_Slider,id:3645,x:32299,y:32223,ptovrint:False,ptlb:Specular,ptin:_Specular,varname:node_3645,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:8355,x:32298,y:32330,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_8355,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:8289,x:31059,y:32960,ptovrint:False,ptlb:N_Texture,ptin:_N_Texture,varname:node_8289,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:106169ca5874b824c886a633713cea9c,ntxv:3,isnm:True;n:type:ShaderForge.SFN_ComponentMask,id:8558,x:31333,y:32956,varname:node_8558,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-8289-RGB;n:type:ShaderForge.SFN_Multiply,id:1054,x:31529,y:32956,varname:node_1054,prsc:2|A-8558-OUT,B-6791-OUT;n:type:ShaderForge.SFN_Slider,id:6791,x:31196,y:33144,ptovrint:False,ptlb:N_Intensity,ptin:_N_Intensity,varname:node_6791,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Append,id:3800,x:31707,y:33030,varname:node_3800,prsc:2|A-1054-OUT,B-8289-B;n:type:ShaderForge.SFN_Color,id:7,x:32024,y:32717,ptovrint:False,ptlb:Transmission,ptin:_Transmission,varname:node_7,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5310345,c2:1,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:6811,x:32024,y:32880,ptovrint:False,ptlb:Warpping,ptin:_Warpping,varname:node_6811,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.986207,c2:1,c3:0,c4:1;n:type:ShaderForge.SFN_Time,id:4128,x:31893,y:33211,varname:node_4128,prsc:2;n:type:ShaderForge.SFN_Sin,id:7021,x:32086,y:33211,varname:node_7021,prsc:2|IN-4128-T;n:type:ShaderForge.SFN_Multiply,id:6776,x:32463,y:33190,varname:node_6776,prsc:2|A-7021-OUT,B-3537-OUT,C-3053-OUT;n:type:ShaderForge.SFN_Slider,id:3537,x:31856,y:33404,ptovrint:False,ptlb:Wind_Speed,ptin:_Wind_Speed,varname:node_3537,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.07544102,max:1;n:type:ShaderForge.SFN_NormalVector,id:4280,x:31744,y:33492,prsc:2,pt:False;n:type:ShaderForge.SFN_Add,id:5508,x:31959,y:33558,varname:node_5508,prsc:2|A-4280-OUT,B-5308-XYZ;n:type:ShaderForge.SFN_Vector4Property,id:5308,x:31744,y:33670,ptovrint:False,ptlb:Wind_Dirction,ptin:_Wind_Dirction,varname:node_5308,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_Normalize,id:3053,x:32253,y:33484,varname:node_3053,prsc:2|IN-5508-OUT;proporder:4130-4631-3645-8355-8289-6791-7-6811-3537-5308;pass:END;sub:END;*/

Shader "Shader Forge/SD_ShuYe" {
    Properties {
        _Leaf_Tex ("Leaf_Tex", 2D) = "white" {}
        _Leaf_Color ("Leaf_Color", Color) = (0.2327638,0.3455882,0.1245134,1)
        _Specular ("Specular", Range(0, 1)) = 0
        _Gloss ("Gloss", Range(0, 1)) = 0
        _N_Texture ("N_Texture", 2D) = "bump" {}
        _N_Intensity ("N_Intensity", Range(0, 10)) = 1
        _Transmission ("Transmission", Color) = (0.5310345,1,0,1)
        _Warpping ("Warpping", Color) = (0.986207,1,0,1)
        _Wind_Speed ("Wind_Speed", Range(0, 1)) = 0.07544102
        _Wind_Dirction ("Wind_Dirction", Vector) = (0,0,0,0)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Leaf_Tex; uniform float4 _Leaf_Tex_ST;
            uniform float4 _Leaf_Color;
            uniform float _Specular;
            uniform float _Gloss;
            uniform sampler2D _N_Texture; uniform float4 _N_Texture_ST;
            uniform float _N_Intensity;
            uniform float4 _Transmission;
            uniform float4 _Warpping;
            uniform float _Wind_Speed;
            uniform float4 _Wind_Dirction;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_4128 = _Time + _TimeEditor;
                v.vertex.xyz += (sin(node_4128.g)*_Wind_Speed*normalize((v.normal+_Wind_Dirction.rgb)));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _N_Texture_var = UnpackNormal(tex2D(_N_Texture,TRANSFORM_TEX(i.uv0, _N_Texture)));
                float3 normalLocal = float3((_N_Texture_var.rgb.rg*_N_Intensity),_N_Texture_var.b);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _Leaf_Tex_var = tex2D(_Leaf_Tex,TRANSFORM_TEX(i.uv0, _Leaf_Tex));
                clip(_Leaf_Tex_var.a - 0.5);
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
                float3 specularColor = float3(_Specular,_Specular,_Specular);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float3 w = _Warpping.rgb*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                float3 backLight = max(float3(0.0,0.0,0.0), -NdotLWrap + w ) * _Transmission.rgb;
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = (forwardLight+backLight) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuseColor = (_Leaf_Color.rgb*_Leaf_Tex_var.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Leaf_Tex; uniform float4 _Leaf_Tex_ST;
            uniform float4 _Leaf_Color;
            uniform float _Specular;
            uniform float _Gloss;
            uniform sampler2D _N_Texture; uniform float4 _N_Texture_ST;
            uniform float _N_Intensity;
            uniform float4 _Transmission;
            uniform float4 _Warpping;
            uniform float _Wind_Speed;
            uniform float4 _Wind_Dirction;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_4128 = _Time + _TimeEditor;
                v.vertex.xyz += (sin(node_4128.g)*_Wind_Speed*normalize((v.normal+_Wind_Dirction.rgb)));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _N_Texture_var = UnpackNormal(tex2D(_N_Texture,TRANSFORM_TEX(i.uv0, _N_Texture)));
                float3 normalLocal = float3((_N_Texture_var.rgb.rg*_N_Intensity),_N_Texture_var.b);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _Leaf_Tex_var = tex2D(_Leaf_Tex,TRANSFORM_TEX(i.uv0, _Leaf_Tex));
                clip(_Leaf_Tex_var.a - 0.5);
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
                float3 specularColor = float3(_Specular,_Specular,_Specular);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float3 w = _Warpping.rgb*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                float3 backLight = max(float3(0.0,0.0,0.0), -NdotLWrap + w ) * _Transmission.rgb;
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = (forwardLight+backLight) * attenColor;
                float3 diffuseColor = (_Leaf_Color.rgb*_Leaf_Tex_var.rgb);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Leaf_Tex; uniform float4 _Leaf_Tex_ST;
            uniform float _Wind_Speed;
            uniform float4 _Wind_Dirction;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_4128 = _Time + _TimeEditor;
                v.vertex.xyz += (sin(node_4128.g)*_Wind_Speed*normalize((v.normal+_Wind_Dirction.rgb)));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 _Leaf_Tex_var = tex2D(_Leaf_Tex,TRANSFORM_TEX(i.uv0, _Leaf_Tex));
                clip(_Leaf_Tex_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
