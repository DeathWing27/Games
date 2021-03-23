Shader "Doctrina/Lava" {
	//входные параметры в материал, не факт, что используются в шейдере
    Properties {
      _Color ("Color", Color) = (1,1,1,1)
      _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader {
      
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert //Lambert - модель освещения


      //входные данные в пиксельном шейдере
      struct Input {
          float2 uv_MainTex; //текстурные координаты
      };

      sampler2D _MainTex; //объявляем текстуру название переменной совпадает с Properties
      float4 _Color; //тоже что и Vector4
      
      void surf (Input IN, inout SurfaceOutput o) {
      	  
      	  float4 c = tex2D (_MainTex, IN.uv_MainTex+ float2( _Time.x * 1.5, 0 ) );//считываем цвет текстуры по UV в переменную 
      	  float4 c2 = tex2D (_MainTex, IN.uv_MainTex*1.5+ float2(  0, _Time.x * 1.5) );//считываем цвет текстуры по UV в переменную 

          o.Albedo = (c * c2 ) * 1.2 * _Color; //записываем цвет в Albedo
          
      }
      
      ENDCG
    } 
    Fallback "Diffuse" //если шейдер не поддерживается, то выберется этот
  }