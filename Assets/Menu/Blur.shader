// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SimpleGrabPassBlur" {
    Properties {
        _distance ("Distance", Range(0, 20)) = 1
		_Kernel("Kernel", Int) = 7
    }
 
    Category {
 
        // We must be transparent, so other objects are drawn before this one.
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Opaque" }
 
 
        SubShader {
     
            // Horizontal blur
            GrabPass {                    
                Tags { "LightMode" = "Always" }
            }
            Pass {
                Tags { "LightMode" = "Always" }
             
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
             
                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 texcoord: TEXCOORD0;
                };
             
                struct v2f {
                    float4 vertex : POSITION;
                    float4 uvgrab : TEXCOORD0;
                };
             
                v2f vert (appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    #if UNITY_UV_STARTS_AT_TOP
                    float scale = -1.0;
                    #else
                    float scale = 1.0;
                    #endif
                    o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
                    o.uvgrab.zw = o.vertex.zw;
                    return o;
                }
             
                sampler2D _GrabTexture;
                float4 _GrabTexture_TexelSize;
                float _Size;
				int _distance;
				int _Kernel;
             
				 float4 boxOfX(sampler2D tex, float2 uv, float4 size){
				 size = size*_distance;
						//float4 c = tex2D(tex, uv + float2(-size.x, size.y)) + tex2D(tex, uv + float2(0, size.y)) + tex2D(tex, uv + float2(size.x, size.y)) +
						//tex2D(tex, uv + float2(-size.x, 0)) + tex2D(tex, uv + float2(0, 0)) + tex2D(tex, uv + float2(size.x, 0)) +
						//tex2D(tex, uv + float2(-size.x, -size.y)) + tex2D(tex, uv + float2(0, -size.y)) + tex2D(tex, uv + float2(size.x, -size.y));
						float4 c;
						if (_Kernel == 7){
						c = 
						tex2D(tex, uv+float2(-size.x,0)) *		0.143392
						+ tex2D(tex, uv+float2(size.x,0)) *		0.143392
						+ tex2D(tex, uv+float2(-2*size.x,0)) *		0.142856
						+ tex2D(tex, uv+float2(2*size.x,0)) 	*		0.142856
						+ tex2D(tex, uv+float2(-3*size.x,0)) *	0.141966
						+ tex2D(tex, uv+float2(3*size.x,0)) *	0.141966
						+tex2D(tex, uv+float2(0,0)) *0.143572;
						}

						else if (_Kernel == 5){
						c = 
						tex2D(tex, uv+float2(-size.x,0)) *		0.20025
						+ tex2D(tex, uv+float2(size.x,0)) *			0.20025
						+ tex2D(tex, uv+float2(-2*size.x,0)) *			0.1995
						+ tex2D(tex, uv+float2(2*size.x,0)) 	*			0.1995
						+tex2D(tex, uv+float2(0,0)) *0.2005;
						}

						else if (_Kernel == 3){
						c = 
						tex2D(tex, uv+float2(-size.x,0)) *			0.333194
						+ tex2D(tex, uv+float2(size.x,0)) *				0.333194
						+tex2D(tex, uv+float2(0,0)) *	0.333611;
						};
	


						return c;
					}
			float4 frag (v2f i) : SV_Target
			{
				float4 col = boxOfX(_GrabTexture, i.uvgrab, _GrabTexture_TexelSize);
				return col;
			}

                ENDCG
            }          
			
		 GrabPass {                    
                Tags { "LightMode" = "Always" }
            }
			
			Pass {
                Tags { "LightMode" = "Always" }
             
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
             
                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 texcoord: TEXCOORD0;
                };
             
                struct v2f {
                    float4 vertex : POSITION;
                    float4 uvgrab : TEXCOORD0;
                };
             
                v2f vert (appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    #if UNITY_UV_STARTS_AT_TOP
                    float scale = -1.0;
                    #else
                    float scale = 1.0;
                    #endif
                    o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
                    o.uvgrab.zw = o.vertex.zw;
                    return o;
                }
             
                sampler2D _GrabTexture;
                float4 _GrabTexture_TexelSize;
                float _Size;
				int _distance;
				int _Kernel;

				float4 boxOfY(sampler2D tex, float2 uv, float4 size){
				 size = size*_distance;
						//float4 c = tex2D(tex, uv + float2(-size.x, size.y)) + tex2D(tex, uv + float2(0, size.y)) + tex2D(tex, uv + float2(size.x, size.y)) +
						//tex2D(tex, uv + float2(-size.x, 0)) + tex2D(tex, uv + float2(0, 0)) + tex2D(tex, uv + float2(size.x, 0)) +
						//tex2D(tex, uv + float2(-size.x, -size.y)) + tex2D(tex, uv + float2(0, -size.y)) + tex2D(tex, uv + float2(size.x, -size.y));
						float4 c ;
						if (_Kernel==7){
						c= tex2D(tex, uv+float2(0,-size.y)) *		0.143392
						+ tex2D(tex, uv+float2(0,size.y)) *		0.143392
						+ tex2D(tex, uv+float2(0,-2*size.y)) *		0.142856
						+ tex2D(tex, uv+float2(0,2*size.y)) 	*		0.142856
						+ tex2D(tex, uv+float2(0,-3*size.y)) *	0.141966
						+ tex2D(tex, uv+float2(0,3*size.y)) *	0.141966
						+tex2D(tex, uv+float2(0,0)) *		0.143572;
						}
						else if (_Kernel==5){
						c= tex2D(tex, uv+float2(0,-size.y)) *		0.20025
						+ tex2D(tex, uv+float2(0,size.y)) *		0.20025
						+ tex2D(tex, uv+float2(0,-2*size.y)) *			0.1995
						+ tex2D(tex, uv+float2(0,2*size.y)) 	*			0.1995
						+tex2D(tex, uv+float2(0,0)) *			0.2005;
						}
						else if (_Kernel==3){
						c= tex2D(tex, uv+float2(0,-size.y)) *			0.333194
						+ tex2D(tex, uv+float2(0,size.y)) *			0.333194
						+tex2D(tex, uv+float2(0,0)) *			0.333611;
						};

							

						return c;
					}

			float4 frag (v2f i) : SV_Target
			{
				float4 col = boxOfY(_GrabTexture, i.uvgrab, _GrabTexture_TexelSize);
				return col;
			}

                ENDCG
            }
           
            
        }
    }
}