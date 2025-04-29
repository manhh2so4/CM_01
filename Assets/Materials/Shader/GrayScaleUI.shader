Shader "UI/Grayscale" // Tên Shader hiển thị trong Inspector (bạn có thể đổi)
{
    Properties
    {
        // Thuộc tính chính cho Sprite Renderer hoặc Image component
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        // Màu phủ (giữ lại để tương thích với tint color của Image/SpriteRenderer)
        _Color ("Tint", Color) = (1,1,1,1)

        // Thuộc tính Stencil thường dùng cho UI Masking
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        // Thuộc tính ColorMask thường dùng cho UI
        _ColorMask ("Color Mask", Float) = 15

        // Tùy chọn để bật/tắt Alpha Clipping (cắt bỏ pixel dựa trên alpha)
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        // Tags quan trọng cho việc render UI
        Tags
        {
            "Queue"="Transparent" // Render trong hàng đợi trong suốt
            "IgnoreProjector"="True"
            "RenderType"="Transparent" // Loại render
            "PreviewType"="Plane"      // Cách hiển thị preview trong editor
            "CanUseSpriteAtlas"="True" // Cho phép sử dụng Sprite Atlas
        }

        // Cấu hình Stencil buffer (dùng cho UI Mask)
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        // Các trạng thái render cơ bản cho UI
        Cull Off // Không loại bỏ mặt nào (vẽ cả mặt trước và sau)
        Lighting Off // Không bị ảnh hưởng bởi ánh sáng
        ZWrite Off // Không ghi vào Z-buffer (quan trọng cho UI trong suốt)
        ZTest [unity_GUIZTestMode] // Chế độ kiểm tra Z-buffer cho UI (thường là Always hoặc LEqual)
        Blend SrcAlpha OneMinusSrcAlpha // Chế độ hòa trộn alpha thông thường
        ColorMask [_ColorMask] // Áp dụng color mask

        Pass
        {
            Name "Default" // Tên của Pass
            CGPROGRAM // Bắt đầu khối mã CG/HLSL
            #pragma vertex vert // Khai báo hàm xử lý đỉnh (vertex shader) là 'vert'
            #pragma fragment frag // Khai báo hàm xử lý pixel (fragment shader) là 'frag'
            #pragma target 2.0 // Chỉ định target shader model (2.0 khá cơ bản, tương thích rộng)

            // Include các file thư viện của Unity chứa hàm và biến hữu ích
            #include "UnityCG.cginc"
            #include "UnityUI.cginc" // Thư viện đặc biệt cho UI

            // Các chỉ thị biên dịch đa luồng cục bộ cho các tính năng UI
            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT // Hỗ trợ cắt theo hình chữ nhật (RectMask2D)
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP // Hỗ trợ alpha clipping

            // Dữ liệu đầu vào cho vertex shader từ mesh
            struct appdata_t
            {
                float4 vertex   : POSITION; // Vị trí đỉnh trong không gian local
                float4 color    : COLOR;    // Màu đỉnh (thường là màu trắng, dùng cho Tint)
                float2 texcoord : TEXCOORD0;// Tọa độ UV của texture
                UNITY_VERTEX_INPUT_INSTANCE_ID // Hỗ trợ GPU instancing (nếu cần)
            };

            // Dữ liệu đầu ra từ vertex shader, truyền vào fragment shader
            struct v2f
            {
                float4 vertex   : SV_POSITION; // Vị trí đỉnh đã chuyển đổi sang không gian clip
                fixed4 color    : COLOR;       // Màu đỉnh đã được xử lý (nhân với Tint)
                float2 texcoord : TEXCOORD0;   // Tọa độ UV đã được xử lý (tiling/offset)
                float4 worldPosition : TEXCOORD1; // Vị trí thế giới (cần cho clipping)
                UNITY_VERTEX_OUTPUT_STEREO // Hỗ trợ render stereo (VR)
            };

            // Khai báo các biến tương ứng với Properties
            sampler2D _MainTex; // Texture chính (Sprite)
            fixed4 _Color; // Màu Tint từ Inspector hoặc component Image
            float4 _ClipRect; // Hình chữ nhật cắt (từ RectMask2D)
            float4 _MainTex_ST; // Biến chứa thông tin Tiling (xy) và Offset (zw) của _MainTex

            // Hàm Vertex Shader
            v2f vert(appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v); // Khởi tạo instance ID (nếu có)
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); // Khởi tạo cho stereo rendering

                // Lưu vị trí thế giới trước khi chuyển đổi (cần cho clipping)
                o.worldPosition = v.vertex;
                // Chuyển đổi vị trí đỉnh từ không gian local sang không gian clip
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Áp dụng Tiling và Offset cho tọa độ UV
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                // Nhân màu đỉnh (thường là trắng) với màu Tint (_Color)
                // Màu Tint này lấy từ thuộc tính Color của Image component
                o.color = v.color * _Color;
                return o; // Trả về dữ liệu đã xử lý cho fragment shader
            }

            // Hàm Fragment Shader (xử lý từng pixel)
            fixed4 frag(v2f i) : SV_Target // SV_Target chỉ định giá trị trả về là màu của pixel
            {
                // 1. Lấy mẫu màu từ texture tại tọa độ UV
                // fixed4 là kiểu dữ liệu màu với độ chính xác thấp hơn float4, thường đủ cho màu sắc
                fixed4 texColor = tex2D(_MainTex, i.texcoord);

                // 2. Tính toán giá trị độ sáng (Luminance) để chuyển sang đen trắng
                // Công thức chuẩn: gray = 0.299*Red + 0.587*Green + 0.114*Blue
                // Dùng hàm dot product để tính toán nhanh hơn: dot(color.rgb, float3(0.299, 0.587, 0.114))
                float gray = dot(texColor.rgb, fixed3(0.299, 0.587, 0.114));

                // 3. Tạo màu đen trắng: gán giá trị gray cho cả 3 kênh R, G, B
                // Giữ nguyên giá trị Alpha (độ trong suốt) từ texture gốc
                fixed4 grayColor = fixed4(gray, gray, gray, texColor.a);

                // 4. Nhân kết quả với màu đỉnh (i.color)
                // Màu đỉnh đã bao gồm màu Tint từ Image và alpha tổng thể
                // Điều này đảm bảo hiệu ứng Tint và độ trong suốt của Image vẫn hoạt động
                grayColor *= i.color;

                // 5. Áp dụng Clipping (nếu được bật và có RectMask2D)
                #ifdef UNITY_UI_CLIP_RECT
                    // Tính toán hệ số alpha dựa trên việc pixel có nằm trong vùng cắt không
                    // và nhân nó vào alpha của màu cuối cùng
                    grayColor.a *= UnityGet2DClipping(i.worldPosition.xy, _ClipRect);
                #endif

                // 6. Áp dụng Alpha Clipping (nếu được bật)
                #ifdef UNITY_UI_ALPHACLIP
                    // Loại bỏ pixel nếu alpha của nó thấp hơn ngưỡng (thường gần 0)
                    clip (grayColor.a - 0.001);
                #endif

                // 7. Trả về màu cuối cùng cho pixel này
                return grayColor;
            }
            ENDCG // Kết thúc khối mã CG/HLSL
        }
    }
}