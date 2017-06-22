using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebJobManager.job;

namespace WebJobManager
{
    /// <summary>
    /// 调度任务管理器
    /// </summary>
    public class JobManager
    {
        private static JobManager _instance = new JobManager();

        /// <summary>
        ///调度器
        /// </summary>
        IScheduler _sc;
        public static JobManager Instance
        {
            get
            {
                return _instance;
            }
        }
        private JobManager()
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            _sc = factory.GetScheduler();
            Init();
        }
        public void Init()
        {
            var checkJob = new JobDetailImpl("MonitorJob", typeof(QiDianMonitorJob));
            //每隔30分钟检查一次
            var trigger = SimpleScheduleBuilder.RepeatMinutelyForever(1).Build();
            trigger.Key = new TriggerKey("MonitorTrigger");
            _sc.ScheduleJob(checkJob, trigger);
        }
        /// <summary>
        /// 开始服务
        /// </summary>
        public void StartJobServer()
        {
            _sc.Start();
        }
        /// <summary>
        /// 关闭服务
        /// </summary>
        public void EndJobServier()
        {
            _sc.Shutdown(true);
        }
    }
}
