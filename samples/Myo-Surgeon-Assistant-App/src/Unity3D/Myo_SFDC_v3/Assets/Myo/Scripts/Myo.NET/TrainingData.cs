using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Thalmic.Myo
{
    internal class TrainingData
    {
        private IntPtr _handle;
        private Myo _myo;

        internal TrainingData(Myo myo)
        {
            _myo = myo;
            //TODO/NOTE: leak this until we move ownership of the training data to libmyo's Myo
            if (libmyo.training_create_dataset(_myo.Handle, out _handle, IntPtr.Zero) != libmyo.Result.Success)
            {
                throw new InvalidOperationException("Unable to create training data.");
            }
        }

        internal event EventHandler<TrainingEventArgs> CollectionProgress;

        internal void Collect(Pose pose)
        {
            GCHandle gch = GCHandle.Alloc(this);
            try
            {
                // Note: libmyo requires us to stop the hub's thread while collecting
                _myo.Hub.StopEventThread();
                libmyo.training_collect_data(_handle, (libmyo.PoseType)pose, (libmyo.TrainingCollectStatus)HandleTrainingData, (IntPtr)gch, IntPtr.Zero);
            }
            finally
            {
                gch.Free();
                _myo.Hub.StartEventThread();
            }
        }

        internal void Train()
        {
            libmyo.training_train_from_dataset(_handle, IntPtr.Zero);
        }

        internal void Annotate(string name, string value)
        {
            libmyo.training_annotate_training_data(_handle, name, value, IntPtr.Zero);
        }

        internal void Send()
        {
            libmyo.training_send_training_data(_handle, IntPtr.Zero);
        }

        private static libmyo.HandlerResult HandleTrainingData(IntPtr userData, byte data, byte progress)
        {
            GCHandle handle = (GCHandle)userData;
            TrainingData self = (TrainingData)handle.Target;
            if (self.CollectionProgress != null)
            {
                self.CollectionProgress(self, new TrainingEventArgs(self._myo, DateTime.Now, data, progress));
            }
            return libmyo.HandlerResult.Continue;
        }
    }
}
