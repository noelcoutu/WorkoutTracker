using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Data.SQLite;
using Extenders;

namespace Models
{
    public static class DB
    {
        /*

        Module Name: DB

        Descriptiopn:   This static class contains ALL database queries and several helper methods used for the encryption of passwords.  
                        Several queries have been pre-written and commented out in anticipation of use in future functionality.

        Fields:     string ConnectionString

        Programmer(s)'s Names:  Chadwick Mayer
                                Dylan Gerace
                                Dustin Schlatter
                                Noel Coutu
                                Salman Almerekhi

        Date Written: 01 Aug 2021

        Version Number 4.0

        */

        //The string of the database's path
        private static string ConnectionString = "Data Source="+ @"C:\Users\Chad Mayer\Documents\GitHub\FitnessTracker\Databases\WorkoutTrackerDB.s3db";


        private static int ExecuteWrite(string query, Dictionary<string, object> args)
        {
            //This helper method is used to actually execute the query and returns the number of rows written to. 
            //args: The dicitonary of the string parameters and their associated values
            //query: A string of the SQL query to be executed

            int numberOfRowsAffected;

            //setup the connection to the database
            using (var con = new SQLiteConnection(ConnectionString))
            {
                con.Open();

                //open a new command
                using (var cmd = new SQLiteCommand(query, con))
                {
                    //set the arguments given in the query
                    foreach (var pair in args)
                    {
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                    }

                    //execute the query and get the number of row affected
                    numberOfRowsAffected = cmd.ExecuteNonQuery();
                }

                return numberOfRowsAffected;
            }
        }

        private static DataTable ExecuteRead(string query, Dictionary<string, object> args)
        {
            //This helper method is used to actually execute the query and returns the results as a DataTable. 
            //args: The dicitonary of the string parameters and their associated values
            //query: A string of the SQL query to be executed

            if (string.IsNullOrEmpty(query.Trim()))
                return null;
            try
            {
                using (var con = new SQLiteConnection(ConnectionString))
                {
                    con.Open();

                    using (var cmd = new SQLiteCommand(query, con))
                    {
                        foreach (KeyValuePair<string, object> entry in args)
                        {
                            cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                        }

                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);

                        DataTable dt = new DataTable();

                        da.Fill(dt);

                        da.Dispose();

                        return dt;
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return new DataTable();
        }

        public static string GetHash(string input)
        {
            //Used to hash the string input using the SHA 256-bit algorithm. This is exclusively used for hashing passwords.
            //Returns the hashed string.
            //input: the stgring to be hashed.

            SHA256 sha256Hash = SHA256.Create();

            //Convert the input string to a byte array and compute the hash.
            byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            //Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            //Loop through each byte of the hashed data
            //and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            //Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static bool VerifyHash(string input, string hash)
        {
            //Verifies an input string against an already generated hash. 
            //Returns truw if they match and false if they do not.
            //Input: the string that needs to be hashed
            //hash: the already hashed string that is being compared.

            //Hash the input.
           var hashOfInput = GetHash(input);

            //Create a StringComparer an compare the hashes.
           StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }

    /*----------------------------------User Queries----------------------------------*/
        public static int AddUser(User user)
        {
            const string query = "INSERT INTO Users(First_Name, Last_Name, Username, Email, User_PW, Height, Weight, Weight_Goal, Is_Metric, Birthdate) " +
                "VALUES(@firstName, @lastName, @username, @email, @password, @height, @weight, @weight_goal, @isMetric, @birthdate)";

            //here we are setting the parameter values that will be actually
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                {"@firstName", user.FirstName},
                {"@lastName", user.LastName},
                {"@username", user.UserName},
                {"@email", user.Email},
                {"@password", user.Password},
                {"@height", user.Height},
                {"@weight", user.Weight },
                {"@weight_goal", user.WeightGoal },
                {"@isMetric", user.isMetric },
                {"@birthdate", user.Birthdate }
            };

            return ExecuteWrite(query, args);
        }

        public static int EditUser(User user)
        {
            const string query = "UPDATE Users SET First_Name = @firstName, Last_Name = @lastName, Email = @email, " +
                "height = @height, weight = @weight, weight_Goal = @weightGoal, Birthdate = @birthdate WHERE User_ID = @id";

            //here we are setting the parameter values that will be actually
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                {"@id", user.UserID},
                {"@firstName", user.FirstName},
                {"@lastName", user.LastName},
                {"@email", user.Email},
                {"@height", user.Height},
                {"@weight", user.Weight},
                {"@weightGoal", user.WeightGoal},
                {"@birthdate", user.Birthdate }
            };

            return ExecuteWrite(query, args);
        }

        public static int DeleteUser(User user)
        {
            const string query = "Delete from Users WHERE User_ID = @id";

            //here we are setting the parameter values that will be actually
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                {"@id", user.UserID}
            };

            return ExecuteWrite(query, args);
        }

