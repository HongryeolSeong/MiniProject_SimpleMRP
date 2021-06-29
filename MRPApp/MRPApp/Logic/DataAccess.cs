using MRPApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRPApp.Logic
{
    public class DataAccess
    {
        // Setting테이블에서 데이터 가져오기
        public static List<Settings> GetSettings()
        {
            List<Model.Settings> list;

            // 이렇게 해도 됨
            //var ctx = new MRPEntities();
            //settings = ctx.Settings.ToList();

            using (var ctx = new MRPEntities())
            {
                list = ctx.Settings.ToList();
            }

            return list;
        }

        internal static int SetSettings(Settings item)
        {
            using (var ctx = new MRPEntities())
            {
                ctx.Settings.AddOrUpdate(item); // INSERT or UPDATE
                return ctx.SaveChanges();       // COMMIT
            }
        }

        internal static int DelSettings(Settings item)
        {
            using (var ctx = new MRPEntities())
            {
                var obj = ctx.Settings.Find(item.BasicCode);
                ctx.Settings.Remove(obj);      // DELETE
                return ctx.SaveChanges();
            }
        }

        internal static List<Schedules> GetSchedules()
        {
            List<Model.Schedules> list;

            // 이렇게 해도 됨
            //var ctx = new MRPEntities();
            //settings = ctx.Settings.ToList();

            using (var ctx = new MRPEntities())
            {
                list = ctx.Schedules.ToList();
            }

            return list;
        }

        internal static int SetSchedule(Schedules item)
        {
            using (var ctx = new MRPEntities())
            {
                ctx.Schedules.AddOrUpdate(item);    // INSERT or UPDATE
                return ctx.SaveChanges();           // COMMIT
            }
        }

        internal static List<Process> GetProcesses()
        {
            List<Model.Process> list;

            using (var ctx = new MRPEntities())
            {
                list = ctx.Process.ToList(); // SELECT
            }

            return list;
        }

        internal static int SetProcess(Process item)
        {
            using (var ctx = new MRPEntities())
            {
                ctx.Process.AddOrUpdate(item);  // INSERT | UPDATE
                return ctx.SaveChanges();       // COMMIT
            }
        }
    }
}
