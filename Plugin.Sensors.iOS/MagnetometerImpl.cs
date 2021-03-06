﻿using System;
using CoreMotion;
using Foundation;


namespace Plugin.Sensors
{
    public class MagnetometerImpl : AbstractSensor, IMagnetometer
    {
        protected override bool IsSensorAvailable(CMMotionManager mgr)
        {
            return mgr.MagnetometerAvailable;
        }


        protected override void Start(CMMotionManager mgr, IObserver<MotionReading> ob)
        {
            mgr.StartMagnetometerUpdates(NSOperationQueue.CurrentQueue, (data, err) =>
                ob.OnNext(new MotionReading(data.MagneticField.X, data.MagneticField.Y, data.MagneticField.Z))
            );
        }


        protected override void Stop(CMMotionManager mgr)
        {
            mgr.StopMagnetometerUpdates();
        }


        protected override void SetReportInterval(CMMotionManager mgr, TimeSpan timeSpan)
        {
            mgr.MagnetometerUpdateInterval = timeSpan.TotalSeconds;
        }
    }
}
