// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using uei = UnityEngine.Internal;

namespace UnityEngine
{
    [RequiredByNativeCode]
    public struct Resolution
    {
        // Keep in sync with ScreenManager::Resolution
        private int m_Width;
        private int m_Height;
        private RefreshRate m_RefreshRate;

        public int width        { get { return m_Width; } set { m_Width = value; } }
        public int height       { get { return m_Height; } set { m_Height = value; } }
        public RefreshRate refreshRateRatio { get { return m_RefreshRate; } set { m_RefreshRate = value; } }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("Resolution.refreshRate is obsolete. Use refreshRateRatio instead.", false)]
        public int refreshRate { get { return (int)Math.Round(m_RefreshRate.value); } set { m_RefreshRate.numerator = (uint)value; m_RefreshRate.denominator = 1; } }

        public override string ToString()
        {
            return string.Format("{0} x {1} @ {2}Hz", m_Width, m_Height, m_RefreshRate);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public partial struct RenderBuffer
    {
        internal int m_RenderTextureInstanceID;
        internal IntPtr m_BufferPtr;

        internal RenderBufferLoadAction  loadAction  { get { return GetLoadAction();  } set { SetLoadAction(value); } }
        internal RenderBufferStoreAction storeAction { get { return GetStoreAction(); } set { SetStoreAction(value); } }
    }

    public struct RenderTargetSetup
    {
        public RenderBuffer[]   color;
        public RenderBuffer     depth;

        public int              mipLevel;
        public CubemapFace      cubemapFace;
        public int              depthSlice;

        public Rendering.RenderBufferLoadAction[]   colorLoad;
        public Rendering.RenderBufferStoreAction[]  colorStore;

        public Rendering.RenderBufferLoadAction     depthLoad;
        public Rendering.RenderBufferStoreAction    depthStore;


        public RenderTargetSetup(
            RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face,
            Rendering.RenderBufferLoadAction[] colorLoad, Rendering.RenderBufferStoreAction[] colorStore,
            Rendering.RenderBufferLoadAction depthLoad, Rendering.RenderBufferStoreAction depthStore
        )
        {
            this.color          = color;
            this.depth          = depth;

            this.mipLevel       = mip;
            this.cubemapFace    = face;
            this.depthSlice     = 0;

            this.colorLoad      = colorLoad;
            this.colorStore     = colorStore;

            this.depthLoad      = depthLoad;
            this.depthStore     = depthStore;
        }

        internal static Rendering.RenderBufferLoadAction[] LoadActions(RenderBuffer[] buf)
        {
            // preserve old discard behaviour: render surface flags are applied only on first activation
            // this will be used only in ctor without load/store actions specified
            Rendering.RenderBufferLoadAction[] ret = new Rendering.RenderBufferLoadAction[buf.Length];
            for (int i = 0; i < buf.Length; ++i)
            {
                ret[i] = buf[i].loadAction;
                buf[i].loadAction = Rendering.RenderBufferLoadAction.Load;
            }
            return ret;
        }

        internal static Rendering.RenderBufferStoreAction[] StoreActions(RenderBuffer[] buf)
        {
            // preserve old discard behaviour: render surface flags are applied only on first activation
            // this will be used only in ctor without load/store actions specified
            Rendering.RenderBufferStoreAction[] ret = new Rendering.RenderBufferStoreAction[buf.Length];
            for (int i = 0; i < buf.Length; ++i)
            {
                ret[i] = buf[i].storeAction;
                buf[i].storeAction = Rendering.RenderBufferStoreAction.Store;
            }
            return ret;
        }

        // TODO: when we enable default arguments support these can be combined into one method
        public RenderTargetSetup(RenderBuffer color, RenderBuffer depth)
            : this(new RenderBuffer[] { color }, depth)
        {
        }

        public RenderTargetSetup(RenderBuffer color, RenderBuffer depth, int mipLevel)
            : this(new RenderBuffer[] { color }, depth, mipLevel)
        {
        }

        public RenderTargetSetup(RenderBuffer color, RenderBuffer depth, int mipLevel, CubemapFace face)
            : this(new RenderBuffer[] { color }, depth, mipLevel, face)
        {
        }

        public RenderTargetSetup(RenderBuffer color, RenderBuffer depth, int mipLevel, CubemapFace face, int depthSlice)
            : this(new RenderBuffer[] { color }, depth, mipLevel, face)
        {
            this.depthSlice = depthSlice;
        }

        // TODO: when we enable default arguments support these can be combined into one method
        public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth)
            : this(color, depth, 0, CubemapFace.Unknown)
        {
        }

        public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth, int mipLevel)
            : this(color, depth, mipLevel, CubemapFace.Unknown)
        {
        }