        public static User GetUserById(int id)
        {
            var query = "SELECT * FROM Users WHERE User_ID = @id";

            var args = new Dictionary<string, object>
            {
                {"@id", id}
            };

            DataTable dt = ExecuteRead(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            var user = new User();

            user.UserID = id;
            user.FirstName = Convert.ToString(dt.Rows[0]["First_Name"]);
            user.LastName = Convert.ToString(dt.Rows[0]["Last_Name"]);
            user.Email = Convert.ToString(dt.Rows[0]["Email"]);
            user.Height = Convert.ToDouble(dt.Rows[0]["Height"]);
            user.Weight = Convert.ToDouble(dt.Rows[0]["Weight"]);
            user.WeightGoal = Convert.ToDouble(dt.Rows[0]["Weight_Goal"]);
            user.isMetric = Convert.ToBoolean(dt.Rows[0]["Is_Metric"]);
            user.Password = Convert.ToString(dt.Rows[0]["User_PW"]);
            user.UserName = Convert.ToString(dt.Rows[0]["Username"]);
            user.Birthdate = Convert.ToDateTime(dt.Rows[0]["Birthdate"]);

            return user;
        }

        public static User GetUserByUserName(string username)
        {
            var query = "SELECT * FROM Users WHERE Username = @username";

            var args = new Dictionary<string, object>
            {
                {"@username", username}
            };

            DataTable dt = ExecuteRead(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            var user = new User();
            user.UserID = Convert.ToInt32(dt.Rows[0]["User_Id"]??"0");
            user.FirstName = Convert.ToString(dt.Rows[0]["First_Name"]);
            user.LastName = Convert.ToString(dt.Rows[0]["Last_Name"]);
            user.Email = Convert.ToString(dt.Rows[0]["Email"]);
            user.UserName = username;
            user.Height = Convert.ToDouble(dt.Rows[0]["Height"]??"0");
            user.Weight = Convert.ToDouble(dt.Rows[0]["Weight"] ?? "0");
            user.WeightGoal = Convert.ToDouble(dt.Rows[0]["Weight_Goal"] ?? "0");
            user.isMetric = Convert.ToBoolean(dt.Rows[0]["Is_Metric"]??false);
            user.Password = Convert.ToString(dt.Rows[0]["User_PW"]);
            user.Birthdate = Convert.ToDateTime(dt.Rows[0]["Birthdate"]??"0");

            return user;
        }

        public static User GetUserByEmail(string email)
        {
            var query = "SELECT * FROM Users WHERE Email = @email";

            var args = new Dictionary<string, object>
            {
                {"@email", email}
            };

            DataTable dt = ExecuteRead(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            var user = new User();
            user.UserID = Convert.ToInt32(dt.Rows[0]["User_Id"]);
            user.FirstName = Convert.ToString(dt.Rows[0]["First_Name"]);
            user.LastName = Convert.ToString(dt.Rows[0]["Last_Name"]);
            user.UserName = Convert.ToString(dt.Rows[0]["Username"]);
            user.Email = email;
            user.Height = Convert.ToDouble(dt.Rows[0]["Height"]);
            user.Weight = Convert.ToDouble(dt.Rows[0]["Weight"]);
            user.WeightGoal = Convert.ToDouble(dt.Rows[0]["Weight_Goal"]);
            user.isMetric = Convert.ToBoolean(dt.Rows[0]["Is_Metric"]);
            user.Password = Convert.ToString(dt.Rows[0]["User_PW"]);
            user.Birthdate = Convert.ToDateTime(dt.Rows[0]["Birthdate"]);

            return user;
        }

    /*----------------------------------ExercisePerson Queries----------------------------------*/
        public static List<DateTime> GetWorkoutDatesForMonth(User user, DateTime date)
        {
            var query = "SELECT DISTINCT Date_Scheduled FROM Exercise_Person WHERE User_Id = @userid AND " +
                "Date_Scheduled < @endofmonth AND Date_Scheduled >= @startofmonth";

            var args = new Dictionary<string, object>
            {
                {"@userid", user.UserID},
                {"@startofmonth",  date.StartOfMonth()},
                {"@endofmonth", date.EndOfMonth()}
            };

            DataTable dt = ExecuteRead(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            List<DateTime> workoutDates = new List<DateTime>();

            foreach(DataRow row in dt.Rows)
            {
                workoutDates.Add(Convert.ToDateTime(row["Date_Scheduled"]));
            }

            return workoutDates;
        }

        public static List<ExercisePerson> GetExercisesByDateAndUser(User user, DateTime date)
        {
            //Gets all ExercisePersons of the same date and user as the parameters and reutrns the result as a list

            var query = "SELECT * FROM Exercise_Person WHERE User_Id = @userid AND Date_Scheduled = @date";

            var args = new Dictionary<string, object>
            {
                {"@userid", user.UserID},
                {"@date", date}
            };

            DataTable dt = ExecuteRead(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            List<ExercisePerson> exercises = new List<ExercisePerson>();


            foreach (DataRow row in dt.Rows)
            {
                ExercisePerson temp = new ExercisePerson();
                temp.ExercisePersonID = Convert.ToInt32(row["Exercise_Person_ID"]);
                temp.ExerciseTemplateID = Convert.ToInt32(row["Exercise_Template_ID"]);
                temp.UserID = user.UserID;
                temp.DateScheduled = date;

                exercises.Add(temp);

            }

            return exercises;
        }

    /*----------------------------------Unit Queries----------------------------------*/

        //public static int AddUnit(Unit units)
        //{
        //    const string query = "INSERT INTO Units(Unit_Name, Quality_Measured, Units_System) VALUES(@unitName, @qualityMeasured, @unitSystem)";

        //    //here we are setting the parameter values that will be actually 
        //    //replaced in the query in Execute method
        //    var args = new Dictionary<string, object>
        //    {
        //        {"@unitName", units.UnitName},
        //        {"@qualityMeasured", units.QualityMeasured},
        //        {"@unitSystem", units.UnitsSystem}
        //    };

        //    return ExecuteWrite(query, args);
        //}

        //public static int EditUnit(Unit units)
        //{
        //    const string query = "UPDATE Units SET Units_Name = @unitName, Quality_Measured = @qualityMeasured, Units_System = @unitSystem WHERE Units_ID = @Unid";

        //    //here we are setting the parameter values that will be actually 
        //    //replaced in the query in Execute method
        //    var args = new Dictionary<string, object>
        //    {
        //        {"@Unid", units.UnitID},
        //        {"@unitName", units.UnitName},
        //        {"@qualityMeasured", units.QualityMeasured},
        //        {"@unitSystem", units.UnitsSystem}
        //    };

        //    return ExecuteWrite(query, args);
        //}

        //public static int DeleteUnit(Unit units)
        //{
        //    const string query = "DELETE FROM Units Units_Name = @unitName, Quality_Measured = @qualityMeasured, Units_System = @unitSystem WHERE Units_ID = @Unid";

        //    //here we are setting the parameter values that will be actually 
        //    //replaced in the query in Execute method
        //    var args = new Dictionary<string, object>
        //    {
        //        {"@Unid", units.UnitID},
        //        {"@unitName", units.UnitName},
        //        {"@qualityMeasured", units.QualityMeasured},
        //        {"@unitSystem", units.UnitsSystem}
        //    };

        //    return ExecuteWrite(query, args);
        //}

        public static List<Unit> GetAllUnits()
        {
            //Gets all the units in the database and returns as a list of Units

            var query = "SELECT * FROM Units";

            DataTable dt = ExecuteRead(query, new Dictionary<string, object>());

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            List<Unit> units = new List<Unit>();


            foreach (DataRow row in dt.Rows)
            {

                Unit temp = new Unit();
                temp.UnitID = Convert.ToInt32(row["Units_ID"]);
                temp.UnitName = Convert.ToString(row["Units_Name"]);
                temp.QualityMeasured = Convert.ToString(row["Quality_Measured"]);
                temp.UnitsSystem = Convert.ToString(row["Units_System"]);

                units.Add(temp);

            }

            return units;
        }

    /*----------------------------------MuscleGroup Queries----------------------------------*/

        //public static int AddMuscleGroup(MuscleGroup musclegroup)
        //{
        //    const string query = "INSERT INTO Muscle_Group(Muscle_Group_Name) VALUES(@musclegroupName)";

        //    //here we are setting the parameter values that will be actually 
        //    //replaced in the query in Execute method
        //    var args = new Dictionary<string, object>
        //    {
        //        {"@musclegroupName", musclegroup.MuscleGroupName}
        //    };

        //    return ExecuteWrite(query, args);
        //}

        //public static int EditMuscleGroup(MuscleGroup musclegroup)
        //{
        //    const string query = "UPDATE Muscle_Group SET Muscle_Group_Name = @musclegroupName WHERE Muscle_Group_ID = @Mgid";

        //    //here we are setting the parameter values that will be actually 
        //    //replaced in the query in Execute method
        //    var args = new Dictionary<string, object>
        //    {
        //        {"@Mgid", musclegroup.MuscleGroupID},
        //        {"@musclegroupName", musclegroup.MuscleGroupName}
        //    };

        //    return ExecuteWrite(query, args);
        //}

        //public static int DeleteMuscleGroup(MuscleGroup musclegroup)
        //{
        //    const string query = "DELETE FROM Muscle_Group Muscle_Group_Name = @musclegroupName WHERE Muscle_Group_ID = @Mgid";

        //    //here we are setting the parameter values that will be actually 
        //    //replaced in the query in Execute method
        //    var args = new Dictionary<string, object>
        //    {
        //        //{"@Unid", units.UnitID},
        //        //{"@unitName", units.UnitName},
        //        //{"@qualityMeasured", units.QualityMeasured},
        //        //{"@unitSystem", units.UnitSystem}
        //    };

        //    return ExecuteWrite(query, args);
        //}

        public static List<MuscleGroup> GetAllMuscleGroups()
        {
            //Gets all the MuscleGroups in the database and returns as a ;ist of MuscleGroups

            var query = "SELECT * FROM Muscle_Group";

            DataTable dt = ExecuteRead(query, new Dictionary<string, object>());

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            List<MuscleGroup> MuscleGroups = new List<MuscleGroup>();


            foreach (DataRow row in dt.Rows)
            {

                MuscleGroup temp = new MuscleGroup();
                temp.MuscleGroupID = Convert.ToInt32(row["Muscle_Group_ID"]);
                temp.MuscleGroupName = Convert.ToString(row["Muscle_Group_Name"]);

                MuscleGroups.Add(temp);

            }

            return MuscleGroups;
        }

        public static MuscleGroup GetMuscleGroupByExerciseTemplate(ExerciseTemplate exerciseTemplate)
        {
            var query = "SELECT * FROM Muscle_Group WHERE Muscle_Group_ID = @musclegroupID";

            var args = new Dictionary<string, object>
            {
                {"@musclegroupID", exerciseTemplate.MuscleGroupID}
            };

            DataTable dt = ExecuteRead(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            MuscleGroup muscleGroup = new MuscleGroup();

            muscleGroup.MuscleGroupID = exerciseTemplate.MuscleGroupID;
            muscleGroup.MuscleGroupName = Convert.ToString(dt.Rows[0]["Muscle_Group_Name"]);

            return muscleGroup;
        }

    /*----------------------------------ExerciseTemplate Queries----------------------------------*/

        //public static int AddExerciseTemplate(ExerciseTemplate exercisetemplate)
        //{
        //    const string query = "INSERT INTO Exercise_Templates(Exercie_Name) VALUE(@exerciseName)";

        //    var args = new Dictionary<string, object>
        //        {
        //            {"@exerciseName", exercisetemplate.ExerciseName}
        //        };

        //    return ExecuteWrite(query, args);
        //}

        //public static int EditExerciseTemplate(ExerciseTemplate exercisetemplate)
        //{
        //    const string query = "UPDATE Exercise_Templates SET Exercie_Name = @mexerciseName WHERE Exercise_ID = @Exid";

        //    //here we are setting the parameter values that will be actually 
        //    //replaced in the query in Execute method
        //    var args = new Dictionary<string, object>
        //    {
        //        {"@Exid", exercisetemplate.ExerciseID},
        //        {"@exerciseName", exercisetemplate.ExerciseName}
        //    };

        //    return ExecuteWrite(query, args);
        //}

        //public static int DeleteExerciseTemplate(ExerciseTemplate exercisetemplate)
        //{
        //    const string query = "DELETE FROM Exercise_Templates Exercie_Name = @exerciseName WHERE Exercise_ID = @Exid";

        //    //here we are setting the parameter values that will be actually 
        //    //replaced in the query in Execute method
        //    var args = new Dictionary<string, object>
        //    {
        //        {"@Exid", exercisetemplate.ExerciseID},
        //        {"@exerciseName", exercisetemplate.ExerciseName}
        //    };

        //    return ExecuteWrite(query, args);
        //}

        public static List<ExerciseTemplate> GetAllExerciseTemplates()
        {
            //Gets all the MuscleGroups in the database and returns as a ;ist of MuscleGroups

            var query = "SELECT * FROM Exercise_Templates";

            var args = new Dictionary<string, object>();


            DataTable dt = ExecuteRead(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            List<ExerciseTemplate> ExerciseTemplates = new List<ExerciseTemplate>();
            

            foreach (DataRow row in dt.Rows)
            {
                ExerciseTemplate temp = new ExerciseTemplate();
                temp.ExerciseID = Convert.ToInt32(row["Exercise_ID"]);
                temp.ExerciseName = Convert.ToString(row["Exercie_Name"]);
                temp.MuscleGroupID = Convert.ToInt32(row["MuscleGroup_ID"]);

                ExerciseTemplates.Add(temp);

            }

            return ExerciseTemplates;
        }

        public static ExerciseTemplate GetExerciseTemplateByExercisePerson(ExercisePerson exercisePerson)
        {
            //Gets all ExerciseEntries for a given ExercisePerson

            var query = "SELECT * FROM Exercise_Templates WHERE Exercise_ID = @exerciseID";

            var args = new Dictionary<string, object>
            {
                {"@exerciseID", exercisePerson.ExerciseTemplateID},
            };

            DataTable dt = ExecuteRead(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            ExerciseTemplate exerciseTemplate = new ExerciseTemplate();

            exerciseTemplate.ExerciseID = exercisePerson.ExerciseTemplateID;
            exerciseTemplate.ExerciseName = Convert.ToString(dt.Rows[0]["Exercie_Name"]);
            exerciseTemplate.MuscleGroupID = Convert.ToInt32(dt.Rows[0]["MuscleGroup_ID"]);

            return exerciseTemplate;
        }

    /*----------------------------------ExercisePerson Queries----------------------------------*/

        public static int AddExercisePerson(ExercisePerson exerciseperson)
        {
            const string query = "INSERT INTO Exercise_Person(Date_Scheduled, User_ID, Exercise_Template_ID) VALUES(@datescheduled, @UserId, @ExerciseTemplateId)";

            var args = new Dictionary<string, object>
            {
                {"@datescheduled", exerciseperson.DateScheduled},
                {"@UserId", exerciseperson.UserID},
                {"@ExerciseTemplateId", exerciseperson.ExerciseTemplateID}
            };

            return ExecuteWrite(query, args);
        }

        //public static int EditExercisePerson(ExercisePerson exerciseperson)
        //{
        //    const string query = "UPDATE Exercise_Person SET Date_Scheduled = @datescheduled WHERE Exercise_Person_ID = @ExPid";

        //    //here we are setting the parameter values that will be actually 
        //    //replaced in the query in Execute method
        //    var args = new Dictionary<string, object>
        //    {
        //        {"@ExPid", exerciseperson.ExercisePersonID},
        //        {"@datescheduled", exerciseperson.DateScheduled}
        //    };

        //    return ExecuteWrite(query, args);
        //}

        //public static int DeleteExercisePerson(ExercisePerson exerciseperson)
        //{
        //    const string query = "DELETE FROM Exercise_Person Date_Scheduled = @datescheduled WHERE Exercise_Person_ID = @ExPid";

        //    //here we are setting the parameter values that will be actually 
        //    //replaced in the query in Execute method
        //    var args = new Dictionary<string, object>
        //    {
        //        {"@ExPid", exerciseperson.ExercisePersonID},
        //        {"@datescheduled", exerciseperson.DateScheduled}
        //    };

        //    return ExecuteWrite(query, args);
        //}

        public static int DeleteExercisePersonByUser(User user)
        {
            const string query = "DELETE FROM Exercise_Person WHERE User_ID = @userID";

            //here we are setting the parameter values that will be actually 
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                {"@userID", user.UserID},
            };

            return ExecuteWrite(query, args);
        }

        public static int[] GetDistinctSetsByExercisePerson(ExercisePerson ex)
        {
            var query = "SELECT DISTINCT Set_Number FROM Exercise_Entry WHERE Exercise_Person_ID = @exercisepersonID";

            var args = new Dictionary<string, object>
            {
                {"@exercisepersonID", ex.ExercisePersonID},
            };

            DataTable dt = ExecuteRead(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            int[] setNumbers = new int[dt.Rows.Count];

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                setNumbers[i] = Convert.ToInt32(dt.Rows[i]["Set_Number"]);
            }

            return setNumbers;
        }

    /*----------------------------------ExerciseEntry Queries----------------------------------*/

        public static int AddExerciseEntry(ExerciseEntry exerciseentry)
        {
            const string query = "INSERT INTO Exercise_Entry(Exercise_Person_ID, Exercise_Unit_ID, Exercise_Entry_Value, Set_Number) " +
                "VALUES(@exercisepersonID, @exerciseunitID, @exerciseentryValue, @setnumber)";

            var args = new Dictionary<string, object>
            {
                {"@exercisepersonID", exerciseentry.ExercisePersonID },
                {"@exerciseunitID", exerciseentry.ExerciseUnitID },
                {"@exerciseentryValue", exerciseentry.ExerciseEntryValue},
                {"@setnumber", exerciseentry.SetNumber }
            };

            return ExecuteWrite(query, args);
        }

        public static int EditExerciseEntry(ExerciseEntry exerciseentry)
        {
            const string query = "UPDATE Exercise_Entry SET Exercise_Entry_Value = @exerciseentryValue WHERE Exercise_Entry_ID = @ExEid";

            //here we are setting the parameter values that will be actually 
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                {"@ExEid", exerciseentry.ExerciseEntryID},
                {"@exerciseentryValue", exerciseentry.ExerciseEntryValue}
            };

            return ExecuteWrite(query, args);
        }

        public static int DeleteExerciseEntry(ExerciseEntry exerciseentry)
        {
            const string query = "DELETE FROM Exercise_Entry WHERE Exercise_Entry_ID = @ExEid";

            //here we are setting the parameter values that will be actually 
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                {"@ExEid", exerciseentry.ExerciseEntryID},
            };

            return ExecuteWrite(query, args);
        }

        public static int DeleteExerciseEntryByUer(User user)
        {
            const string query = "DELETE FROM Exercise_Entry WHERE Exercise_Person_ID IN(SELECT Exercise_Person_ID from Exercise_Person where User_ID = @userID)";

            //here we are setting the parameter values that will be actually 
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                {"@userID", user.UserID},
            };

            return ExecuteWrite(query, args);
        }

        public static List<ExerciseEntry> GetExerciseEntriesByExercisePerson(ExercisePerson exerciseperson)
        {
            //Gets all ExerciseEntries for a given ExercisePerson

            var query = "SELECT * FROM Exercise_Entry WHERE Exercise_Person_ID = @expercisepersonID";

            var args = new Dictionary<string, object>
            {
                {"@expercisepersonID", exerciseperson.ExercisePersonID},
            };

            DataTable dt = ExecuteRead(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            List<ExerciseEntry> exerciseEntries = new List<ExerciseEntry>();


            foreach (DataRow row in dt.Rows)
            {

                ExerciseEntry temp = new ExerciseEntry();

                temp.ExerciseEntryID = Convert.ToInt32(row["Exercise_Entry_ID"]);
                temp.ExercisePersonID = exerciseperson.ExercisePersonID;
                temp.ExerciseUnitID = Convert.ToInt32(row["Exercise_Unit_ID"]);
                temp.ExerciseEntryValue = Convert.ToDouble(row["Exercise_Entry_Value"]);
                temp.SetNumber = Convert.ToInt32(row["Set_Number"]);

                exerciseEntries.Add(temp);

            }

            return exerciseEntries;
        }

    /*----------------------------------ExerciseUnit Queries----------------------------------*/

        public static List<ExerciseUnit> GetExerciseUnitByExerciseTemplate(ExerciseTemplate exercisetemplate)
        {
            //Gets all ExerciseEntries for a given ExercisePerson

            var query = "SELECT * FROM Exericse_Units WHERE Exercise_Template_ID = @exercisetemplateID";

            var args = new Dictionary<string, object>
            {
                {"@exercisetemplateID", exercisetemplate.ExerciseID},
            };

            DataTable dt = ExecuteRead(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            List<ExerciseUnit> exerciseUnits = new List<ExerciseUnit>();


            foreach (DataRow row in dt.Rows)
            {

                ExerciseUnit temp = new ExerciseUnit();

                temp.ExerciseTemplateID = exercisetemplate.ExerciseID;
                temp.UnitID = Convert.ToInt32(row["Unit_ID"]);
                temp.ExerciseUnitID = Convert.ToInt32(row["Exercise_Unit_ID"]);

                exerciseUnits.Add(temp);

            }

            return exerciseUnits;
        }
    }
}