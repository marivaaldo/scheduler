﻿using Scheduler.Jobs.Domain;
using Scheduler.Jobs.Sample.Domain.Enums;
using System.Runtime.ExceptionServices;

namespace Scheduler.Jobs.Sample.Infrastructure.Jobs
{
    public abstract class BaseJob : BaseJob<string>
    {
        public override void Execute(string data)
        {
            Execute();
        }

        public abstract void Execute();
    }

    public abstract class BaseJob<D> : IJob<D>
        where D : class
    {
        public string JobId { get; set; }

        public IJobConsole Console { get; set; }

        public string Queue => JobQueue.ToString();

        public abstract JobQueue JobQueue { get; }

        public void Execute(object data)
        {
            try
            {
                Execute((D)data);
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
        }

        public abstract void Execute(D data);
    }
}