        public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face)
            : this(color, depth, mip, face, LoadActions(color), StoreActions(color), depth.loadAction, depth.storeAction)
        {
        }
    }

    public struct RenderParams
    {
        public RenderParams(Material mat)
        {
            layer = 0;
            renderingLayerMask = RenderingLayerMask.defaultRenderingLayerMask;
            rendererPriority = 0;
            worldBounds = new Bounds(Vector3.zero, Vector3.zero);
            camera = null;
            motionVectorMode = MotionVectorGenerationMode.Camera;
            reflectionProbeUsage = ReflectionProbeUsage.Off;
            material = mat;
            matProps = null;
            shadowCastingMode = ShadowCastingMode.Off;
            receiveShadows = false;
            lightProbeUsage = LightProbeUsage.Off;
            lightProbeProxyVolume = null;
            overrideSceneCullingMask = false;
            sceneCullingMask = 0;
            instanceID = 0;
            forceMeshLod = -1;
            meshLodSelectionBias = 0.0f;
        }

        public int layer {get; set;}
        public uint renderingLayerMask {get; set;}
        public int rendererPriority {get; set;}
        public int instanceID {get; set;}
        public Bounds worldBounds {get; set;}
        public Camera camera {get; set;}
        public MotionVectorGenerationMode motionVectorMode {get; set;}
        public ReflectionProbeUsage reflectionProbeUsage {get; set;}

        public Material material {get; set;}
        public MaterialPropertyBlock matProps {get; set;}

        public ShadowCastingMode shadowCastingMode {get; set;}
        public bool receiveShadows {get; set;}

        public LightProbeUsage lightProbeUsage {get; set;}
        public LightProbeProxyVolume lightProbeProxyVolume {get; set;}

        public bool overrideSceneCullingMask { get; set; }
        public ulong sceneCullingMask { get; set; }

        public int forceMeshLod { get; set; }
        public float meshLodSelectionBias { get; set; }
    }

    internal readonly struct RenderInstancedDataLayout
    {
        public RenderInstancedDataLayout(System.Type t)
        {
            size = Marshal.SizeOf(t);
            offsetObjectToWorld = t == typeof(Matrix4x4) ? 0 : Marshal.OffsetOf(t, "objectToWorld").ToInt32();

            // fill optional data members
            try {offsetPrevObjectToWorld = Marshal.OffsetOf(t, "prevObjectToWorld").ToInt32();} catch (ArgumentException) {offsetPrevObjectToWorld = -1;}
            try {offsetRenderingLayerMask = Marshal.OffsetOf(t, "renderingLayerMask").ToInt32();} catch (ArgumentException) {offsetRenderingLayerMask = -1;}
        }

        public int size {get;}
        public int offsetObjectToWorld {get;}
        public int offsetPrevObjectToWorld {get;}
        public int offsetRenderingLayerMask {get;}
    }
}


//
// Graphics.SetRenderTarget
//


namespace UnityEngine
{
    public partial class Graphics
    {
        internal static void CheckLoadActionValid(Rendering.RenderBufferLoadAction load, string bufferType)
        {
            if (load != Rendering.RenderBufferLoadAction.Load && load != Rendering.RenderBufferLoadAction.DontCare)
                throw new ArgumentException(string.Format("Bad {0} LoadAction provided.", bufferType));
        }

        internal static void CheckStoreActionValid(Rendering.RenderBufferStoreAction store, string bufferType)
        {
            if (store != Rendering.RenderBufferStoreAction.Store && store != Rendering.RenderBufferStoreAction.DontCare)
                throw new ArgumentException(string.Format("Bad {0} StoreAction provided.", bufferType));
        }

        internal static void SetRenderTargetImpl(RenderTargetSetup setup)
        {
            if (setup.color.Length == 0)
                throw new ArgumentException("Invalid color buffer count for SetRenderTarget");
            if (setup.color.Length != setup.colorLoad.Length)
                throw new ArgumentException("Color LoadAction and Buffer arrays have different sizes");
            if (setup.color.Length != setup.colorStore.Length)
                throw new ArgumentException("Color StoreAction and Buffer arrays have different sizes");

            foreach (var load in setup.colorLoad)
                CheckLoadActionValid(load, "Color");
            foreach (var store in setup.colorStore)
                CheckStoreActionValid(store, "Color");

            CheckLoadActionValid(setup.depthLoad, "Depth");
            CheckStoreActionValid(setup.depthStore, "Depth");

            if ((int)setup.cubemapFace < (int)CubemapFace.Unknown || (int)setup.cubemapFace > (int)CubemapFace.NegativeZ)
                throw new ArgumentException("Bad CubemapFace provided");

            Internal_SetMRTFullSetup(
                setup.color, setup.depth, setup.mipLevel, setup.cubemapFace, setup.depthSlice,
                setup.colorLoad, setup.colorStore, setup.depthLoad, setup.depthStore
            );
        }

        internal static void SetRenderTargetImpl(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel, CubemapFace face, int depthSlice)
        {
            Internal_SetRTSimple(colorBuffer, depthBuffer, mipLevel, face, depthSlice);
        }

        internal static void SetRenderTargetImpl(RenderTexture rt, int mipLevel, CubemapFace face, int depthSlice)
        {
            if (rt) SetRenderTargetImpl(rt.colorBuffer, rt.depthBuffer, mipLevel, face, depthSlice);
            else    Internal_SetNullRT();
        }

        internal static void SetRenderTargetImpl(GraphicsTexture rt, int mipLevel, CubemapFace face, int depthSlice)
        {
            if (rt != null) Internal_SetGfxRT(rt, mipLevel, face, depthSlice);
            else    Internal_SetNullRT();
        }

        internal static void SetRenderTargetImpl(RenderBuffer[] colorBuffers, RenderBuffer depthBuffer, int mipLevel, CubemapFace face, int depthSlice)
        {
            RenderBuffer depth = depthBuffer;
            Internal_SetMRTSimple(colorBuffers, depth, mipLevel, face, depthSlice);
        }

        public static void SetRenderTarget(RenderTexture rt, [uei.DefaultValue("0")] int mipLevel, [uei.DefaultValue("CubemapFace.Unknown")] CubemapFace face, [uei.DefaultValue("0")] int depthSlice)
        {
            SetRenderTargetImpl(rt, mipLevel, face, depthSlice);
        }

        public static void SetRenderTarget(GraphicsTexture rt, [uei.DefaultValue("0")] int mipLevel, [uei.DefaultValue("CubemapFace.Unknown")] CubemapFace face, [uei.DefaultValue("0")] int depthSlice)
        {
            SetRenderTargetImpl(rt, mipLevel, face, depthSlice);
        }

        public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, [uei.DefaultValue("0")] int mipLevel, [uei.DefaultValue("CubemapFace.Unknown")] CubemapFace face, [uei.DefaultValue("0")] int depthSlice)
        {
            SetRenderTargetImpl(colorBuffer, depthBuffer, mipLevel, face, depthSlice);
        }

