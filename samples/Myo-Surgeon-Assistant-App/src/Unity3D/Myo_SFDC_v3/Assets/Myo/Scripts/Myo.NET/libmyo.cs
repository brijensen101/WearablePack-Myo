using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Thalmic.Myo
{
    internal static class libmyo
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        private const string MYO_DLL = "myo";
#elif UNITY_ANDROID
        private const string MYO_DLL = "myo-android";
#elif WIN64
        private const string MYO_DLL = "myo64.dll";
#elif WIN32
        private const string MYO_DLL = "myo32.dll";
#endif

        public enum Result
        {
            Success,
            Error,
            ErrorInvalidArgument,
            ErrorRuntime
        }

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_error_cstring",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern string error_cstring(IntPtr error);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_error_kind",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result error_kind(IntPtr error);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_free_error_details",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern void free_error_details(IntPtr error);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_init_hub",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result init_hub(out IntPtr hub, IntPtr error);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_shutdown_hub",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result shutdown_hub(IntPtr hub, IntPtr error);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_get_mac_address",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 get_mac_address(IntPtr myo);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_string_to_mac_address",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 string_to_mac_address(string addressAsString);

        public enum VibrationType
        {
            Short,
            Medium,
            Long
        }

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_vibrate",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern void vibrate(IntPtr myo, VibrationType type, IntPtr error);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_request_rssi",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern void request_rssi(IntPtr myo, IntPtr error);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_pair_any",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result pair_any(IntPtr hub, uint count, IntPtr error);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_pair_by_mac_address",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result pair_by_mac_address(IntPtr hub, UInt64 macAddress, IntPtr error);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_pair_adjacent",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result pair_adjacent(IntPtr hub, uint count, IntPtr error);

        public enum PoseType
        {
            Rest,
            Fist,
            WaveIn,
            WaveOut,
            FingersSpread,
            TwistIn,
            NumPoses,
            Unknown = 0xffff
        }

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_training_is_available",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern bool training_is_available(IntPtr myo);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_training_create_dataset",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result training_create_dataset(IntPtr hub, out IntPtr dataset, IntPtr error);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate HandlerResult TrainingCollectStatus(IntPtr userData, byte value, byte progress);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_training_collect_data",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result training_collect_data(IntPtr dataset, PoseType pose, libmyo.TrainingCollectStatus callback, IntPtr userData, IntPtr error);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_training_train_from_dataset",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result training_train_from_dataset(IntPtr dataset, IntPtr error);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_training_free_dataset",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result training_free_dataset(IntPtr dataset);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_training_load_profile",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result training_load_profile(IntPtr myo, string filename, IntPtr error);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_training_store_profile",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result training_store_profile(IntPtr myo, IntPtr filename, IntPtr error);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_training_send_training_data",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result training_send_training_data(IntPtr dataset, IntPtr error);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_training_annotate_training_data",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result training_annotate_training_data(IntPtr dataset, string name, string value, IntPtr error);

        public enum EventType
        {
            Paired,
            Connected,
            Disconnected,
            ArmRecognized,
            ArmLost,
            Orientation,
            Pose,
            Rssi
        }

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_event_get_type",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern EventType event_get_type(IntPtr evt);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_event_get_timestamp",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 event_get_timestamp(IntPtr evt);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_now",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 now();

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_event_get_myo",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr event_get_myo(IntPtr evt);

        public enum VersionComponent
        {
            Major,
            Minor,
            Patch
        }

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_event_get_firmware_version",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern uint event_get_firmware_version(IntPtr evt, VersionComponent component);

        public enum OrientationIndex
        {
            X = 0,
            Y = 1,
            Z = 2,
            W = 3
        }

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_event_get_orientation",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern float event_get_orientation(IntPtr evt, OrientationIndex index);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_event_get_accelerometer",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern float event_get_accelerometer(IntPtr evt, uint index);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_event_get_gyroscope",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern float event_get_gyroscope(IntPtr evt, uint index);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_event_get_pose",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern PoseType event_get_pose(IntPtr evt);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_event_get_rssi",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern sbyte event_get_rssi(IntPtr evt);

        public enum HandlerResult
        {
            Continue,
            Stop
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate HandlerResult Handler(IntPtr userData, IntPtr evt);

        [DllImport(MYO_DLL,
                   EntryPoint = "libmyo_run",
                   CallingConvention = CallingConvention.Cdecl)]
        public static extern Result run(IntPtr hub, uint durationMs, Handler handler, IntPtr userData, IntPtr error);
    }
}
