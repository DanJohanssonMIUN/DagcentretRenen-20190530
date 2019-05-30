using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Configuration;


namespace DatabasDagcentretRenen
{
    class dbOpsRead
    {
        public List <Klasser_tabeller.Schedule> GetSchedule()
        {
            Klasser_tabeller.Schedule s = new Klasser_tabeller.Schedule();
            List<Klasser_tabeller.Schedule> schema = new List<Klasser_tabeller.Schedule>();
            string stmt = "SELECT serial_id FROM schedule";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            s = new Klasser_tabeller.Schedule()
                            {
                                Serial_nr = (reader.GetInt32(0))
                            }; schema.Add(s);
                        }
                    }
                }
            }
            return schema;

        }

        public List <Klasser_tabeller.KidsSchedule> GetSchedule(int cId)
        {
            Klasser_tabeller.KidsSchedule k = new Klasser_tabeller.KidsSchedule();
            List<Klasser_tabeller.KidsSchedule> schedules = new List<Klasser_tabeller.KidsSchedule>();
            string stmt = "select schedule.start_date, schedule.type_of_absence, schedule.start_time, schedule.end_time, room.name, presence.action " +
                          "from(((schedule " +
                          "inner join child ON child.child_id = schedule.child_id) " +
                          "inner join room on room.room_id = child.room_id) " +
                          "inner join presence on child.child_id = presence.child_id) " +
	                      "where child.child_id = " + cId +
                          "order by start_date asc";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {

                    using (var reader = cmd.ExecuteReader())
                    {


                        while (reader.Read())
                        {
                            k = new Klasser_tabeller.KidsSchedule()
                            {
                                StartDate = (reader.GetValue(0).ToString()),
                                TypeOfAbsence = (reader.GetValue(1).ToString()),
                                StartTime = (reader.GetValue(2).ToString()),
                                EndTime = (reader.GetValue(3).ToString()),
                                RoomName = (reader.GetValue(4).ToString()),
                                Action = (reader.GetValue(5).ToString())

                            }; schedules.Add(k);
                        }

                    }
                    return schedules;
                }


            }




        }

        public List<Klasser_tabeller.Person> GetPersons()
        {
            Klasser_tabeller.Person p;
            List<Klasser_tabeller.Person> people = new List<Klasser_tabeller.Person>();

            string stmt = "SELECT * FROM person";

            
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            p = new Klasser_tabeller.Person()
                            {
                                Person_ID = (reader.GetInt32(0)),
                                First_name = (reader.GetString(3)),
                                Last_name = (reader.GetString(2)),
                                Phone_nr = (reader.GetString(1))

                            };people.Add(p);
                        }
                    }
                }
            }

            return people;
        }


        // Metod för att hämta tabellen "staff" ur databas och skapa lista av den.
        public List<Klasser_tabeller.Staff> GetStaff()
        {
            Klasser_tabeller.Staff s;
            List<Klasser_tabeller.Staff> staff = new List<Klasser_tabeller.Staff>();

            string stmt = "SELECT * FROM staff";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {

                

                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            s = new Klasser_tabeller.Staff()
                            {
                                Staff_id = (reader.GetInt32(0)),
                                PhoneNr = (reader.GetString(1)),
                                FirstName = (reader.GetString(2)),
                                LastName = (reader.GetString(3))

                            }; staff.Add(s);
                        }
                    }

                }
            }

            return staff;
        }



        //Metod för att hämta tabellen Child ur databasen och presentera den i en lista
        public List<Klasser_tabeller.GuardiansChild> GetChildren(int id)
        {
            Klasser_tabeller.GuardiansChild c;
            List<Klasser_tabeller.GuardiansChild> children = new List<Klasser_tabeller.GuardiansChild>();

            string stmt = "SELECT child.first_name, child.child_id FROM child INNER JOIN guardian ON guardian.child_id = child.child_id WHERE guardian.person_id = " + id;

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            c = new Klasser_tabeller.GuardiansChild()
                            {
                                First_name = (reader.GetString(0)),
                                Child_ID = (reader.GetInt32(1))
                            }; children.Add(c);
                        }
                    }

                }
            }
            return children;
        }

        //Metod för att Innerjoina massa skit så att personal ser vilka barn den ansvarar för idag, och vilka av de som är närvarande

        public List<Klasser_tabeller.PresentChildrenForStaff> GetTodaysKids(int id, string date)
        {
           
            
            Klasser_tabeller.PresentChildrenForStaff p = new Klasser_tabeller.PresentChildrenForStaff();
            List<Klasser_tabeller.PresentChildrenForStaff> kids = new List<Klasser_tabeller.PresentChildrenForStaff>();
            string stmt = "";

            if (id == 0)
            {
                stmt = "SELECT distinct schedule.serial_id,  child.first_name, child.last_name, room.name, child.child_id, schedule.breakfast, schedule.start_time, schedule.end_time, presence.action, schedule.serial_id " +
                "FROM((((room " +
                "INNER JOIN child ON room.room_id = child.room_id) " +
                "INNER JOIN staff ON room.room_id = staff.room_id) " +
                "INNER JOIN schedule on child.child_id = schedule.child_id) " +
                "INNER JOIN presence on presence.child_id = child.child_id) " +
                "where schedule.presence = true AND date(schedule.start_date) = '" + date + "' AND date(presence.date_time) = '" + date + "'";
            }
            else
            {
                stmt = "SELECT schedule.serial_id,  child.first_name, child.last_name, room.name, child.child_id, schedule.breakfast, schedule.start_time, schedule.end_time, presence.action, schedule.serial_id " +
                "FROM((((room " +
                "INNER JOIN child ON room.room_id = child.room_id) " +
                "INNER JOIN staff ON room.room_id = staff.room_id) " +
                "INNER JOIN schedule on child.child_id = schedule.child_id) " +
                "INNER JOIN presence on presence.child_id = child.child_id) " +
                "where schedule.presence = true and staff.staff_id = " + id + " and date(schedule.start_date) = '" + date + "' AND date(presence.date_time) = '" + date + "'";
            }
            

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Connection = conn;
                    cmd.CommandText = stmt;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            p = new Klasser_tabeller.PresentChildrenForStaff()
                            {
                                FirstName = (reader.GetString(1)),
                                LastName = (reader.GetString(2)),
                                Section = (reader.GetString(3)),
                                Child_ID = (reader.GetInt32(4)),
                                Breakfast = (reader.GetBoolean(5)),
                                StartTime = (reader.GetValue(6).ToString()),
                                EndTime = (reader.GetValue(7).ToString()),
                                Action = (reader.GetValue(8).ToString()),
                                Serial_id = (reader.GetInt32(9))
                            }; kids.Add(p);
                        }
                    }

                }
            }

            return kids;
        }

        public List<string> AllergyList (string date)
        {
            List<string> allergyList = new List<string>();

            string stmt = "select child.first_name, child.last_name, child.allergy " +
            "from child " +
            "inner join schedule on child.child_id = schedule.child_ID " +
            "where allergy is not null AND schedule.start_date = '" + date + "'";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Connection = conn;
                    cmd.CommandText = stmt;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allergyList.Add((reader.GetString(0)) + " " + (reader.GetString(1)) + " är allergisk mot " + reader.GetString(2));
                                //(reader.GetString(0)) (reader.GetString(1)) (reader.GetString(2));
                        }
                    }
                }
            }

            return allergyList;

        }

        public int NumberOfBreakfasts (string date)
        {
            int breakfastNumber = 0;
            string stmt = "SELECT COUNT (breakfast) FROM schedule WHERE start_date = '" + date + "' AND breakfast = true";

            Klasser_tabeller.PresentChildrenForStaff p = new Klasser_tabeller.PresentChildrenForStaff();
            List<Klasser_tabeller.PresentChildrenForStaff> breakfastKids = new List<Klasser_tabeller.PresentChildrenForStaff>();

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Connection = conn;
                    cmd.CommandText = stmt;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            breakfastNumber = (reader.GetInt32(0));
                        }
                    }
                }
            }

            return breakfastNumber;
        }

        public List<Klasser_tabeller.Child> GetChildInfo(Klasser_tabeller.PresentChildrenForStaff activePc)
        {
            Klasser_tabeller.Child s;
            List<Klasser_tabeller.Child> children = new List<Klasser_tabeller.Child>();
            string stmt = "SELECT child.first_name, child.last_name, child.special_needs, child.allergy, child.walk_alone FROM child " +
                "WHERE child_id = " + activePc.Child_ID;


            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Connection = conn;
                    cmd.CommandText = stmt;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            s = new Klasser_tabeller.Child()
                            {

                                First_name = (reader.GetString(0)),
                                Last_name = (reader.GetString(1)),
                                Special_needs = (reader.GetValue(2).ToString()),
                                Allergy = (reader.GetValue(3).ToString()),
                                Walk_alone = (reader.GetBoolean(4))


                            }; children.Add(s);
                        }
                    }
                }
            }

            return children;
        }
        public List<Klasser_tabeller.Guardian> GetGuardianInfo(Klasser_tabeller.PresentChildrenForStaff activePc)
        {
            Klasser_tabeller.Guardian g;
            List<Klasser_tabeller.Guardian> Guardians = new List<Klasser_tabeller.Guardian>();
            string stmt = "SELECT  person.first_name, person.last_name, person.phone_nr FROM((guardian " +
            "INNER JOIN child ON guardian.child_ID = child.Child_ID) " +
            "INNER JOIN person ON guardian.person_ID = person.person_ID) " +
            "where child.child_id = '" + activePc.Child_ID + "'";



            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Connection = conn;
                    cmd.CommandText = stmt;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            g = new Klasser_tabeller.Guardian()
                            {

                                First_name = (reader.GetString(0)),
                                Last_name = (reader.GetString(1)),
                                Phone_nr = (reader.GetString(2)),
                               


                            }; Guardians.Add(g);
                        }
                    }
                }
            }

            return Guardians;

        }
        public List<Klasser_tabeller.Getter> GetGetterInfo(Klasser_tabeller.PresentChildrenForStaff activePc)
        {
            Klasser_tabeller.Getter g;
            List<Klasser_tabeller.Getter> Getters = new List<Klasser_tabeller.Getter>();
            string stmt = "SELECT  person.first_name, person.last_name, person.phone_nr FROM((getter " +
            "INNER JOIN child ON getter.child_ID = child.Child_ID) " +
            "INNER JOIN person ON getter.person_ID = person.person_ID) " +
            "where child.child_id = '" + activePc.Child_ID + "'";



            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Connection = conn;
                    cmd.CommandText = stmt;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            g = new Klasser_tabeller.Getter()
                            {

                                First_name = (reader.GetString(0)),
                                Last_name = (reader.GetString(1)),
                                Phone_nr = (reader.GetString(2)),



                            }; Getters.Add(g);
                        }
                    }
                }
            }

            return Getters;

        }
    }
}