        public static void SetRenderTarget(RenderBuffer[] colorBuffers, RenderBuffer depthBuffer)
        {
            SetRenderTargetImpl(colorBuffers, depthBuffer, 0, CubemapFace.Unknown, 0);
        }

        public static void SetRenderTarget(RenderTargetSetup setup)
        {
            SetRenderTargetImpl(setup);
        }
    }

    public partial class Graphics
    {
        public static RenderBuffer activeColorBuffer { get { return GetActiveColorBuffer(); } }
        public static RenderBuffer activeDepthBuffer { get { return GetActiveDepthBuffer(); } }

        public static void SetRandomWriteTarget(int index, RenderTexture uav)
        {
            if (index < 0 || index >= SystemInfo.supportedRandomWriteTargetCount)
                throw new ArgumentOutOfRangeException("index", string.Format("must be non-negative less than {0}.", SystemInfo.supportedRandomWriteTargetCount));

            Internal_SetRandomWriteTargetRT(index, uav);
        }

        public static void SetRandomWriteTarget(int index, ComputeBuffer uav, [uei.DefaultValue("false")] bool preserveCounterValue)
        {
            if (uav == null) throw new ArgumentNullException("uav");
            if (uav.m_Ptr == IntPtr.Zero) throw new System.ObjectDisposedException("uav");
            if (index < 0 || index >= SystemInfo.supportedRandomWriteTargetCount)
                throw new ArgumentOutOfRangeException("index", string.Format("must be non-negative less than {0}.", SystemInfo.supportedRandomWriteTargetCount));

            Internal_SetRandomWriteTargetBuffer(index, uav, preserveCounterValue);
        }

        public static void SetRandomWriteTarget(int index, GraphicsBuffer uav, [uei.DefaultValue("false")] bool preserveCounterValue)
        {
            if (uav == null) throw new ArgumentNullException("uav");
            if (uav.m_Ptr == IntPtr.Zero) throw new System.ObjectDisposedException("uav");
            if (index < 0 || index >= SystemInfo.supportedRandomWriteTargetCount)
                throw new ArgumentOutOfRangeException("index", string.Format("must be non-negative less than {0}.", SystemInfo.supportedRandomWriteTargetCount));

            Internal_SetRandomWriteTargetGraphicsBuffer(index, uav, preserveCounterValue);
        }

        public static void CopyTexture(Texture src, Texture dst)
        {
            CopyTexture_Full(src, dst);
        }

        public static void CopyTexture(Texture src, int srcElement, Texture dst, int dstElement)
        {
            CopyTexture_Slice_AllMips(src, srcElement, dst, dstElement);
        }

        public static void CopyTexture(Texture src, int srcElement, int srcMip, Texture dst, int dstElement, int dstMip)
        {
            CopyTexture_Slice(src, srcElement, srcMip, dst, dstElement, dstMip);
        }

        public static void CopyTexture(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, Texture dst, int dstElement, int dstMip, int dstX, int dstY)
        {
            CopyTexture_Region(src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dst, dstElement, dstMip, dstX, dstY);
        }

        public static void CopyTexture(GraphicsTexture src, GraphicsTexture dst)
        {
            CopyTexture_Full_Gfx(src, dst);
        }

        public static void CopyTexture(GraphicsTexture src, int srcElement, GraphicsTexture dst, int dstElement)
        {
            CopyTexture_Slice_AllMips_Gfx(src, srcElement, dst, dstElement);
        }

        public static void CopyTexture(GraphicsTexture src, int srcElement, int srcMip, GraphicsTexture dst, int dstElement, int dstMip)
        {
            CopyTexture_Slice_Gfx(src, srcElement, srcMip, dst, dstElement, dstMip);
        }

        public static void CopyTexture(GraphicsTexture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsTexture dst, int dstElement, int dstMip, int dstX, int dstY)
        {
            CopyTexture_Region_Gfx(src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dst, dstElement, dstMip, dstX, dstY);
        }

        public static bool ConvertTexture(Texture src, Texture dst)
        {
            return ConvertTexture_Full(src, dst);
        }

        public static bool ConvertTexture(Texture src, int srcElement, Texture dst, int dstElement)
        {
            return ConvertTexture_Slice(src, srcElement, dst, dstElement);
        }

        public static bool ConvertTexture(GraphicsTexture src, GraphicsTexture dst)
        {
            return ConvertTexture_Full_Gfx(src, dst);
        }

        public static bool ConvertTexture(GraphicsTexture src, int srcElement, GraphicsTexture dst, int dstElement)
        {
            return ConvertTexture_Slice_Gfx(src, srcElement, dst, dstElement);
        }

        public static GraphicsFence CreateAsyncGraphicsFence([uei.DefaultValue("SynchronisationStage.PixelProcessing")] SynchronisationStage stage)
        {
            return CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, GraphicsFence.TranslateSynchronizationStageToFlags(stage));
        }

        public static GraphicsFence CreateAsyncGraphicsFence()
        {
            return CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, SynchronisationStageFlags.PixelProcessing);
        }

        public static GraphicsFence CreateGraphicsFence(GraphicsFenceType fenceType, [uei.DefaultValue("SynchronisationStage.PixelProcessing")] SynchronisationStageFlags stage)
        {
            GraphicsFence newFence = new GraphicsFence();
            newFence.m_FenceType = fenceType;
            newFence.m_Ptr = CreateGPUFenceImpl(fenceType, stage);
            newFence.InitPostAllocation();
            newFence.Validate();
            return newFence;
        }

        public static void WaitOnAsyncGraphicsFence(GraphicsFence fence)
        {
            WaitOnAsyncGraphicsFence(fence, SynchronisationStage.PixelProcessing);
        }

        public static void WaitOnAsyncGraphicsFence(GraphicsFence fence, [uei.DefaultValue("SynchronisationStage.PixelProcessing")] SynchronisationStage stage)
        {
            if (fence.m_FenceType != GraphicsFenceType.AsyncQueueSynchronisation)
                throw new ArgumentException("Graphics.WaitOnGraphicsFence can only be called with fences created with GraphicsFenceType.AsyncQueueSynchronization.");

            fence.Validate();

            //Don't wait on a fence that's already known to have passed
            if (fence.IsFencePending())
                WaitOnGPUFenceImpl(fence.m_Ptr, GraphicsFence.TranslateSynchronizationStageToFlags(stage));
        }

        internal static void ValidateCopyBuffer(GraphicsBuffer source, GraphicsBuffer dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            var sourceSize = (long)source.count * source.stride;
            var destSize = (long)dest.count * dest.stride;
            if (sourceSize != destSize)
                throw new ArgumentException($"CopyBuffer source and destination buffers must be the same size, source was {sourceSize} bytes, dest was {destSize} bytes");
            if ((source.target & GraphicsBuffer.Target.CopySource) == 0)
                throw new ArgumentException($"CopyBuffer source must have {nameof(GraphicsBuffer.Target.CopySource)} target", nameof(source));
            if ((dest.target & GraphicsBuffer.Target.CopyDestination) == 0)
                throw new ArgumentException($"CopyBuffer destination must have {nameof(GraphicsBuffer.Target.CopyDestination)} target", nameof(dest));
        }

        public static void CopyBuffer(GraphicsBuffer source, GraphicsBuffer dest)
        {
            ValidateCopyBuffer(source, dest);
            CopyBufferImpl(source, dest);
        }
    }
}


