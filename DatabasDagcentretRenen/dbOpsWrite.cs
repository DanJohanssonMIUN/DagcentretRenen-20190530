using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Configuration;
using System.Windows;

namespace DatabasDagcentretRenen
{
    class dbOpsWrite
    {



        public void AddPresence(int serial, int child_id, string action, string date)
        {
            string time = DateTime.Now.ToString("HH:mm");
            string thisdate = "'" + date + " " + time + "'";
            string stmt = "UPDATE presence SET action = " + action + " WHERE serial_ID = " + serial;


            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = stmt;
                    cmd.ExecuteNonQuery();
                }


            }

        }


        public void AddSchedule(int gId, int cId, string date, string startTime, string endTime, bool breakfast)
        {
            DateTime realDate = DateTime.Parse(date);

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO schedule (person_id, child_id, presence, start_date, start_time, end_time, breakfast) VALUES " +
                        "(@person_id, @child_id, @presence, @start_date, @start_time, @end_time, @breakfast)";
                    cmd.Parameters.AddWithValue("person_id", gId);
                    cmd.Parameters.AddWithValue("child_id", cId);
                    cmd.Parameters.AddWithValue("start_date", realDate);
                    cmd.Parameters.AddWithValue("start_time", startTime);
                    cmd.Parameters.AddWithValue("end_time", endTime);
                    cmd.Parameters.AddWithValue("breakfast", breakfast);
                    cmd.Parameters.AddWithValue("presence", true);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Presence(int cId, string action, int serId)
        {

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();


                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO presence (child_id, action, serial_id) VALUES " +
                        "(@child_id, @action, @serial_id)";
                    cmd.Parameters.AddWithValue("child_id", cId);
                    cmd.Parameters.AddWithValue("action", action);
                    cmd.Parameters.AddWithValue("serial_id", serId);
                    cmd.ExecuteNonQuery();
                }





            }
        }




    }
}
