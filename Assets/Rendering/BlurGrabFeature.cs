using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlurGrabFeature : ScriptableRendererFeature
{
    class BlurPass : ScriptableRenderPass
    {

        RenderTexture blurred;
        Material replaceMat, blurMat;
        RenderTargetIdentifier source;
        RenderTargetHandle tempTex;
        RenderStateBlock renderStateBlock;
        RenderTexture copyTex;
        ShaderTagId m_ShaderTagId = new ShaderTagId("BlurPass");

        OutlineSettings settings;

        public BlurPass(RenderTexture tex)
        {
            copyTex = tex;
            tempTex.Init("_TemporaryColorTexture");
            renderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
        }

        public void Setup(RenderTargetIdentifier source, OutlineSettings settings)
        {
            this.settings = settings;
            this.source = source;
        }

        // This method is called before executing the render pass.
        // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
        // When empty this render pass will render to the active camera render target.
        // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
        // The render pipeline will ensure target setup and clearing happens in an performance manner.
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            cmd.GetTemporaryRT(tempTex.id, cameraTextureDescriptor);
            replaceMat = new Material(Shader.Find("Outline/GlowReplace"));
            blurMat = new Material(Shader.Find("Outline/Blur"));
            blurMat.SetVector("_BlurSize", new Vector2(0.001f, 0.001f));
        }

        // Here you can implement the rendering logic.
        // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
        // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
        // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get("Blur crl");
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();

            Blit(cmd, source, tempTex.Identifier());
            for (int i = 0; i < 4; i++)
            {
                Blit(cmd, tempTex.Identifier(), source, blurMat, 0);
                Blit(cmd, source, tempTex.Identifier(), blurMat, 1);
            }
            Blit(cmd, tempTex.Identifier(), copyTex);
            Blit(cmd, tempTex.Identifier(), source);
            context.ExecuteCommandBuffer(cmd);



            var sortFlags = renderingData.cameraData.defaultOpaqueSortFlags;
            var drawSettings = CreateDrawingSettings(m_ShaderTagId, ref renderingData, sortFlags);
            drawSettings.overrideMaterial = settings.outlineMaterial;
            drawSettings.overrideMaterialPassIndex = 0;
            FilteringSettings filters = new FilteringSettings(RenderQueueRange.opaque, settings.layerMask);
            context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filters, ref renderStateBlock);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        /// Cleanup any allocated resources that were created during the execution of this render pass.
        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tempTex.id);
        }
    }

    [System.Serializable]
    public class OutlineSettings
    {
        public Material outlineMaterial = null;
        public LayerMask layerMask;
    }

    public OutlineSettings settings = new OutlineSettings();
    private RenderTexture texture;
    BlurPass m_ScriptablePass;


    public override void Create()
    {
        texture = new RenderTexture(Screen.width, Screen.height, 24);
        m_ScriptablePass = new BlurPass(texture);

        // Configures where the render pass should be injected.
        m_ScriptablePass.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        //texture.Dump("crl.png");
        m_ScriptablePass.Setup(renderer.cameraColorTarget, settings);
        renderer.EnqueuePass(m_ScriptablePass);
    }
}