//
// Graphics.Draw*
//


namespace UnityEngine
{
    [VisibleToOtherModules("UnityEngine.IMGUIModule")]
    internal struct Internal_DrawTextureArguments
    {
        public Rect screenRect, sourceRect;
        public int leftBorder, rightBorder, topBorder, bottomBorder;
        public Color leftBorderColor, rightBorderColor, topBorderColor, bottomBorderColor;
        public Color color;
        public Vector4 borderWidths;
        public Vector4 cornerRadiuses;
        public bool smoothCorners;
        public int pass;
        public Texture texture;
        public Material mat;
    }


    public partial class Graphics
    {
        private static void DrawTextureImpl(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, Material mat, int pass)
        {
            Internal_DrawTextureArguments args = new Internal_DrawTextureArguments();
            args.screenRect = screenRect; args.sourceRect = sourceRect;
            args.leftBorder = leftBorder; args.rightBorder = rightBorder; args.topBorder = topBorder; args.bottomBorder = bottomBorder;
            args.color = color;
            args.leftBorderColor = Color.black;
            args.topBorderColor = Color.black;
            args.rightBorderColor = Color.black;
            args.bottomBorderColor = Color.black;
            args.pass = pass;
            args.texture = texture;
            args.smoothCorners = true;
            args.mat = mat;
            Internal_DrawTexture(ref args);
        }

        public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, [uei.DefaultValue("null")] Material mat, [uei.DefaultValue("-1")] int pass)
        {
            DrawTextureImpl(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat, pass);
        }

