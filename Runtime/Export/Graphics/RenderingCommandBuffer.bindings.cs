// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Experimental.Rendering;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Profiling;
using Unity.Profiling;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace UnityEngine.Rendering
{
    [NativeHeader("Runtime/Shaders/ComputeShader.h")]
    [NativeHeader("Runtime/Shaders/RayTracing/RayTracingShader.h")]
    [NativeHeader("Runtime/Export/Graphics/RenderingCommandBuffer.bindings.h")]
    [NativeType("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
    [UsedByNativeCode]
    public partial class CommandBuffer : IDisposable
    {
        public void ConvertTexture(RenderTargetIdentifier src, RenderTargetIdentifier dst)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            ConvertTexture_Internal(src, 0, dst, 0);
        }

        public void ConvertTexture(RenderTargetIdentifier src, int srcElement, RenderTargetIdentifier dst, int dstElement)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            ConvertTexture_Internal(src, srcElement, dst, dstElement);
        }

        [NativeMethod("AddWaitAllAsyncReadbackRequests")]
        public extern void WaitAllAsyncReadbackRequests();

        public void RequestAsyncReadback(ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                Internal_RequestAsyncReadback_1(src, callback);
            }
        }

        public void RequestAsyncReadback(GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                Internal_RequestAsyncReadback_8(src, callback);
            }
        }

        public void RequestAsyncReadback(ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                Internal_RequestAsyncReadback_2(src, size, offset, callback);
            }
        }

        public void RequestAsyncReadback(GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                Internal_RequestAsyncReadback_9(src, size, offset, callback);
            }
        }

        public void RequestAsyncReadback(Texture src, Action<AsyncGPUReadbackRequest> callback)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                Internal_RequestAsyncReadback_3(src, callback);
            }
        }

        public void RequestAsyncReadback(Texture src, int mipIndex, Action<AsyncGPUReadbackRequest> callback)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                Internal_RequestAsyncReadback_4(src, mipIndex, callback);
            }
        }

        public void RequestAsyncReadback(Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                Internal_RequestAsyncReadback_5(src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
            }
        }

        public void RequestAsyncReadback(Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                Internal_RequestAsyncReadback_5(src, mipIndex, dstFormat, callback);
            }
        }

        public void RequestAsyncReadback(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                Internal_RequestAsyncReadback_6(src, mipIndex, x, width, y, height, z, depth, callback);
            }
        }

        public void RequestAsyncReadback(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
            }
        }

        public void RequestAsyncReadback(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, dstFormat, callback);
            }
        }

        public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_1(src, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_2(src, size, offset, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_8(src, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_9(src, size, offset, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_3(src, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_4(src, mipIndex, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_5(src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_5(src, mipIndex, dstFormat, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_6(src, mipIndex, x, width, y, height, z, depth, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, dstFormat, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_1(src, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_2(src, size, offset, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_8(src, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_9(src, size, offset, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_3(src, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_4(src, mipIndex, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_5(src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_5(src, mipIndex, dstFormat, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_6(src, mipIndex, x, width, y, height, z, depth, callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback, &data);
            }
        }

        public void RequestAsyncReadbackIntoNativeSlice<T>(ref NativeSlice<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback) where T : struct
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            unsafe
            {
                var data = AsyncRequestNativeArrayData.CreateAndCheckAccess(output);
                Internal_RequestAsyncReadback_7(src, mipIndex, x, width, y, height, z, depth, dstFormat, callback, &data);
            }
        }

        [NativeMethod("AddRequestAsyncReadback")]
        unsafe extern private void Internal_RequestAsyncReadback_1([NotNull] ComputeBuffer src, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);
        [NativeMethod("AddRequestAsyncReadback")]
        unsafe extern private void Internal_RequestAsyncReadback_2([NotNull] ComputeBuffer src, int size, int offset, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);
        [NativeMethod("AddRequestAsyncReadback")]
        unsafe extern private void Internal_RequestAsyncReadback_3([NotNull] Texture src, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);
        [NativeMethod("AddRequestAsyncReadback")]
        unsafe extern private void Internal_RequestAsyncReadback_4([NotNull] Texture src, int mipIndex, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);
        [NativeMethod("AddRequestAsyncReadback")]
        unsafe extern private void Internal_RequestAsyncReadback_5([NotNull] Texture src, int mipIndex, GraphicsFormat dstFormat, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);
        [NativeMethod("AddRequestAsyncReadback")]
        unsafe extern private void Internal_RequestAsyncReadback_6([NotNull] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);
        [NativeMethod("AddRequestAsyncReadback")]
        unsafe extern private void Internal_RequestAsyncReadback_7([NotNull] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);
        [NativeMethod("AddRequestAsyncReadback")]
        unsafe extern private void Internal_RequestAsyncReadback_8([NotNull] GraphicsBuffer src, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);
        [NativeMethod("AddRequestAsyncReadback")]
        unsafe extern private void Internal_RequestAsyncReadback_9([NotNull] GraphicsBuffer src, int size, int offset, [NotNull] Action<AsyncGPUReadbackRequest> callback, AsyncRequestNativeArrayData* nativeArrayData = null);

        [NativeMethod("AddSetInvertCulling")]
        public extern void SetInvertCulling(bool invertCulling);

        extern void ConvertTexture_Internal(RenderTargetIdentifier src, int srcElement, RenderTargetIdentifier dst, int dstElement);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetSinglePassStereo", HasExplicitThis = true)]
        extern private void Internal_SetSinglePassStereo(SinglePassStereoMode mode);

        [FreeFunction("RenderingCommandBuffer_Bindings::InitBuffer")]
        extern private static IntPtr InitBuffer();

        [FreeFunction("RenderingCommandBuffer_Bindings::CreateGPUFence_Internal", HasExplicitThis = true)]
        extern private IntPtr CreateGPUFence_Internal(GraphicsFenceType fenceType, SynchronisationStageFlags stage);


        [FreeFunction("RenderingCommandBuffer_Bindings::WaitOnGPUFence_Internal", HasExplicitThis = true)]
        extern private void WaitOnGPUFence_Internal(IntPtr fencePtr, SynchronisationStageFlags stage);

        [FreeFunction("RenderingCommandBuffer_Bindings::ReleaseBuffer", HasExplicitThis = true, IsThreadSafe = true)]
        extern private void ReleaseBuffer();

        [FreeFunction("RenderingCommandBuffer_Bindings::SetComputeFloatParam", HasExplicitThis = true)]
        extern public void SetComputeFloatParam([NotNull] ComputeShader computeShader, int nameID, float val);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetComputeIntParam", HasExplicitThis = true)]
        extern public void SetComputeIntParam([NotNull] ComputeShader computeShader, int nameID, int val);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetComputeVectorParam", HasExplicitThis = true)]
        extern public void SetComputeVectorParam([NotNull] ComputeShader computeShader, int nameID, Vector4 val);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetComputeVectorArrayParam", HasExplicitThis = true)]
        extern public void SetComputeVectorArrayParam([NotNull] ComputeShader computeShader, int nameID, Vector4[] values);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetComputeMatrixParam", HasExplicitThis = true)]
        extern public void SetComputeMatrixParam([NotNull] ComputeShader computeShader, int nameID, Matrix4x4 val);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetComputeMatrixArrayParam", HasExplicitThis = true)]
        extern public void SetComputeMatrixArrayParam([NotNull] ComputeShader computeShader, int nameID, Matrix4x4[] values);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeFloats", HasExplicitThis = true)]
        extern private void Internal_SetComputeFloats([NotNull] ComputeShader computeShader, int nameID, float[] values);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeInts", HasExplicitThis = true)]
        extern private void Internal_SetComputeInts([NotNull] ComputeShader computeShader, int nameID, int[] values);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeTextureParam", HasExplicitThis = true)]
        extern private void Internal_SetComputeTextureParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, ref UnityEngine.Rendering.RenderTargetIdentifier rt, int mipLevel, RenderTextureSubElement element);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetComputeBufferParam", HasExplicitThis = true)]
        extern private void Internal_SetComputeBufferParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, ComputeBuffer buffer);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetComputeBufferParam", HasExplicitThis = true)]
        extern private void Internal_SetComputeGraphicsBufferHandleParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBufferHandle bufferHandle);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetComputeBufferParam", HasExplicitThis = true)]
        extern private void Internal_SetComputeGraphicsBufferParam([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, GraphicsBuffer buffer);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetComputeConstantBufferParam", HasExplicitThis = true)]
        extern private void Internal_SetComputeConstantComputeBufferParam([NotNull] ComputeShader computeShader, int nameID, ComputeBuffer buffer, int offset, int size);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetComputeConstantBufferParam", HasExplicitThis = true)]
        extern private void Internal_SetComputeConstantGraphicsBufferParam([NotNull] ComputeShader computeShader, int nameID, GraphicsBuffer buffer, int offset, int size);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetComputeParamsFromMaterial", HasExplicitThis = true)]
        extern private void Internal_SetComputeParamsFromMaterial([NotNull] ComputeShader computeShader, int kernelIndex, Material material);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchCompute", HasExplicitThis = true, ThrowsException = true)]
        extern private void Internal_DispatchCompute([NotNull] ComputeShader computeShader, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchComputeIndirect", HasExplicitThis = true, ThrowsException = true)]
        extern private void Internal_DispatchComputeIndirect([NotNull] ComputeShader computeShader, int kernelIndex, ComputeBuffer indirectBuffer, uint argsOffset);
        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchComputeIndirect", HasExplicitThis = true, ThrowsException = true)]
        extern private void Internal_DispatchComputeIndirectGraphicsBuffer([NotNull] ComputeShader computeShader, int kernelIndex, GraphicsBuffer indirectBuffer, uint argsOffset);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingBufferParam", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingComputeBufferParam([NotNull] RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingBufferParam", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingGraphicsBufferParam([NotNull] RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingBufferParam", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingGraphicsBufferHandleParam([NotNull] RayTracingShader rayTracingShader, int nameID, GraphicsBufferHandle bufferHandle);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingConstantBufferParam", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingConstantComputeBufferParam([NotNull] RayTracingShader rayTracingShader, int nameID, ComputeBuffer buffer, int offset, int size);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingConstantBufferParam", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingConstantGraphicsBufferParam([NotNull] RayTracingShader rayTracingShader, int nameID, GraphicsBuffer buffer, int offset, int size);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingTextureParam", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingTextureParam([NotNull] RayTracingShader rayTracingShader, int nameID, ref UnityEngine.Rendering.RenderTargetIdentifier rt);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingFloatParam", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingFloatParam([NotNull] RayTracingShader rayTracingShader, int nameID, float val);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingIntParam", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingIntParam([NotNull] RayTracingShader rayTracingShader, int nameID, int val);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingVectorParam", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingVectorParam([NotNull] RayTracingShader rayTracingShader, int nameID, Vector4 val);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingVectorArrayParam", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingVectorArrayParam([NotNull] RayTracingShader rayTracingShader, int nameID, Vector4[] values);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingMatrixParam", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingMatrixParam([NotNull] RayTracingShader rayTracingShader, int nameID, Matrix4x4 val);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingMatrixArrayParam", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingMatrixArrayParam([NotNull] RayTracingShader rayTracingShader, int nameID, Matrix4x4[] values);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingFloats", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingFloats([NotNull] RayTracingShader rayTracingShader, int nameID, float[] values);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingInts", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingInts([NotNull] RayTracingShader rayTracingShader, int nameID, int[] values);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_BuildRayTracingAccelerationStructure", HasExplicitThis = true)]
        extern private void Internal_BuildRayTracingAccelerationStructure([NotNull] RayTracingAccelerationStructure accelerationStructure, RayTracingAccelerationStructure.BuildSettings buildSettings);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetRayTracingAccelerationStructure", HasExplicitThis = true)]
        extern private void Internal_SetRayTracingAccelerationStructure([NotNull] RayTracingShader rayTracingShader, int nameID, [NotNull] RayTracingAccelerationStructure accelerationStructure);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetComputeRayTracingAccelerationStructure", HasExplicitThis = true)]
        extern private void Internal_SetComputeRayTracingAccelerationStructure([NotNull] ComputeShader computeShader, int kernelIndex, int nameID, [NotNull] RayTracingAccelerationStructure accelerationStructure);

        [NativeMethod("AddSetRayTracingShaderPass")]
        extern public void SetRayTracingShaderPass([NotNull] RayTracingShader rayTracingShader, string passName);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchRays", HasExplicitThis = true, ThrowsException = true)]
        extern private void Internal_DispatchRays([NotNull] RayTracingShader rayTracingShader, string rayGenShaderName, UInt32 width, UInt32 height, UInt32 depth, Camera camera = null);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchRaysIndirect", HasExplicitThis = true, ThrowsException = true)]
        extern private void Internal_DispatchRaysIndirect([NotNull] RayTracingShader rayTracingShader, string rayGenShaderName, [NotNull] GraphicsBuffer argsBuffer, uint argsOffset = 0, Camera camera = null);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_BuildMachineLearningOperator", HasExplicitThis = true)]
        extern private void Internal_BuildMachineLearningOperator(IntPtr machineLearningOperator);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_SetMachineLearningOperatorTensors", HasExplicitThis = true)]
        extern private void Internal_SetMachineLearningOperatorTensors(IntPtr machineLearningOperator, ReadOnlySpan<IntPtr> inputs, ReadOnlySpan<IntPtr> outputs);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DispatchMachineLearningOperator", HasExplicitThis = true)]
        extern private void Internal_DispatchMachineLearningOperator(IntPtr machineLearningOperator);

        [NativeMethod("AddGenerateMips")]
        extern private void Internal_GenerateMips(RenderTargetIdentifier rt);

        [NativeMethod("AddResolveAntiAliasedSurface")]
        extern private void Internal_ResolveAntiAliasedSurface(RenderTexture rt, RenderTexture target);

        [NativeMethod("AddCopyCounterValue")]
        extern private void CopyCounterValueCC(ComputeBuffer src, ComputeBuffer dst, uint dstOffsetBytes);
        [NativeMethod("AddCopyCounterValue")]
        extern private void CopyCounterValueGC(GraphicsBuffer src, ComputeBuffer dst, uint dstOffsetBytes);
        [NativeMethod("AddCopyCounterValue")]
        extern private void CopyCounterValueCG(ComputeBuffer src, GraphicsBuffer dst, uint dstOffsetBytes);
        [NativeMethod("AddCopyCounterValue")]
        extern private void CopyCounterValueGG(GraphicsBuffer src, GraphicsBuffer dst, uint dstOffsetBytes);

        extern public string name { get; set; }
        extern public int sizeInBytes { [NativeMethod("GetBufferSize")] get; }

        [NativeMethod("ClearCommands")]
        extern public void Clear();

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMesh", HasExplicitThis = true)]
        extern private void Internal_DrawMesh([NotNull] Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass, MaterialPropertyBlock properties);

        [NativeMethod("AddDrawMultipleMeshes")]
        extern private void Internal_DrawMultipleMeshes(Matrix4x4[] matrices, Mesh[] meshes, int[] subsetIndices, int count, Material material, int shaderPass, MaterialPropertyBlock properties);

        [NativeMethod("AddDrawRenderer")]
        extern private void Internal_DrawRenderer([NotNull] Renderer renderer, Material material, int submeshIndex, int shaderPass);

        [NativeMethod("AddDrawRendererList")]
        extern private void Internal_DrawRendererList(RendererList rendererList);

        private void Internal_DrawRenderer(Renderer renderer, Material material, int submeshIndex)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            Internal_DrawRenderer(renderer, material, submeshIndex, -1);
        }

        private void Internal_DrawRenderer(Renderer renderer, Material material)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            Internal_DrawRenderer(renderer, material, 0);
        }

        [NativeMethod("AddDrawProcedural")]
        extern private void Internal_DrawProcedural(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int vertexCount, int instanceCount, MaterialPropertyBlock properties);

        [NativeMethod("AddDrawProceduralIndexed")]
        extern private void Internal_DrawProceduralIndexed(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, int indexCount, int instanceCount, MaterialPropertyBlock properties);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndirect", HasExplicitThis = true)]
        extern private void Internal_DrawProceduralIndirect(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndexedIndirect", HasExplicitThis = true)]
        extern private void Internal_DrawProceduralIndexedIndirect(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndirect", HasExplicitThis = true)]
        extern private void Internal_DrawProceduralIndirectGraphicsBuffer(Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawProceduralIndexedIndirect", HasExplicitThis = true)]
        extern private void Internal_DrawProceduralIndexedIndirectGraphicsBuffer(GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstanced", HasExplicitThis = true)]
        extern private void Internal_DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, int shaderPass, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstancedProcedural", HasExplicitThis = true)]
        extern private void Internal_DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, int shaderPass, int count, MaterialPropertyBlock properties);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstancedIndirect", HasExplicitThis = true)]
        extern private void Internal_DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, int shaderPass, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawMeshInstancedIndirect", HasExplicitThis = true)]
        extern private void Internal_DrawMeshInstancedIndirectGraphicsBuffer(Mesh mesh, int submeshIndex, Material material, int shaderPass, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties);

        [FreeFunction("RenderingCommandBuffer_Bindings::Internal_DrawOcclusionMesh", HasExplicitThis = true)]
        extern private void Internal_DrawOcclusionMesh(RectInt normalizedCamViewport);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetRandomWriteTarget_Texture", HasExplicitThis = true, ThrowsException = true)]
        extern private void SetRandomWriteTarget_Texture(int index, ref UnityEngine.Rendering.RenderTargetIdentifier rt);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetRandomWriteTarget_Buffer", HasExplicitThis = true, ThrowsException = true)]
        extern private void SetRandomWriteTarget_Buffer(int index, ComputeBuffer uav, bool preserveCounterValue);
        [FreeFunction("RenderingCommandBuffer_Bindings::SetRandomWriteTarget_Buffer", HasExplicitThis = true, ThrowsException = true)]
        extern private void SetRandomWriteTarget_GraphicsBuffer(int index, GraphicsBuffer uav, bool preserveCounterValue);

        [FreeFunction("RenderingCommandBuffer_Bindings::ClearRandomWriteTargets", HasExplicitThis = true, ThrowsException = true)]
        extern public void ClearRandomWriteTargets();

        [FreeFunction("RenderingCommandBuffer_Bindings::SetViewport", HasExplicitThis = true, ThrowsException = true)]
        extern public void SetViewport(Rect pixelRect);

        [FreeFunction("RenderingCommandBuffer_Bindings::EnableScissorRect", HasExplicitThis = true, ThrowsException = true)]
        extern public void EnableScissorRect(Rect scissor);

        [FreeFunction("RenderingCommandBuffer_Bindings::DisableScissorRect", HasExplicitThis = true, ThrowsException = true)]
        extern public void DisableScissorRect();

        [FreeFunction("RenderingCommandBuffer_Bindings::CopyTexture_Internal", HasExplicitThis = true)]
        extern private void CopyTexture_Internal(ref UnityEngine.Rendering.RenderTargetIdentifier src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight,
            ref UnityEngine.Rendering.RenderTargetIdentifier dst, int dstElement, int dstMip, int dstX, int dstY, int mode);

        [FreeFunction("RenderingCommandBuffer_Bindings::Blit_Texture", HasExplicitThis = true)]
        extern private void Blit_Texture(Texture source, ref UnityEngine.Rendering.RenderTargetIdentifier dest, Material mat, int pass, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice);

        [FreeFunction("RenderingCommandBuffer_Bindings::Blit_Identifier", HasExplicitThis = true)]
        extern private void Blit_Identifier(ref UnityEngine.Rendering.RenderTargetIdentifier source, ref UnityEngine.Rendering.RenderTargetIdentifier dest, Material mat, int pass, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice);

        [FreeFunction("RenderingCommandBuffer_Bindings::GetTemporaryRT", HasExplicitThis = true)]
        extern private void GetTemporaryRT(int nameID, int width, int height, FilterMode filter, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode, bool useDynamicScale, ShadowSamplingMode shadowSamplingMode);

        public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode, bool useDynamicScale)
        {
            GraphicsFormat depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, format);

            GetTemporaryRT(nameID, width, height, filter, format, depthStencilFormat, antiAliasing, enableRandomWrite, memorylessMode, useDynamicScale, ShadowSamplingMode.None);
        }

        public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode)
        {
            GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, antiAliasing, enableRandomWrite, memorylessMode, false);
        }

        public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite)
        {
            GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, antiAliasing, enableRandomWrite, RenderTextureMemoryless.None);
        }

        public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing)
        {
            GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, antiAliasing, false, RenderTextureMemoryless.None);
        }

        public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, GraphicsFormat format)
        {
            GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, 1);
        }

        public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode, bool useDynamicScale)
        {
            ShadowSamplingMode shadowSamplingMode = RenderTexture.GetShadowSamplingModeForFormat(format);
            GraphicsFormat colorFormat = GraphicsFormatUtility.GetGraphicsFormat(format, readWrite);
            GraphicsFormat depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, format);

            GetTemporaryRT(nameID, width, height, filter, colorFormat, depthStencilFormat, antiAliasing, enableRandomWrite, memorylessMode, useDynamicScale, shadowSamplingMode);
        }

        public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, bool enableRandomWrite, RenderTextureMemoryless memorylessMode)
        {
            GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, readWrite, antiAliasing, enableRandomWrite, memorylessMode, false);
        }

        public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, bool enableRandomWrite)
        {
            GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, readWrite, antiAliasing, enableRandomWrite, RenderTextureMemoryless.None);
        }

        public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing)
        {
            GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, readWrite, antiAliasing, false);
        }

        public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite)
        {
            GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, readWrite, 1);
        }

        public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter, RenderTextureFormat format)
        {
            GetTemporaryRT(nameID, width, height, depthBuffer, filter, format, RenderTextureReadWrite.Default);
        }

        public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer, FilterMode filter)
        {
            GetTemporaryRT(nameID, width, height, depthBuffer, filter, RenderTextureFormat.Default);
        }

        public void GetTemporaryRT(int nameID, int width, int height, int depthBuffer)
        {
            GetTemporaryRT(nameID, width, height, depthBuffer, FilterMode.Point);
        }

        public void GetTemporaryRT(int nameID, int width, int height)
        {
            GetTemporaryRT(nameID, width, height, 0);
        }

        [FreeFunction("RenderingCommandBuffer_Bindings::GetTemporaryRTWithDescriptor", HasExplicitThis = true)]
        extern private void GetTemporaryRTWithDescriptor(int nameID, RenderTextureDescriptor desc, FilterMode filter);

        public void GetTemporaryRT(int nameID, RenderTextureDescriptor desc, FilterMode filter)
        {
            GetTemporaryRTWithDescriptor(nameID, desc, filter);
        }

        public void GetTemporaryRT(int nameID, RenderTextureDescriptor desc)
        {
            GetTemporaryRT(nameID, desc, FilterMode.Point);
        }

        [FreeFunction("RenderingCommandBuffer_Bindings::GetTemporaryRTArray", HasExplicitThis = true)]
        extern private void GetTemporaryRTArray(int nameID, int width, int height, int slices, FilterMode filter, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat, int antiAliasing, bool enableRandomWrite, bool useDynamicScale, ShadowSamplingMode shadowSamplingMode);

        public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite, bool useDynamicScale)
        {
            GraphicsFormat depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, format);

            GetTemporaryRTArray(nameID, width, height, slices, filter, format, depthStencilFormat, antiAliasing, enableRandomWrite, useDynamicScale, ShadowSamplingMode.None);
        }

        public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing, bool enableRandomWrite)
        {
            GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, antiAliasing, enableRandomWrite, false);
        }

        public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, GraphicsFormat format, int antiAliasing)
        {
            GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, antiAliasing, false);
        }

        public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, GraphicsFormat format)
        {
            GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, 1);
        }

        public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, bool enableRandomWrite)
        {
            ShadowSamplingMode shadowSamplingMode = RenderTexture.GetShadowSamplingModeForFormat(format);
            GraphicsFormat colorFormat = GraphicsFormatUtility.GetGraphicsFormat(format, readWrite);
            GraphicsFormat depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, format);

            GetTemporaryRTArray(nameID, width, height, slices, filter, colorFormat, depthStencilFormat, antiAliasing, enableRandomWrite, false, shadowSamplingMode);
        }

        public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing)
        {
            GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, readWrite, antiAliasing, false);
        }

        public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, RenderTextureFormat format, RenderTextureReadWrite readWrite)
        {
            GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, readWrite, 1);
        }

        public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter, RenderTextureFormat format)
        {
            GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, format, RenderTextureReadWrite.Default);
        }

        public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer, FilterMode filter)
        {
            GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, filter, RenderTextureFormat.Default);
        }

        public void GetTemporaryRTArray(int nameID, int width, int height, int slices, int depthBuffer)
        {
            GetTemporaryRTArray(nameID, width, height, slices, depthBuffer, FilterMode.Point);
        }

        public void GetTemporaryRTArray(int nameID, int width, int height, int slices)
        {
            GetTemporaryRTArray(nameID, width, height, slices, 0);
        }

        [FreeFunction("RenderingCommandBuffer_Bindings::ReleaseTemporaryRT", HasExplicitThis = true)]
        extern public void ReleaseTemporaryRT(int nameID);

        public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor)
        {
            ClearRenderTarget(clearDepth, clearColor, backgroundColor, 1.0f, 0);
        }

        public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor, float depth)
        {
            ClearRenderTarget(clearDepth, clearColor, backgroundColor, depth, 0);
        }

        public void ClearRenderTarget(bool clearDepth, bool clearColor, Color backgroundColor, float depth = 1.0f, uint stencil = 0)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            // Legacy behaviour: this interface implicitely clears stencil when depth is cleared.
            ClearRenderTargetSingle_Internal((RTClearFlags)((clearColor ? RTClearFlags.Color : RTClearFlags.None) | (clearDepth ? RTClearFlags.DepthStencil : RTClearFlags.None)), backgroundColor, depth, stencil);
        }

        public void ClearRenderTarget(RTClearFlags clearFlags, Color backgroundColor, float depth = 1.0f, uint stencil = 0)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            ClearRenderTargetSingle_Internal(clearFlags, backgroundColor, depth, stencil);
        }

        public void ClearRenderTarget(RTClearFlags clearFlags, Color[] backgroundColors, float depth = 1.0f, uint stencil = 0)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            if (backgroundColors.Length < 1)
                throw new ArgumentException(string.Format("The number of clear colors must be at least 1, but is {0}", backgroundColors.Length));
            if (backgroundColors.Length > SystemInfo.supportedRenderTargetCount)
                throw new ArgumentException(string.Format("The number of clear colors ({0}) exceeds the maximum supported number of render targets ({1})", backgroundColors.Length, SystemInfo.supportedRenderTargetCount));

            ClearRenderTargetMulti_Internal(clearFlags, backgroundColors, depth, stencil);
        }

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalFloat", HasExplicitThis = true)]
        extern public void SetGlobalFloat(int nameID, float value);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalInt", HasExplicitThis = true)]
        extern public void SetGlobalInt(int nameID, int value);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalInteger", HasExplicitThis = true)]
        extern public void SetGlobalInteger(int nameID, int value);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalVector", HasExplicitThis = true)]
        extern public void SetGlobalVector(int nameID, Vector4 value);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalColor", HasExplicitThis = true)]
        extern public void SetGlobalColor(int nameID, Color value);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalMatrix", HasExplicitThis = true)]
        extern public void SetGlobalMatrix(int nameID, Matrix4x4 value);

        [FreeFunction("RenderingCommandBuffer_Bindings::EnableShaderKeyword", HasExplicitThis = true)]
        extern public void EnableShaderKeyword(string keyword);

        [FreeFunction("RenderingCommandBuffer_Bindings::EnableShaderKeyword", HasExplicitThis = true)]
        extern private void EnableGlobalKeyword(GlobalKeyword keyword);
        [FreeFunction("RenderingCommandBuffer_Bindings::EnableMaterialKeyword", HasExplicitThis = true)]
        extern private void EnableMaterialKeyword(Material material, LocalKeyword keyword);
        [FreeFunction("RenderingCommandBuffer_Bindings::EnableComputeKeyword", HasExplicitThis = true)]
        extern private void EnableComputeKeyword(ComputeShader computeShader, LocalKeyword keyword);

        public void EnableKeyword(in GlobalKeyword keyword) { EnableGlobalKeyword(keyword); }
        public void EnableKeyword(Material material, in LocalKeyword keyword) { EnableMaterialKeyword(material, keyword); }
        public void EnableKeyword(ComputeShader computeShader, in LocalKeyword keyword) { EnableComputeKeyword(computeShader, keyword); }

        [FreeFunction("RenderingCommandBuffer_Bindings::DisableShaderKeyword", HasExplicitThis = true)]
        extern public void DisableShaderKeyword(string keyword);

        [FreeFunction("RenderingCommandBuffer_Bindings::DisableShaderKeyword", HasExplicitThis = true)]
        extern private void DisableGlobalKeyword(GlobalKeyword keyword);
        [FreeFunction("RenderingCommandBuffer_Bindings::DisableMaterialKeyword", HasExplicitThis = true)]
        extern private void DisableMaterialKeyword(Material material, LocalKeyword keyword);
        [FreeFunction("RenderingCommandBuffer_Bindings::DisableComputeKeyword", HasExplicitThis = true)]
        extern private void DisableComputeKeyword(ComputeShader computeShader, LocalKeyword keyword);

        public void DisableKeyword(in GlobalKeyword keyword) { DisableGlobalKeyword(keyword); }
        public void DisableKeyword(Material material, in LocalKeyword keyword) { DisableMaterialKeyword(material, keyword); }
        public void DisableKeyword(ComputeShader computeShader, in LocalKeyword keyword) { DisableComputeKeyword(computeShader, keyword); }

        [FreeFunction("RenderingCommandBuffer_Bindings::SetShaderKeyword", HasExplicitThis = true)]
        extern private void SetGlobalKeyword(GlobalKeyword keyword, bool value);
        [FreeFunction("RenderingCommandBuffer_Bindings::SetMaterialKeyword", HasExplicitThis = true)]
        extern private void SetMaterialKeyword(Material material, LocalKeyword keyword, bool value);
        [FreeFunction("RenderingCommandBuffer_Bindings::SetComputeKeyword", HasExplicitThis = true)]
        extern private void SetComputeKeyword(ComputeShader computeShader, LocalKeyword keyword, bool value);

        public void SetKeyword(in GlobalKeyword keyword, bool value) { SetGlobalKeyword(keyword, value); }
        public void SetKeyword(Material material, in LocalKeyword keyword, bool value) { SetMaterialKeyword(material, keyword, value); }
        public void SetKeyword(ComputeShader computeShader, in LocalKeyword keyword, bool value) { SetComputeKeyword(computeShader, keyword, value); }

        [FreeFunction("RenderingCommandBuffer_Bindings::SetViewMatrix", HasExplicitThis = true, ThrowsException = true)]
        extern public void SetViewMatrix(Matrix4x4 view);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetProjectionMatrix", HasExplicitThis = true, ThrowsException = true)]
        extern public void SetProjectionMatrix(Matrix4x4 proj);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetViewProjectionMatrices", HasExplicitThis = true, ThrowsException = true)]
        extern public void SetViewProjectionMatrices(Matrix4x4 view, Matrix4x4 proj);

        [NativeMethod("AddSetGlobalDepthBias")]
        extern public void SetGlobalDepthBias(float bias, float slopeBias);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetExecutionFlags", HasExplicitThis = true, ThrowsException = true)]
        extern public void SetExecutionFlags(CommandBufferExecutionFlags flags);

        [FreeFunction("RenderingCommandBuffer_Bindings::ValidateAgainstExecutionFlags", HasExplicitThis = true, ThrowsException = true)]
        extern private bool ValidateAgainstExecutionFlags(CommandBufferExecutionFlags requiredFlags, CommandBufferExecutionFlags invalidFlags);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalFloatArrayListImpl", HasExplicitThis = true)]
        extern private void SetGlobalFloatArrayListImpl(int nameID, object values);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalVectorArrayListImpl", HasExplicitThis = true)]
        extern private void SetGlobalVectorArrayListImpl(int nameID, object values);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalMatrixArrayListImpl", HasExplicitThis = true)]
        extern private void SetGlobalMatrixArrayListImpl(int nameID, object values);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalFloatArray", HasExplicitThis = true, ThrowsException = true)]
        extern public void SetGlobalFloatArray(int nameID, [NotNull]float[] values);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalVectorArray", HasExplicitThis = true, ThrowsException = true)]
        extern public void SetGlobalVectorArray(int nameID, [NotNull]Vector4[] values);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalMatrixArray", HasExplicitThis = true, ThrowsException = true)]
        extern public void SetGlobalMatrixArray(int nameID, [NotNull]Matrix4x4[] values);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetLateLatchProjectionMatrices", HasExplicitThis = true, ThrowsException = true)]
        extern public void SetLateLatchProjectionMatrices([NotNull]Matrix4x4[] projectionMat);

        [FreeFunction("RenderingCommandBuffer_Bindings::MarkLateLatchMatrixShaderPropertyID", HasExplicitThis = true)]
        extern public void MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType matrixPropertyType, int shaderPropertyID);

        [FreeFunction("RenderingCommandBuffer_Bindings::UnmarkLateLatchMatrix", HasExplicitThis = true)]
        extern public void UnmarkLateLatchMatrix(CameraLateLatchMatrixType matrixPropertyType);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalTexture_Impl", HasExplicitThis = true)]
        extern private void SetGlobalTexture_Impl(int nameID, ref UnityEngine.Rendering.RenderTargetIdentifier rt, RenderTextureSubElement element);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalBuffer", HasExplicitThis = true)]
        extern private void SetGlobalBufferInternal(int nameID, ComputeBuffer value);
        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalBuffer", HasExplicitThis = true)]
        extern private void SetGlobalGraphicsBufferInternal(int nameID, GraphicsBuffer value);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalRayTracingAccelerationStructure", HasExplicitThis = true)]
        extern private void SetGlobalRayTracingAccelerationStructureInternal(RayTracingAccelerationStructure accelerationStructure, int nameID);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetShadowSamplingMode_Impl", HasExplicitThis = true)]
        extern private void SetShadowSamplingMode_Impl(ref UnityEngine.Rendering.RenderTargetIdentifier shadowmap, ShadowSamplingMode mode);

        [FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginEventInternal", HasExplicitThis = true)]
        extern private void IssuePluginEventInternal(IntPtr callback, int eventID);

        [FreeFunction("RenderingCommandBuffer_Bindings::BeginSample", HasExplicitThis = true)]
        extern public void BeginSample(string name);

        [FreeFunction("RenderingCommandBuffer_Bindings::EndSample", HasExplicitThis = true)]
        extern public void EndSample(string name);
        public void BeginSample(CustomSampler sampler) { BeginSample_CustomSampler(sampler); }
        public void EndSample(CustomSampler sampler) { EndSample_CustomSampler(sampler); }

        [FreeFunction("RenderingCommandBuffer_Bindings::BeginSample_CustomSampler", HasExplicitThis = true)]
        extern private void BeginSample_CustomSampler([NotNull] CustomSampler sampler);
        [FreeFunction("RenderingCommandBuffer_Bindings::EndSample_CustomSampler", HasExplicitThis = true)]
        extern private void EndSample_CustomSampler([NotNull] CustomSampler sampler);

        [Pure]
        [MethodImpl(256)]
        [Conditional("ENABLE_PROFILER")]
        public void BeginSample(ProfilerMarker marker) { BeginSample_ProfilerMarker(marker.Handle); }

        [Pure]
        [MethodImpl(256)]
        [Conditional("ENABLE_PROFILER")]
        public void EndSample(ProfilerMarker marker) { EndSample_ProfilerMarker(marker.Handle); }

        [FreeFunction("RenderingCommandBuffer_Bindings::BeginSample_ProfilerMarker", HasExplicitThis = true, ThrowsException = true)]
        extern private void BeginSample_ProfilerMarker(IntPtr markerHandle);
        [FreeFunction("RenderingCommandBuffer_Bindings::EndSample_ProfilerMarker", HasExplicitThis = true, ThrowsException = true)]
        extern private void EndSample_ProfilerMarker(IntPtr markerHandle);

        [FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginEventAndDataInternal", HasExplicitThis = true)]
        extern private void IssuePluginEventAndDataInternal(IntPtr callback, int eventID, IntPtr data);

        [FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginEventAndDataWithFlagsInternal", HasExplicitThis = true)]
        extern private void IssuePluginEventAndDataInternalWithFlags(IntPtr callback, int eventID, CustomMarkerCallbackFlags flags, IntPtr data);

        [FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginCustomBlitInternal", HasExplicitThis = true)]
        extern private void IssuePluginCustomBlitInternal(IntPtr callback, uint command, ref UnityEngine.Rendering.RenderTargetIdentifier source, ref UnityEngine.Rendering.RenderTargetIdentifier dest, uint commandParam, uint commandFlags);

        [FreeFunction("RenderingCommandBuffer_Bindings::IssuePluginCustomTextureUpdateInternal", HasExplicitThis = true)]
        extern private void IssuePluginCustomTextureUpdateInternal(IntPtr callback, Texture targetTexture, uint userData, bool useNewUnityRenderingExtTextureUpdateParamsV2);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalConstantBuffer", HasExplicitThis = true)]
        extern private void SetGlobalConstantBufferInternal(ComputeBuffer buffer, int nameID, int offset, int size);
        [FreeFunction("RenderingCommandBuffer_Bindings::SetGlobalConstantBuffer", HasExplicitThis = true)]
        extern private void SetGlobalConstantGraphicsBufferInternal(GraphicsBuffer buffer, int nameID, int offset, int size);

        [FreeFunction("RenderingCommandBuffer_Bindings::IncrementUpdateCount", HasExplicitThis = true)]
        extern public void IncrementUpdateCount(UnityEngine.Rendering.RenderTargetIdentifier dest);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetInstanceMultiplier", HasExplicitThis = true)]
        extern public void SetInstanceMultiplier(uint multiplier);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetFoveatedRenderingMode", HasExplicitThis = true)]
        extern public void SetFoveatedRenderingMode(FoveatedRenderingMode foveatedRenderingMode);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetWireframe", HasExplicitThis = true)]
        extern public void SetWireframe(bool enable);

        [FreeFunction("RenderingCommandBuffer_Bindings::ConfigureFoveatedRendering", HasExplicitThis = true)]
        extern public void ConfigureFoveatedRendering(IntPtr platformData);

        static public bool ThrowOnSetRenderTarget = false;

        static void CheckThrowOnSetRenderTarget()
        {
            if (ThrowOnSetRenderTarget) throw new Exception("Setrendertarget is not allowed in this context");
        }

        public void SetRenderTarget(RenderTargetIdentifier rt)
        {
            CheckThrowOnSetRenderTarget();
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            SetRenderTargetSingle_Internal(rt, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
        }

        public void SetRenderTarget(RenderTargetIdentifier rt, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction)
        {
            CheckThrowOnSetRenderTarget();
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            if (loadAction == RenderBufferLoadAction.Clear)
                throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
            SetRenderTargetSingle_Internal(rt, loadAction, storeAction, loadAction, storeAction);
        }

        public void SetRenderTarget(RenderTargetIdentifier rt,
            RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction,
            RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
        {
            CheckThrowOnSetRenderTarget();
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            if (colorLoadAction == RenderBufferLoadAction.Clear || depthLoadAction == RenderBufferLoadAction.Clear)
                throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
            SetRenderTargetSingle_Internal(rt, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
        }

        public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            if (mipLevel < 0)
                throw new ArgumentException(String.Format("Invalid value for mipLevel ({0})", mipLevel));
            SetRenderTargetSingle_Internal(new RenderTargetIdentifier(rt, mipLevel),
                RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
        }

        public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel, CubemapFace cubemapFace)
        {
            CheckThrowOnSetRenderTarget();
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            if (mipLevel < 0)
                throw new ArgumentException(String.Format("Invalid value for mipLevel ({0})", mipLevel));
            SetRenderTargetSingle_Internal(new RenderTargetIdentifier(rt, mipLevel, cubemapFace),
                RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
        }

        public void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel, CubemapFace cubemapFace, int depthSlice)
        {
            CheckThrowOnSetRenderTarget();
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            if (depthSlice < -1)
                throw new ArgumentException(String.Format("Invalid value for depthSlice ({0})", depthSlice));
            if (mipLevel < 0)
                throw new ArgumentException(String.Format("Invalid value for mipLevel ({0})", mipLevel));
            SetRenderTargetSingle_Internal(new RenderTargetIdentifier(rt, mipLevel, cubemapFace, depthSlice),
                RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
        }

        public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth)
        {
            CheckThrowOnSetRenderTarget();
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            SetRenderTargetColorDepth_Internal(color, depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderTargetFlags.None);
        }

        public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel)
        {
            CheckThrowOnSetRenderTarget();
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            if (mipLevel < 0)
                throw new ArgumentException(String.Format("Invalid value for mipLevel ({0})", mipLevel));
            SetRenderTargetColorDepth_Internal(new RenderTargetIdentifier(color, mipLevel),
                depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store,
                RenderTargetFlags.None);
        }

        public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace)
        {
            CheckThrowOnSetRenderTarget();
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            if (mipLevel < 0)
                throw new ArgumentException(String.Format("Invalid value for mipLevel ({0})", mipLevel));
            SetRenderTargetColorDepth_Internal(new RenderTargetIdentifier(color, mipLevel, cubemapFace),
                depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderTargetFlags.None);
        }

        public void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace, int depthSlice)
        {
            CheckThrowOnSetRenderTarget();
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            if (depthSlice < -1)
                throw new ArgumentException(String.Format("Invalid value for depthSlice ({0})", depthSlice));
            if (mipLevel < 0)
                throw new ArgumentException(String.Format("Invalid value for mipLevel ({0})", mipLevel));
            SetRenderTargetColorDepth_Internal(new RenderTargetIdentifier(color, mipLevel, cubemapFace, depthSlice),
                depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderTargetFlags.None);
        }

        public void SetRenderTarget(RenderTargetIdentifier color, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction,
            RenderTargetIdentifier depth, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
        {
            CheckThrowOnSetRenderTarget();
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            if (colorLoadAction == RenderBufferLoadAction.Clear || depthLoadAction == RenderBufferLoadAction.Clear)
                throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
            SetRenderTargetColorDepth_Internal(color, depth, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction, RenderTargetFlags.None);
        }

        public void SetRenderTarget(RenderTargetIdentifier[] colors, Rendering.RenderTargetIdentifier depth)
        {
            CheckThrowOnSetRenderTarget();
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            if (colors.Length < 1)
                throw new ArgumentException(string.Format("colors.Length must be at least 1, but was {0}", colors.Length));
            if (colors.Length > SystemInfo.supportedRenderTargetCount)
                throw new ArgumentException(string.Format("colors.Length is {0} and exceeds the maximum number of supported render targets ({1})", colors.Length, SystemInfo.supportedRenderTargetCount));

            SetRenderTargetMulti_Internal(colors, depth, null, null, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderTargetFlags.None);
        }

        public void SetRenderTarget(RenderTargetIdentifier[] colors, Rendering.RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace, int depthSlice)
        {
            CheckThrowOnSetRenderTarget();
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            if (colors.Length < 1)
                throw new ArgumentException(string.Format("colors.Length must be at least 1, but was {0}", colors.Length));
            if (colors.Length > SystemInfo.supportedRenderTargetCount)
                throw new ArgumentException(string.Format("colors.Length is {0} and exceeds the maximum number of supported render targets ({1})", colors.Length, SystemInfo.supportedRenderTargetCount));
            SetRenderTargetMultiSubtarget(colors, depth, null, null, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, mipLevel, cubemapFace, depthSlice);
        }

        public void SetRenderTarget(RenderTargetBinding binding, int mipLevel, CubemapFace cubemapFace, int depthSlice)
        {
            CheckThrowOnSetRenderTarget();
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            if (binding.colorRenderTargets.Length < 1)
                throw new ArgumentException(string.Format("The number of color render targets must be at least 1, but was {0}", binding.colorRenderTargets.Length));
            if (binding.colorRenderTargets.Length > SystemInfo.supportedRenderTargetCount)
                throw new ArgumentException(string.Format("The number of color render targets ({0}) and exceeds the maximum supported number of render targets ({1})", binding.colorRenderTargets.Length, SystemInfo.supportedRenderTargetCount));
            if (binding.colorLoadActions.Length != binding.colorRenderTargets.Length)
                throw new ArgumentException(string.Format("The number of color load actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
            if (binding.colorStoreActions.Length != binding.colorRenderTargets.Length)
                throw new ArgumentException(string.Format("The number of color store actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
            if (binding.depthLoadAction == RenderBufferLoadAction.Clear || Array.IndexOf(binding.colorLoadActions, RenderBufferLoadAction.Clear) > -1)
                throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");
            if (binding.colorRenderTargets.Length == 1) // non-MRT case respects mip/face/slice of color's RenderTargetIdentifier
                SetRenderTargetColorDepthSubtarget(binding.colorRenderTargets[0], binding.depthRenderTarget, binding.colorLoadActions[0], binding.colorStoreActions[0], binding.depthLoadAction, binding.depthStoreAction, mipLevel, cubemapFace, depthSlice);
            else
                SetRenderTargetMultiSubtarget(binding.colorRenderTargets, binding.depthRenderTarget, binding.colorLoadActions, binding.colorStoreActions, binding.depthLoadAction, binding.depthStoreAction, mipLevel, cubemapFace, depthSlice);
        }

        public void SetRenderTarget(RenderTargetBinding binding)
        {
            CheckThrowOnSetRenderTarget();
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            if (binding.colorRenderTargets.Length < 1)
                throw new ArgumentException(string.Format("The number of color render targets must be at least 1, but was {0}", binding.colorRenderTargets.Length));
            if (binding.colorRenderTargets.Length > SystemInfo.supportedRenderTargetCount)
                throw new ArgumentException(string.Format("The number of color render targets ({0}) and exceeds the maximum supported number of render targets ({1})", binding.colorRenderTargets.Length, SystemInfo.supportedRenderTargetCount));
            if (binding.colorLoadActions.Length != binding.colorRenderTargets.Length)
                throw new ArgumentException(string.Format("The number of color load actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
            if (binding.colorStoreActions.Length != binding.colorRenderTargets.Length)
                throw new ArgumentException(string.Format("The number of color store actions provided ({0}) does not match the number of color render targets ({1})", binding.colorLoadActions.Length, binding.colorRenderTargets.Length));
            if (binding.depthLoadAction == RenderBufferLoadAction.Clear || Array.IndexOf(binding.colorLoadActions, RenderBufferLoadAction.Clear) > -1)
                throw new ArgumentException("RenderBufferLoadAction.Clear is not supported");

            if (binding.colorRenderTargets.Length == 1) // non-MRT case respects mip/face/slice of color's RenderTargetIdentifier
                SetRenderTargetColorDepth_Internal(binding.colorRenderTargets[0], binding.depthRenderTarget, binding.colorLoadActions[0], binding.colorStoreActions[0], binding.depthLoadAction, binding.depthStoreAction, binding.flags);
            else
                SetRenderTargetMulti_Internal(binding.colorRenderTargets, binding.depthRenderTarget, binding.colorLoadActions, binding.colorStoreActions, binding.depthLoadAction, binding.depthStoreAction, binding.flags);
        }

        extern private void ClearRenderTargetSingle_Internal(RTClearFlags clearFlags, Color color, float depth, UInt32 stencil);
        extern private void ClearRenderTargetMulti_Internal(RTClearFlags clearFlags, Color[] colors, float depth, UInt32 stencil);

        extern private void SetRenderTargetSingle_Internal(RenderTargetIdentifier rt,
            RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction,
            RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction);

        extern private void SetRenderTargetColorDepth_Internal(RenderTargetIdentifier color, RenderTargetIdentifier depth,
            RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction,
            RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction,
            RenderTargetFlags flags);

        extern private void SetRenderTargetMulti_Internal(RenderTargetIdentifier[] colors, Rendering.RenderTargetIdentifier depth,
            RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions,
            RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction,
            RenderTargetFlags flags);
        extern private void SetRenderTargetColorDepthSubtarget(RenderTargetIdentifier color, RenderTargetIdentifier depth,
            RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction,
            RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction,
            int mipLevel, CubemapFace cubemapFace, int depthSlice);
        extern private void SetRenderTargetMultiSubtarget(RenderTargetIdentifier[] colors, Rendering.RenderTargetIdentifier depth,
            RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions,
            RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction,
            int mipLevel, CubemapFace cubemapFace, int depthSlice);

        [NativeMethod("ProcessVTFeedback")]
        extern private void Internal_ProcessVTFeedback(RenderTargetIdentifier rt, IntPtr resolver, int slice, int x, int width, int y, int height, int mip);

        // Set buffer data.
        [System.Security.SecuritySafeCritical] // due to Marshal.SizeOf
        public void SetBufferData(ComputeBuffer buffer, Array data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (!UnsafeUtility.IsArrayBlittable(data))
            {
                throw new ArgumentException(
                    string.Format("Array passed to RenderingCommandBuffer.SetBufferData(array) must be blittable.\n{0}",
                        UnsafeUtility.GetReasonForArrayNonBlittable(data)));
            }
            InternalSetComputeBufferData(buffer, data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
        }

        // Set buffer data.
        [System.Security.SecuritySafeCritical] // due to Marshal.SizeOf
        public void SetBufferData<T>(ComputeBuffer buffer, List<T> data) where T : struct
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (!UnsafeUtility.IsGenericListBlittable<T>())
            {
                throw new ArgumentException(
                    string.Format("List<{0}> passed to RenderingCommandBuffer.SetBufferData(List<>) must be blittable.\n{1}",
                        typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
            }
            InternalSetComputeBufferData(buffer, NoAllocHelpers.ExtractArrayFromList(data), 0, 0, NoAllocHelpers.SafeLength(data), Marshal.SizeOf(typeof(T)));
        }

        [System.Security.SecuritySafeCritical] // due to Marshal.SizeOf
        public void SetBufferData<T>(ComputeBuffer buffer, NativeArray<T> data) where T : struct
        {
            // Note: no IsBlittable test here because it's already done at NativeArray creation time
            unsafe { InternalSetComputeBufferNativeData(buffer, (IntPtr)data.GetUnsafeReadOnlyPtr(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>()); };
        }

        // Set partial buffer data
        [System.Security.SecuritySafeCritical] // due to Marshal.SizeOf
        public void SetBufferData(ComputeBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (!UnsafeUtility.IsArrayBlittable(data))
            {
                throw new ArgumentException(
                    string.Format("Array passed to RenderingCommandBuffer.SetBufferData(array) must be blittable.\n{0}",
                        UnsafeUtility.GetReasonForArrayNonBlittable(data)));
            }

            if (managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length)
                throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));

            InternalSetComputeBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
        }

        // Set partial buffer data
        [System.Security.SecuritySafeCritical] // due to Marshal.SizeOf
        public void SetBufferData<T>(ComputeBuffer buffer, List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (!UnsafeUtility.IsGenericListBlittable<T>())
            {
                throw new ArgumentException(
                    string.Format("List<{0}> passed to RenderingCommandBuffer.SetBufferData(List<>) must be blittable.\n{1}",
                        typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
            }

            if (managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count)
                throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));

            InternalSetComputeBufferData(buffer, NoAllocHelpers.ExtractArrayFromList(data), managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
        }

        [System.Security.SecuritySafeCritical] // due to Marshal.SizeOf
        public void SetBufferData<T>(ComputeBuffer buffer, NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
        {
            // Note: no IsBlittable test here because it's already done at NativeArray creation time
            if (nativeBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length)
                throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, graphicsBufferStartIndex, count));

            unsafe { InternalSetComputeBufferNativeData(buffer, (IntPtr)data.GetUnsafeReadOnlyPtr(), nativeBufferStartIndex, graphicsBufferStartIndex, count, UnsafeUtility.SizeOf<T>()); };
        }

        public void SetBufferCounterValue(ComputeBuffer buffer, uint counterValue)
        {
            InternalSetComputeBufferCounterValue(buffer, counterValue);
        }

        [FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferNativeData", HasExplicitThis = true, ThrowsException = true)]
        extern private void InternalSetComputeBufferNativeData([NotNull] ComputeBuffer buffer, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

        [FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferData", HasExplicitThis = true, ThrowsException = true)]
        extern void InternalSetComputeBufferData([NotNull] ComputeBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

        [FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferCounterValue", HasExplicitThis = true)]
        extern private void InternalSetComputeBufferCounterValue([NotNull] ComputeBuffer buffer, uint counterValue);

        // Set buffer data.
        [System.Security.SecuritySafeCritical] // due to Marshal.SizeOf
        public void SetBufferData(GraphicsBuffer buffer, Array data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (!UnsafeUtility.IsArrayBlittable(data))
            {
                throw new ArgumentException(
                    string.Format("Array passed to RenderingCommandBuffer.SetBufferData(array) must be blittable.\n{0}",
                        UnsafeUtility.GetReasonForArrayNonBlittable(data)));
            }
            InternalSetGraphicsBufferData(buffer, data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
        }

        // Set buffer data.
        [System.Security.SecuritySafeCritical] // due to Marshal.SizeOf
        public void SetBufferData<T>(GraphicsBuffer buffer, List<T> data) where T : struct
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (!UnsafeUtility.IsGenericListBlittable<T>())
            {
                throw new ArgumentException(
                    string.Format("List<{0}> passed to RenderingCommandBuffer.SetBufferData(List<>) must be blittable.\n{1}",
                        typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
            }
            InternalSetGraphicsBufferData(buffer, NoAllocHelpers.ExtractArrayFromList(data), 0, 0, NoAllocHelpers.SafeLength(data), Marshal.SizeOf(typeof(T)));
        }

        [System.Security.SecuritySafeCritical] // due to Marshal.SizeOf
        public void SetBufferData<T>(GraphicsBuffer buffer, NativeArray<T> data) where T : struct
        {
            // Note: no IsBlittable test here because it's already done at NativeArray creation time
            unsafe { InternalSetGraphicsBufferNativeData(buffer, (IntPtr)data.GetUnsafeReadOnlyPtr(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>()); };
        }

        // Set partial buffer data
        [System.Security.SecuritySafeCritical] // due to Marshal.SizeOf
        public void SetBufferData(GraphicsBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (!UnsafeUtility.IsArrayBlittable(data))
            {
                throw new ArgumentException(
                    string.Format("Array passed to RenderingCommandBuffer.SetBufferData(array) must be blittable.\n{0}",
                        UnsafeUtility.GetReasonForArrayNonBlittable(data)));
            }

            if (managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length)
                throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));

            InternalSetGraphicsBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
        }

        // Set partial buffer data
        [System.Security.SecuritySafeCritical] // due to Marshal.SizeOf
        public void SetBufferData<T>(GraphicsBuffer buffer, List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (!UnsafeUtility.IsGenericListBlittable<T>())
            {
                throw new ArgumentException(
                    string.Format("List<{0}> passed to RenderingCommandBuffer.SetBufferData(List<>) must be blittable.\n{1}",
                        typeof(T), UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
            }

            if (managedBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count)
                throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (managedBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", managedBufferStartIndex, graphicsBufferStartIndex, count));

            InternalSetGraphicsBufferData(buffer, NoAllocHelpers.ExtractArrayFromList(data), managedBufferStartIndex, graphicsBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
        }

        [System.Security.SecuritySafeCritical] // due to Marshal.SizeOf
        public void SetBufferData<T>(GraphicsBuffer buffer, NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
        {
            // Note: no IsBlittable test here because it's already done at NativeArray creation time
            if (nativeBufferStartIndex < 0 || graphicsBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length)
                throw new ArgumentOutOfRangeException(string.Format("Bad indices/count arguments (nativeBufferStartIndex:{0} graphicsBufferStartIndex:{1} count:{2})", nativeBufferStartIndex, graphicsBufferStartIndex, count));

            unsafe { InternalSetGraphicsBufferNativeData(buffer, (IntPtr)data.GetUnsafeReadOnlyPtr(), nativeBufferStartIndex, graphicsBufferStartIndex, count, UnsafeUtility.SizeOf<T>()); };
        }

        public void SetBufferCounterValue(GraphicsBuffer buffer, uint counterValue)
        {
            InternalSetGraphicsBufferCounterValue(buffer, counterValue);
        }

        [FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferNativeData", HasExplicitThis = true, ThrowsException = true)]
        extern private void InternalSetGraphicsBufferNativeData([NotNull] GraphicsBuffer buffer, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

        [System.Security.SecurityCritical] // to prevent accidentally making this public in the future
        [FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferData", HasExplicitThis = true, ThrowsException = true)]
        extern private void InternalSetGraphicsBufferData([NotNull] GraphicsBuffer buffer, Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

        [FreeFunction(Name = "RenderingCommandBuffer_Bindings::InternalSetGraphicsBufferCounterValue", HasExplicitThis = true)]
        extern private void InternalSetGraphicsBufferCounterValue([NotNull] GraphicsBuffer buffer, uint counterValue);

        [FreeFunction(Name = "RenderingCommandBuffer_Bindings::CopyBuffer", HasExplicitThis = true, ThrowsException = true)]
        extern void CopyBufferImpl([NotNull] GraphicsBuffer source, [NotNull] GraphicsBuffer dest);



        [FreeFunction("RenderingCommandBuffer_Bindings::BeginRenderPass", HasExplicitThis = true)]
        extern void BeginRenderPass_Internal(int width, int height, int volumeDepth, int samples, ReadOnlySpan<AttachmentDescriptor> attachments, int depthAttachmentIndex, int shadingRateImageAttachmentIndex, ReadOnlySpan<SubPassDescriptor> subPasses, ReadOnlySpan<byte> debugNameUtf8);

        public void BeginRenderPass(int width, int height, int volumeDepth, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex, NativeArray<SubPassDescriptor> subPasses, ReadOnlySpan<byte> debugNameUtf8)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            BeginRenderPass_Internal(width, height, volumeDepth, samples, attachments, depthAttachmentIndex, -1, subPasses, debugNameUtf8);
        }

        public void BeginRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex, NativeArray<SubPassDescriptor> subPasses, ReadOnlySpan<byte> debugNameUtf8)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            BeginRenderPass_Internal(width, height, 1, samples, attachments, depthAttachmentIndex, -1, subPasses, debugNameUtf8);
        }

        public void BeginRenderPass(int width, int height, int volumeDepth, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex, NativeArray<SubPassDescriptor> subPasses)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            BeginRenderPass_Internal(width, height, volumeDepth, samples, attachments, depthAttachmentIndex, -1, subPasses, new ReadOnlySpan<byte>());
        }

        public void BeginRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex, NativeArray<SubPassDescriptor> subPasses)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            BeginRenderPass_Internal(width, height, 1, samples, attachments, depthAttachmentIndex, -1, subPasses, new ReadOnlySpan<byte>());
        }

        public void BeginRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex, int shadingRateImageAttachmentIndex, NativeArray<SubPassDescriptor> subPasses)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            BeginRenderPass_Internal(width, height, 1, samples, attachments, depthAttachmentIndex, shadingRateImageAttachmentIndex, subPasses, new ReadOnlySpan<byte>());
        }

        public void BeginRenderPass(int width, int height, int volumeDepth, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex, int shadingRateImageAttachmentIndex, NativeArray<SubPassDescriptor> subPasses)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            BeginRenderPass_Internal(width, height, volumeDepth, samples, attachments, depthAttachmentIndex, shadingRateImageAttachmentIndex, subPasses, new ReadOnlySpan<byte>());
        }

        public void BeginRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex, int shadingRateImageAttachmentIndex, NativeArray<SubPassDescriptor> subPasses, ReadOnlySpan<byte> debugNameUtf8)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            BeginRenderPass_Internal(width, height, 1, samples, attachments, depthAttachmentIndex, shadingRateImageAttachmentIndex, subPasses, debugNameUtf8);
        }

        public void BeginRenderPass(int width, int height, int volumeDepth, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex, int shadingRateImageAttachmentIndex, NativeArray<SubPassDescriptor> subPasses, ReadOnlySpan<byte> debugNameUtf8)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            BeginRenderPass_Internal(width, height, volumeDepth, samples, attachments, depthAttachmentIndex, shadingRateImageAttachmentIndex, subPasses, debugNameUtf8);
        }

        [FreeFunction("RenderingCommandBuffer_Bindings::NextSubPass", HasExplicitThis = true)]
        extern void NextSubPass_Internal();
        public void NextSubPass()
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            NextSubPass_Internal();
        }

        [FreeFunction("RenderingCommandBuffer_Bindings::EndRenderPass", HasExplicitThis = true)]
        extern void EndRenderPass_Internal();
        public void EndRenderPass()
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            EndRenderPass_Internal();
        }

        [FreeFunction("RenderingCommandBuffer_Bindings::SetupCameraProperties", HasExplicitThis = true)]
        extern void SetupCameraProperties_Internal([NotNull] Camera camera);
        public void SetupCameraProperties(Camera camera)
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            SetupCameraProperties_Internal(camera);
        }

        [FreeFunction("RenderingCommandBuffer_Bindings::InvokeOnRenderObjectCallbacks", HasExplicitThis = true)]
        extern void InvokeOnRenderObjectCallbacks_Internal();
        public void InvokeOnRenderObjectCallbacks()
        {
            ValidateAgainstExecutionFlags(CommandBufferExecutionFlags.None, CommandBufferExecutionFlags.AsyncCompute);
            InvokeOnRenderObjectCallbacks_Internal();
        }

        internal static class BindingsMarshaller
        {
            public static CommandBuffer ConvertToManaged(IntPtr ptr) => new CommandBuffer(ptr);
            public static IntPtr ConvertToNative(CommandBuffer commandBuffer) => commandBuffer.m_Ptr;
        }

        /// <summary>
        /// Set base shading rate fragment size.
        /// </summary>
        /// <param name="shadingRateFragmentSize">Shading rate fragment size</param>
        public void SetShadingRateFragmentSize(ShadingRateFragmentSize shadingRateFragmentSize)
        {
            SetShadingRateFragmentSize_Impl(shadingRateFragmentSize);
        }

        /// <summary>
        /// Set shading rate combiners
        /// </summary>
        /// <param name="shadingRateCombiner">Shading rate combiners</param>
        public void SetShadingRateCombiner(ShadingRateCombinerStage stage, ShadingRateCombiner combiner)
        {
            SetShadingRateCombiner_Impl(stage, combiner);
        }

        /// <summary>
        /// Set shading rate image
        /// </summary>
        /// <param name="RenderTargetIdentifier">Shading rate image to set</param>
        public void SetShadingRateImage(in RenderTargetIdentifier shadingRateImage)
        {
            SetShadingRateImage_Impl(shadingRateImage);
        }

        /// <summary>
        /// Convenience function to reset shading rate state to default values.
        /// </summary>
        /// <returns></returns>
        public void ResetShadingRate()
        {
            ResetShadingRate_Impl();
        }

        [FreeFunction("RenderingCommandBuffer_Bindings::SetShadingRateFragmentSize_Impl", HasExplicitThis = true)]
        extern private void SetShadingRateFragmentSize_Impl(ShadingRateFragmentSize shadingRateFragmentSize);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetShadingRateCombiner_Impl", HasExplicitThis = true)]
        extern private void SetShadingRateCombiner_Impl(ShadingRateCombinerStage stage, ShadingRateCombiner combiner);

        [FreeFunction("RenderingCommandBuffer_Bindings::SetShadingRateImage_Impl", HasExplicitThis = true)]
        extern private void SetShadingRateImage_Impl(in RenderTargetIdentifier shadingRateImage);

        [FreeFunction("RenderingCommandBuffer_Bindings::ResetShadingRate_Impl", HasExplicitThis = true)]
        extern private void ResetShadingRate_Impl();
    }
}
