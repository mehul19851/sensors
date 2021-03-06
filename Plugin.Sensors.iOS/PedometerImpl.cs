﻿using System;
using System.Reactive.Linq;
using CoreMotion;
using Foundation;


namespace Plugin.Sensors
{
    public class PedometerImpl : IPedometer
    {
        public IObservable<bool> IsAvailable()
        {
            return Observable.Return(CMStepCounter.IsStepCountingAvailable);
        }


        IObservable<int> stepOb;
        public IObservable<int> WhenReadingTaken()
        {
            this.stepOb = this.stepOb ?? Observable.Create<int>(ob =>
            {
                var scm = new CMStepCounter();
                scm.StartStepCountingUpdates(NSOperationQueue.CurrentQueue, 10, (steps, timestamp, error) =>
                {

                });
                return () =>
                {
                    scm.StopStepCountingUpdates();
                    scm.Dispose();
                };
            });
            return this.stepOb;
        }
    }
}
