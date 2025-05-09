// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using Unity.Profiling;
using UnityEngine.Scripting;
using UnityEngine.Rendering;

namespace UnityEngine.UIElements
{
    using UIR;

    /// <summary>
    /// A renderer Component that should be added next to a UIDocument Component to allow
    /// world-space rendering. This Component is added automatically by the UIDocument when
    /// the PanelSettings asset is configured in world-space.
    /// </summary>
    [NativeType(Header = "Modules/UIElements/Core/Native/Renderer/UIRenderer.h")]
    public sealed class UIRenderer : Renderer
    {
        internal volatile List<CommandList>[] commandLists;
        internal volatile bool skipRendering;

        internal extern void AddDrawCallData(int safeFrameIndex, int cmdListIndex, Material mat);
        internal extern void ResetDrawCallData();

        [RequiredByNativeCode]
        static void OnRenderNodeExecute(UIRenderer renderer, int safeFrameIndex, int cmdListIndex)
        {
            if (renderer.skipRendering)
                return;

            var commandLists = renderer.commandLists;
            var cmdList = commandLists != null ? commandLists[safeFrameIndex] : null;
            if (cmdList != null && cmdListIndex < cmdList.Count)
                cmdList[cmdListIndex]?.Execute();
        }
    }
}

namespace UnityEngine.UIElements.UIR
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct GfxUpdateBufferRange
    {
        public UInt32 offsetFromWriteStart;
        public UInt32 size;
        public UIntPtr source;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DrawBufferRange
    {
        public int firstIndex;
        public int indexCount;
        public int minIndexVal;
        public int vertsReferenced;
    }

    [NativeHeader("Modules/UIElements/Core/Native/Renderer/UIRendererUtility.h")]
    [VisibleToOtherModules("Unity.UIElements")]
    internal partial class Utility
    {
        internal enum GPUBufferType { Vertex, Index }
        unsafe public class GPUBuffer<T> : IDisposable where T : struct
        {
            IntPtr buffer;
            int elemCount;
            int elemStride;

            unsafe public GPUBuffer(int elementCount, GPUBufferType type)
            {
                elemCount = elementCount;
                elemStride = UnsafeUtility.SizeOf<T>();
                buffer = AllocateBuffer(elementCount, elemStride, type == GPUBufferType.Vertex);
            }

            public void Dispose()
            {
                FreeBuffer(buffer);
            }

            public void UpdateRanges(NativeSlice<GfxUpdateBufferRange> ranges, int rangesMin, int rangesMax)
            {
                UpdateBufferRanges(buffer, new IntPtr(ranges.GetUnsafePtr()), ranges.Length, rangesMin, rangesMax);
            }

            public int ElementStride { get { return elemStride; } }
            public int Count { get { return elemCount; } }
            internal IntPtr BufferPointer { get { return buffer; } }
        }

        unsafe public static void SetVectorArray<T>(MaterialPropertyBlock props, int name, NativeSlice<T> vector4s) where T : struct
        {
            int vector4Count = (vector4s.Length * vector4s.Stride) / (sizeof(float) * 4);
            SetVectorArray(props, name, new IntPtr(vector4s.GetUnsafePtr()), vector4Count);
        }

        public static event Action<bool> GraphicsResourcesRecreate;
        public static event Action EngineUpdate;
        public static event Action FlushPendingResources;

        [RequiredByNativeCode]
        internal static void RaiseGraphicsResourcesRecreate(bool recreate)
        {
            GraphicsResourcesRecreate?.Invoke(recreate);
        }

        static ProfilerMarker s_MarkerRaiseEngineUpdate = new ProfilerMarker("UIR.RaiseEngineUpdate");

        [RequiredByNativeCode]
        internal static void RaiseEngineUpdate()
        {
            if (EngineUpdate != null)
            {
                s_MarkerRaiseEngineUpdate.Begin();
                EngineUpdate.Invoke();
                s_MarkerRaiseEngineUpdate.End();
            }
        }

        [RequiredByNativeCode]
        internal static void RaiseFlushPendingResources()
        {
            FlushPendingResources?.Invoke();
        }

        [ThreadSafe] extern static IntPtr AllocateBuffer(int elementCount, int elementStride, bool vertexBuffer);
        [ThreadSafe] extern static void FreeBuffer(IntPtr buffer);
        [ThreadSafe] extern static void UpdateBufferRanges(IntPtr buffer, IntPtr ranges, int rangeCount, int writeRangeStart, int writeRangeEnd);
        [ThreadSafe] extern static void SetVectorArray(MaterialPropertyBlock props, int name, IntPtr vector4s, int count);
        [ThreadSafe] public extern static IntPtr GetVertexDeclaration(VertexAttributeDescriptor[] vertexAttributes);

        [ThreadSafe] public extern unsafe static void DrawRanges(IntPtr ib, IntPtr* vertexStreams, int streamCount, IntPtr ranges, int rangeCount, IntPtr vertexDecl);
        [ThreadSafe] public extern static void SetPropertyBlock(MaterialPropertyBlock props);
        [ThreadSafe] public extern static void SetScissorRect(RectInt scissorRect);
        [ThreadSafe] public extern static void DisableScissor();
        [ThreadSafe] public extern static bool IsScissorEnabled();
        [ThreadSafe] public extern static IntPtr CreateStencilState(StencilState stencilState);
        [ThreadSafe] public extern static void SetStencilState(IntPtr stencilState, int stencilRef);
        [ThreadSafe] public extern static bool HasMappedBufferRange();
        [ThreadSafe] public extern static UInt32 InsertCPUFence();
        [ThreadSafe] public extern static bool CPUFencePassed(UInt32 fence);
        [ThreadSafe] public extern static void WaitForCPUFencePassed(UInt32 fence);
        [ThreadSafe] public extern static void SyncRenderThread();
        [ThreadSafe] public extern static RectInt GetActiveViewport();
        [ThreadSafe] public extern static void ProfileDrawChainBegin();
        [ThreadSafe] public extern static void ProfileDrawChainEnd();
        public extern static void NotifyOfUIREvents(bool subscribe);
        [ThreadSafe] public extern static Matrix4x4 GetUnityProjectionMatrix();
        [ThreadSafe] public extern static Matrix4x4 GetDeviceProjectionMatrix();
        [ThreadSafe] public extern static bool DebugIsMainThread(); // For debug code only
    }
}