        public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [uei.DefaultValue("null")] Material mat, [uei.DefaultValue("-1")] int pass)
        {
            Color32 color = new Color32(128, 128, 128, 128);
            DrawTextureImpl(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat, pass);
        }

        public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [uei.DefaultValue("null")] Material mat, [uei.DefaultValue("-1")] int pass)
        {
            DrawTexture(screenRect, texture, new Rect(0, 0, 1, 1), leftBorder, rightBorder, topBorder, bottomBorder, mat, pass);
        }

        public static void DrawTexture(Rect screenRect, Texture texture, [uei.DefaultValue("null")] Material mat, [uei.DefaultValue("-1")] int pass)
        {
            DrawTexture(screenRect, texture, 0, 0, 0, 0, mat, pass);
        }

        public unsafe static void RenderMesh(in RenderParams rparams, Mesh mesh, int submeshIndex, Matrix4x4 objectToWorld, [uei.DefaultValue("null")] Matrix4x4? prevObjectToWorld = null)
        {
            if (prevObjectToWorld.HasValue)
            {
                Matrix4x4 temp = prevObjectToWorld.Value;
                Internal_RenderMesh(rparams, mesh, submeshIndex, objectToWorld, &temp);
            }
            else
                Internal_RenderMesh(rparams, mesh, submeshIndex, objectToWorld, null);
        }

        internal static Dictionary<int, RenderInstancedDataLayout> s_RenderInstancedDataLayouts = new Dictionary<int, RenderInstancedDataLayout>();
        private static RenderInstancedDataLayout GetCachedRenderInstancedDataLayout(Type type)
        {
            int typeHashCode = type.GetHashCode();
            RenderInstancedDataLayout layout;
            if(!s_RenderInstancedDataLayouts.TryGetValue(typeHashCode, out layout))
            {
                layout = new RenderInstancedDataLayout(type);
                s_RenderInstancedDataLayouts.Add(typeHashCode, layout);
            }
            return layout;
        }

        public unsafe static void RenderMeshInstanced<T>(in RenderParams rparams, Mesh mesh, int submeshIndex, T[] instanceData, [uei.DefaultValue("-1")] int instanceCount = -1, [uei.DefaultValue("0")] int startInstance = 0) where T : unmanaged
        {
            if (!SystemInfo.supportsInstancing)
                throw new InvalidOperationException("Instancing is not supported.");
            else if (!rparams.material.enableInstancing)
                throw new InvalidOperationException("Material needs to enable instancing for use with RenderMeshInstanced.");
            else if (instanceData == null)
                throw new ArgumentNullException("instanceData");
            RenderInstancedDataLayout layout = GetCachedRenderInstancedDataLayout(typeof(T));
            uint count = Math.Min((uint)instanceCount, (uint)Math.Max(0, instanceData.Length - startInstance));
            fixed(T *data = instanceData) {Internal_RenderMeshInstanced(rparams, mesh, submeshIndex, (IntPtr)(data + startInstance), layout, count);}
        }

        public unsafe static void RenderMeshInstanced<T>(in RenderParams rparams, Mesh mesh, int submeshIndex, List<T> instanceData, [uei.DefaultValue("-1")] int instanceCount = -1, [uei.DefaultValue("0")] int startInstance = 0) where T : unmanaged
        {
            if (!SystemInfo.supportsInstancing)
                throw new InvalidOperationException("Instancing is not supported.");
            else if (!rparams.material.enableInstancing)
                throw new InvalidOperationException("Material needs to enable instancing for use with RenderMeshInstanced.");
            else if (instanceData == null)
                throw new ArgumentNullException("instanceData");
            RenderInstancedDataLayout layout = GetCachedRenderInstancedDataLayout(typeof(T));
            uint count = Math.Min((uint)instanceCount, (uint)Math.Max(0, instanceData.Count - startInstance));
            fixed(T *data = NoAllocHelpers.ExtractArrayFromList(instanceData)) {Internal_RenderMeshInstanced(rparams, mesh, submeshIndex, (IntPtr)(data + startInstance), layout, count);}
        }

        public unsafe static void RenderMeshInstanced<T>(RenderParams rparams, Mesh mesh, int submeshIndex, NativeArray<T> instanceData, [uei.DefaultValue("-1")] int instanceCount = -1, [uei.DefaultValue("0")] int startInstance = 0) where T : unmanaged
        {
            if (!SystemInfo.supportsInstancing)
                throw new InvalidOperationException("Instancing is not supported.");
            else if (!rparams.material.enableInstancing)
                throw new InvalidOperationException("Material needs to enable instancing for use with RenderMeshInstanced.");
            else if (instanceData == null)
                throw new ArgumentNullException("instanceData");
            RenderInstancedDataLayout layout = GetCachedRenderInstancedDataLayout(typeof(T));
            uint count = Math.Min((uint)instanceCount, (uint)Math.Max(0, instanceData.Length - startInstance));
            Internal_RenderMeshInstanced(rparams, mesh, submeshIndex, (IntPtr)((T*)instanceData.GetUnsafePtr() + startInstance), layout, count);
        }

        public static void RenderMeshIndirect(in RenderParams rparams, Mesh mesh, GraphicsBuffer argsBuffer, [uei.DefaultValue("1")] int commandCount = 1, [uei.DefaultValue("0")] int startCommand = 0)
        {
            if (!SystemInfo.supportsInstancing)
                throw new InvalidOperationException("Instancing is not supported.");
            if (!SystemInfo.supportsIndirectArgumentsBuffer)
                throw new InvalidOperationException("Indirect argument buffers are not supported.");
            Internal_RenderMeshIndirect(rparams, mesh, argsBuffer, commandCount, startCommand);
        }

        public static void RenderMeshPrimitives(in RenderParams rparams, Mesh mesh, int submeshIndex, [uei.DefaultValue("1")] int instanceCount = 1)
        {
            if (!SystemInfo.supportsInstancing)
                throw new InvalidOperationException("Instancing is not supported.");
            Internal_RenderMeshPrimitives(rparams, mesh, submeshIndex, instanceCount);
        }

        public static void RenderPrimitives(in RenderParams rparams, MeshTopology topology, int vertexCount, [uei.DefaultValue("1")] int instanceCount = 1)
        {
            if (!SystemInfo.supportsInstancing)
                throw new InvalidOperationException("Instancing is not supported.");
            Internal_RenderPrimitives(rparams, topology, vertexCount, instanceCount);
        }

        public static void RenderPrimitivesIndexed(in RenderParams rparams, MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, [uei.DefaultValue("0")] int startIndex = 0, [uei.DefaultValue("1")] int instanceCount = 1)
        {
            if (!SystemInfo.supportsInstancing)
                throw new InvalidOperationException("Instancing is not supported.");
            Internal_RenderPrimitivesIndexed(rparams, topology, indexBuffer, indexCount, startIndex, instanceCount);
        }

        public static void RenderPrimitivesIndirect(in RenderParams rparams, MeshTopology topology, GraphicsBuffer argsBuffer, [uei.DefaultValue("1")] int commandCount = 1, [uei.DefaultValue("0")] int startCommand = 0)
        {
            if (!SystemInfo.supportsInstancing)
                throw new InvalidOperationException("Instancing is not supported.");
            if (!SystemInfo.supportsIndirectArgumentsBuffer)
                throw new InvalidOperationException("Indirect argument buffers are not supported.");
            Internal_RenderPrimitivesIndirect(rparams, topology, argsBuffer, commandCount, startCommand);
        }

        public static void RenderPrimitivesIndexedIndirect(in RenderParams rparams, MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer commandBuffer, [uei.DefaultValue("1")] int commandCount = 1, [uei.DefaultValue("0")] int startCommand = 0)
        {
            if (!SystemInfo.supportsInstancing)
                throw new InvalidOperationException("Instancing is not supported.");
            if (!SystemInfo.supportsIndirectArgumentsBuffer)
                throw new InvalidOperationException("Indirect argument buffers are not supported.");
            Internal_RenderPrimitivesIndexedIndirect(rparams, topology, indexBuffer, commandBuffer, commandCount, startCommand);
        }

        public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation, int materialIndex)
        {
            if (mesh == null)
                throw new ArgumentNullException("mesh");
            Internal_DrawMeshNow1(mesh, materialIndex, position, rotation);
        }

        public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix, int materialIndex)
        {
            if (mesh == null)
                throw new ArgumentNullException("mesh");
            Internal_DrawMeshNow2(mesh, materialIndex, matrix);
        }

        public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation) { DrawMeshNow(mesh, position, rotation, -1); }
        public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix) { DrawMeshNow(mesh, matrix, -1); }


        public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, [uei.DefaultValue("null")] Camera camera, [uei.DefaultValue("0")] int submeshIndex, [uei.DefaultValue("null")] MaterialPropertyBlock properties, [uei.DefaultValue("true")] bool castShadows, [uei.DefaultValue("true")] bool receiveShadows, [uei.DefaultValue("true")] bool useLightProbes)
        {
            DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
        }

        public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, [uei.DefaultValue("true")] bool receiveShadows, [uei.DefaultValue("null")] Transform probeAnchor, [uei.DefaultValue("true")] bool useLightProbes)
        {
            DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
        }

        public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, [uei.DefaultValue("null")] Camera camera, [uei.DefaultValue("0")] int submeshIndex, [uei.DefaultValue("null")] MaterialPropertyBlock properties, [uei.DefaultValue("true")] bool castShadows, [uei.DefaultValue("true")] bool receiveShadows, [uei.DefaultValue("true")] bool useLightProbes)
        {
            DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
        }

        public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage, [uei.DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
        {
            if (lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null)
                throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
            Internal_DrawMesh(mesh, submeshIndex, matrix, material, layer, camera, properties, castShadows, receiveShadows, probeAnchor, lightProbeUsage, lightProbeProxyVolume);
        }

        public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, [uei.DefaultValue("matrices.Length")] int count, [uei.DefaultValue("null")] MaterialPropertyBlock properties, [uei.DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [uei.DefaultValue("true")] bool receiveShadows, [uei.DefaultValue("0")] int layer, [uei.DefaultValue("null")] Camera camera, [uei.DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [uei.DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
        {
            if (!SystemInfo.supportsInstancing)
                throw new InvalidOperationException("Instancing is not supported.");
            else if (mesh == null)
                throw new ArgumentNullException("mesh");
            else if (submeshIndex < 0 || submeshIndex >= mesh.subMeshCount)
                throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
            else if (material == null)
                throw new ArgumentNullException("material");
            else if (!material.enableInstancing)
                throw new InvalidOperationException("Material needs to enable instancing for use with DrawMeshInstanced.");
            else if (matrices == null)
                throw new ArgumentNullException("matrices");
            else if (count < 0 || count > Mathf.Min(kMaxDrawMeshInstanceCount, matrices.Length))
                throw new ArgumentOutOfRangeException("count", String.Format("Count must be in the range of 0 to {0}.", Mathf.Min(kMaxDrawMeshInstanceCount, matrices.Length)));
            else if (lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null)
                throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");

            if (count > 0)
                Internal_DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
        }

        public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, [uei.DefaultValue("null")] MaterialPropertyBlock properties, [uei.DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [uei.DefaultValue("true")] bool receiveShadows, [uei.DefaultValue("0")] int layer, [uei.DefaultValue("null")] Camera camera, [uei.DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [uei.DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
        {
            if (matrices == null)
                throw new ArgumentNullException("matrices");

            DrawMeshInstanced(mesh, submeshIndex, material, NoAllocHelpers.ExtractArrayFromList(matrices), matrices.Count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
        }

        public static void DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, Bounds bounds, int count, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0, Camera camera = null, LightProbeUsage lightProbeUsage = LightProbeUsage.BlendProbes, LightProbeProxyVolume lightProbeProxyVolume = null)
        {
            if (!SystemInfo.supportsInstancing)
                throw new InvalidOperationException("Instancing is not supported.");
            else if (mesh == null)
                throw new ArgumentNullException("mesh");
            else if (submeshIndex < 0 || submeshIndex >= mesh.subMeshCount)
                throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
            else if (material == null)
                throw new ArgumentNullException("material");
            else if (count <= 0)
                throw new ArgumentOutOfRangeException("count");
            else if (lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null)
                throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");

            if (count > 0)
                Internal_DrawMeshInstancedProcedural(mesh, submeshIndex, material, bounds, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
        }

        public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, [uei.DefaultValue("0")] int argsOffset, [uei.DefaultValue("null")] MaterialPropertyBlock properties, [uei.DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [uei.DefaultValue("true")] bool receiveShadows, [uei.DefaultValue("0")] int layer, [uei.DefaultValue("null")] Camera camera, [uei.DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [uei.DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
        {
            if (!SystemInfo.supportsInstancing)
                throw new InvalidOperationException("Instancing is not supported.");
            if (!SystemInfo.supportsIndirectArgumentsBuffer)
                throw new InvalidOperationException("Indirect argument buffers are not supported.");
            else if (mesh == null)
                throw new ArgumentNullException("mesh");
            else if (submeshIndex < 0 || submeshIndex >= mesh.subMeshCount)
                throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
            else if (material == null)
                throw new ArgumentNullException("material");
            else if (bufferWithArgs == null)
                throw new ArgumentNullException("bufferWithArgs");
            if (lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null)
                throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");

            Internal_DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
        }

        public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, GraphicsBuffer bufferWithArgs, [uei.DefaultValue("0")] int argsOffset, [uei.DefaultValue("null")] MaterialPropertyBlock properties, [uei.DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [uei.DefaultValue("true")] bool receiveShadows, [uei.DefaultValue("0")] int layer, [uei.DefaultValue("null")] Camera camera, [uei.DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [uei.DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
        {
            if (!SystemInfo.supportsInstancing)
                throw new InvalidOperationException("Instancing is not supported.");
            if (!SystemInfo.supportsIndirectArgumentsBuffer)
                throw new InvalidOperationException("Indirect argument buffers are not supported.");
            else if (mesh == null)
                throw new ArgumentNullException("mesh");
            else if (submeshIndex < 0 || submeshIndex >= mesh.subMeshCount)
                throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
            else if (material == null)
                throw new ArgumentNullException("material");
            else if (bufferWithArgs == null)
                throw new ArgumentNullException("bufferWithArgs");
            if (lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null)
                throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");

            Internal_DrawMeshInstancedIndirectGraphicsBuffer(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
        }

        public static void DrawProceduralNow(MeshTopology topology, int vertexCount, int instanceCount = 1)
        {
            Internal_DrawProceduralNow(topology, vertexCount, instanceCount);
        }

        public static void DrawProceduralNow(MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount = 1)
        {
            if (indexBuffer == null)
                throw new ArgumentNullException("indexBuffer");
            Internal_DrawProceduralIndexedNow(topology, indexBuffer, indexCount, instanceCount);
        }

        public static void DrawProceduralIndirectNow(MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset = 0)
        {
            if (!SystemInfo.supportsIndirectArgumentsBuffer)
                throw new InvalidOperationException("Indirect argument buffers are not supported.");
            if (bufferWithArgs == null)
                throw new ArgumentNullException("bufferWithArgs");

            Internal_DrawProceduralIndirectNow(topology, bufferWithArgs, argsOffset);
        }

        public static void DrawProceduralIndirectNow(MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset = 0)
        {
            if (!SystemInfo.supportsIndirectArgumentsBuffer)
                throw new InvalidOperationException("Indirect argument buffers are not supported.");
            if (indexBuffer == null)
                throw new ArgumentNullException("indexBuffer");
            if (bufferWithArgs == null)
                throw new ArgumentNullException("bufferWithArgs");

            Internal_DrawProceduralIndexedIndirectNow(topology, indexBuffer, bufferWithArgs, argsOffset);
        }

        public static void DrawProceduralIndirectNow(MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset = 0)
        {
            if (!SystemInfo.supportsIndirectArgumentsBuffer)
                throw new InvalidOperationException("Indirect argument buffers are not supported.");
            if (bufferWithArgs == null)
                throw new ArgumentNullException("bufferWithArgs");

            Internal_DrawProceduralIndirectNowGraphicsBuffer(topology, bufferWithArgs, argsOffset);
        }

        public static void DrawProceduralIndirectNow(MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer bufferWithArgs, int argsOffset = 0)
        {
            if (!SystemInfo.supportsIndirectArgumentsBuffer)
                throw new InvalidOperationException("Indirect argument buffers are not supported.");
            if (indexBuffer == null)
                throw new ArgumentNullException("indexBuffer");
            if (bufferWithArgs == null)
                throw new ArgumentNullException("bufferWithArgs");

            Internal_DrawProceduralIndexedIndirectNowGraphicsBuffer(topology, indexBuffer, bufferWithArgs, argsOffset);
        }

        public static void DrawProcedural(Material material, Bounds bounds, MeshTopology topology, int vertexCount, int instanceCount = 1, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
        {
            Internal_DrawProcedural(material, bounds, topology, vertexCount, instanceCount, camera, properties, castShadows, receiveShadows, layer);
        }

        public static void DrawProcedural(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount = 1, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
        {
            if (indexBuffer == null)
                throw new ArgumentNullException("indexBuffer");
            Internal_DrawProceduralIndexed(material, bounds, topology, indexBuffer, indexCount, instanceCount, camera, properties, castShadows, receiveShadows, layer);
        }

        public static void DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset = 0, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
        {
            if (!SystemInfo.supportsIndirectArgumentsBuffer)
                throw new InvalidOperationException("Indirect argument buffers are not supported.");
            if (bufferWithArgs == null)
                throw new ArgumentNullException("bufferWithArgs");

            Internal_DrawProceduralIndirect(material, bounds, topology, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
        }

        public static void DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset = 0, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
        {
            if (!SystemInfo.supportsIndirectArgumentsBuffer)
                throw new InvalidOperationException("Indirect argument buffers are not supported.");
            if (bufferWithArgs == null)
                throw new ArgumentNullException("bufferWithArgs");

            Internal_DrawProceduralIndirectGraphicsBuffer(material, bounds, topology, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
        }

        public static void DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset = 0, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
        {
            if (!SystemInfo.supportsIndirectArgumentsBuffer)
                throw new InvalidOperationException("Indirect argument buffers are not supported.");
            if (indexBuffer == null)
                throw new ArgumentNullException("indexBuffer");
            if (bufferWithArgs == null)
                throw new ArgumentNullException("bufferWithArgs");

            Internal_DrawProceduralIndexedIndirect(material, bounds, topology, indexBuffer, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
        }

        public static void DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer bufferWithArgs, int argsOffset = 0, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
        {
            if (!SystemInfo.supportsIndirectArgumentsBuffer)
                throw new InvalidOperationException("Indirect argument buffers are not supported.");
            if (indexBuffer == null)
                throw new ArgumentNullException("indexBuffer");
            if (bufferWithArgs == null)
                throw new ArgumentNullException("bufferWithArgs");

            Internal_DrawProceduralIndexedIndirectGraphicsBuffer(material, bounds, topology, indexBuffer, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
        }
    }
}


//
// Graphics.Blit*
//


namespace UnityEngine
{
    public partial class Graphics
    {
        public static void Blit(Texture source, RenderTexture dest)
        {
            Blit2(source, dest);
        }

        public static void Blit(Texture source, RenderTexture dest, int sourceDepthSlice, int destDepthSlice)
        {
            Blit3(source, dest, sourceDepthSlice, destDepthSlice);
        }

        public static void Blit(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset)
        {
            Blit4(source, dest, scale, offset);
        }

        public static void Blit(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
        {
            Blit5(source, dest, scale, offset, sourceDepthSlice, destDepthSlice);
        }

        public static void Blit(Texture source, RenderTexture dest, Material mat, [uei.DefaultValue("-1")] int pass)
        {
            Internal_BlitMaterial5(source, dest, mat, pass, true);
        }

        public static void Blit(Texture source, RenderTexture dest, Material mat, int pass, int destDepthSlice)
        {
            Internal_BlitMaterial6(source, dest, mat, pass, true, destDepthSlice);
        }

        public static void Blit(Texture source, RenderTexture dest, Material mat)
        {
            Blit(source, dest, mat, -1);
        }

        public static void Blit(Texture source, Material mat, [uei.DefaultValue("-1")] int pass)
        {
            Internal_BlitMaterial5(source, null, mat, pass, false);
        }

        public static void Blit(Texture source, Material mat, int pass, int destDepthSlice)
        {
            Internal_BlitMaterial6(source, null, mat, pass, false, destDepthSlice);
        }

        public static void Blit(Texture source, Material mat)
        {
            Blit(source, mat, -1);
        }

        public static void BlitMultiTap(Texture source, RenderTexture dest, Material mat, params Vector2[] offsets)
        {
            // in case params were not passed, we will end up with empty array (not null) but our cpp code is not ready for that.
            // do explicit argument exception instead of potential nullref coming from native side
            if (offsets.Length == 0)
                throw new ArgumentException("empty offsets list passed.", "offsets");
            Internal_BlitMultiTap4(source, dest, mat, offsets);
        }

        public static void BlitMultiTap(Texture source, RenderTexture dest, Material mat, int destDepthSlice, params Vector2[] offsets)
        {
            // in case params were not passed, we will end up with empty array (not null) but our cpp code is not ready for that.
            // do explicit argument exception instead of potential nullref coming from native side
            if (offsets.Length == 0)
                throw new ArgumentException("empty offsets list passed.", "offsets");
            Internal_BlitMultiTap5(source, dest, mat, offsets, destDepthSlice);
        }

        //
        // Blit to GraphicsTexture
        //

        public static void Blit(Texture source, GraphicsTexture dest)
        {
            BlitGfx2(source, dest);
        }

        public static void Blit(Texture source, GraphicsTexture dest, int sourceDepthSlice, int destDepthSlice)
        {
            BlitGfx3(source, dest, sourceDepthSlice, destDepthSlice);
        }

        public static void Blit(Texture source, GraphicsTexture dest, Vector2 scale, Vector2 offset)
        {
            BlitGfx4(source, dest, scale, offset);
        }

        public static void Blit(Texture source, GraphicsTexture dest, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
        {
            BlitGfx5(source, dest, scale, offset, sourceDepthSlice, destDepthSlice);
        }

        public static void Blit(Texture source, GraphicsTexture dest, Material mat, [uei.DefaultValue("-1")] int pass)
        {
            Internal_BlitMaterialGfx5(source, dest, mat, pass, true);
        }

        public static void Blit(Texture source, GraphicsTexture dest, Material mat, int pass, int destDepthSlice)
        {
            Internal_BlitMaterialGfx6(source, dest, mat, pass, true, destDepthSlice);
        }

        public static void Blit(Texture source, GraphicsTexture dest, Material mat)
        {
            Blit(source, dest, mat, -1);
        }

        public static void BlitMultiTap(Texture source, GraphicsTexture dest, Material mat, params Vector2[] offsets)
        {
            // in case params were not passed, we will end up with empty array (not null) but our cpp code is not ready for that.
            // do explicit argument exception instead of potential nullref coming from native side
            if (offsets.Length == 0)
                throw new ArgumentException("empty offsets list passed.", "offsets");
            Internal_BlitMultiTapGfx4(source, dest, mat, offsets);
        }

        public static void BlitMultiTap(Texture source, GraphicsTexture dest, Material mat, int destDepthSlice, params Vector2[] offsets)
        {
            // in case params were not passed, we will end up with empty array (not null) but our cpp code is not ready for that.
            // do explicit argument exception instead of potential nullref coming from native side
            if (offsets.Length == 0)
                throw new ArgumentException("empty offsets list passed.", "offsets");
            Internal_BlitMultiTapGfx5(source, dest, mat, offsets, destDepthSlice);
        }
    }
}


//
// QualitySettings
//


namespace UnityEngine
{
    public sealed partial class QualitySettings
    {
        /// <summary>
        /// Callback raised when the current active quality level is being changed
        /// It passes to the callback the previous quality level and the current quality level
        /// </summary>
        public static event Action<int,int> activeQualityLevelChanged;

        [RequiredByNativeCode]
        internal static void OnActiveQualityLevelChanged(int previousQualityLevel, int currentQualityLevel)
        {
            activeQualityLevelChanged?.Invoke(previousQualityLevel, currentQualityLevel);
        }

        public static void IncreaseLevel([uei.DefaultValue("false")] bool applyExpensiveChanges)
        {
            SetQualityLevel(GetQualityLevel() + 1, applyExpensiveChanges);
        }

        public static void DecreaseLevel([uei.DefaultValue("false")] bool applyExpensiveChanges)
        {
            SetQualityLevel(GetQualityLevel() - 1, applyExpensiveChanges);
        }

        public static void SetQualityLevel(int index) { SetQualityLevel(index, true); }
        public static void IncreaseLevel() { IncreaseLevel(false); }
        public static void DecreaseLevel() { DecreaseLevel(false); }
    }
}

//
// Extensions
//

namespace UnityEngine
{
    public static partial class RendererExtensions
    {
        static public void UpdateGIMaterials(this Renderer renderer) { UpdateGIMaterialsForRenderer(renderer); }
    }
}

//
// Attributes
//

namespace UnityEngine
{
    [UsedByNativeCode]
    public sealed partial class ImageEffectTransformsToLDR : Attribute
    {
    }

    public sealed partial class ImageEffectAllowedInSceneView : Attribute
    {
    }

    [UsedByNativeCode]
    public sealed partial class ImageEffectOpaque : Attribute
    {
    }

    [UsedByNativeCode]
    public sealed partial class ImageEffectAfterScale : Attribute
    {
    }

    [UsedByNativeCode]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed partial class ImageEffectUsesCommandBuffer : Attribute
    {
    }
}
