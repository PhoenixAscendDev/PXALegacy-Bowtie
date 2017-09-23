using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

using JB2.Common;

using JB2.Bowtie.Extensions;

namespace JB2.Bowtie.ConsoleTest.V3
{
    class Program
    {
        static private BitArray convertInt8(int number)
        {
            //string s = Convert.ToString(number, 2);

            bool[] bits = Convert.ToString((int)number, 2).PadLeft(8, '0').Select(s => s.Equals('1')).ToArray();

            Array.Reverse(bits);

            BitArray result = new BitArray(bits);

            return result;
        }


        static private BitArray convertToBitArray(long number, int size = 64)
        {
            //string s = Convert.ToString(number, 2);

            bool[] bits = Convert.ToString((long)number, 2).PadLeft(size, '0').Select(s => s.Equals('1')).ToArray();

            Array.Reverse(bits);

            BitArray result = new BitArray(bits);

            return result;
        }

        static private BitArray convertToBitArray(ulong number, int size = 16)
        {
            return convertToBitArray((long)number, size);
        }

        static private T convertToNumber<T>(BitArray b)
        {
            object result = 0;

            if (typeof(T) == typeof(ulong))
            {
                var array = new byte[8];
                b.CopyTo(array, 0);

                result = BitConverter.ToUInt64(array, 0);
            }

            else if (typeof(T) == typeof(long))
            {
                var array = new byte[8];
                b.CopyTo(array, 0);

                result = BitConverter.ToInt64(array, 0);
            }

            else if (typeof(T) == typeof(uint))
            {
                var array = new byte[4];
                b.CopyTo(array, 0);

                result = BitConverter.ToUInt32(array, 0);
            }



            if (typeof(T) == typeof(int))
            {
                var array = new byte[4];
                b.CopyTo(array, 0);

                result = BitConverter.ToInt32(array, 0);
            }


            else if (typeof(T) == typeof(ushort))
            {
                var array = new byte[2];
                b.CopyTo(array, 0);

                result = BitConverter.ToUInt16(array, 0);
            }

            else if (typeof(T) == typeof(short))
            {
                var array = new byte[2];
                b.CopyTo(array, 0);

                result = BitConverter.ToInt16(array, 0);
            }

            return (T)Convert.ChangeType(result, typeof(T));


        }

        static IEnumerable<BasicDewdrop> GetStandardDewdrops()
        {
            var d1 = new BasicDewdrop();
            d1.ID = 201;
            d1.Name = "Login";
            d1.ParentGDID = 0.ToString();
            d1.ValueType = Enum.DewDropValueType.Count;
            d1.IsActive = true;

            var d2 = new BasicDewdrop();
            d2.ID = 202;
            d2.Name = "ButtonClick";
            d2.ParentGDID = 0.ToString();
            d2.ValueType = Enum.DewDropValueType.Count;
            d2.IsActive = true;

            var d3 = new BasicDewdrop();
            d3.ID = 203;
            d3.Name = "LastLoginDay";
            d3.ParentGDID = 0.ToString();
            d3.ValueType = Enum.DewDropValueType.Flags;
            d3.IsActive = true;

            var d4 = new BasicDewdrop();
            d4.ID = 204;
            d4.Name = "LastLoginTime";
            d4.ParentGDID = 0.ToString();
            d4.ValueType = Enum.DewDropValueType.Flags;
            d4.IsActive = true;

            var d5 = new BasicDewdrop();
            d5.ID = 205;
            d5.Name = "AddToInventory";
            d5.ParentGDID = 0.ToString();
            d5.ValueType = Enum.DewDropValueType.Count;
            d5.IsActive = true;


            var d6 = new BasicDewdrop();
            d6.ID = 206;
            d6.Name = "MinutesPlayed";
            d6.ParentGDID = 0.ToString();
            d6.ValueType = Enum.DewDropValueType.Flags;
            d6.IsActive = true;








            d1.ApplicationID = d2.ApplicationID = d3.ApplicationID = d4.ApplicationID = d5.ApplicationID = d6.ApplicationID = 0.ToString();


            List<BasicDewdrop> list = new List<BasicDewdrop>();
            list.Add(d1);
            list.Add(d2);
            list.Add(d3);
            list.Add(d4);
            list.Add(d5);
            list.Add(d6);




            return list;
        }

        static void InitBowtie()
        {
            var uofw = new JB2.Bowtie.UnitofWork();

            var configure = new JB2.Configure.Bowtie();

            configure.AddUnitOfWork(uofw);
            configure.AddRNG(delegate () { return 1; });
            configure.AddJsonSerializer(delegate (object obj) { return Newtonsoft.Json.JsonConvert.SerializeObject(obj); });

            configure.AddJsonDeserializer(delegate (string json, Type type) { return Newtonsoft.Json.JsonConvert.DeserializeObject(json, type); });
            configure.AddOnlineCheck(delegate () { return true; });
            configure.AddDateTimeNow(delegate () { return DateTime.UtcNow; });

            
        }

        static void dewdropAdded(DewdropData data, IDewdropEntry entry)
        {
            Console.WriteLine(entry.SubmittedDate);
        }

        static void achievementComplated(JB2.Bowtie.IAchievement achievement, IAchievementEntry entry)
        {

            Console.WriteLine(achievement.Name + ":" + entry.DateEarned);
        }

        static IEnumerable<IDewdrop> ConfigureThisThatDewdrops()
        {
            List<IDewdrop> list = new List<IDewdrop>();

            var tt1 = new BasicDewdrop();
            tt1.ID = 1;
            tt1.Name = "Vote";
            tt1.ParentGDID = "8D38169B";
            tt1.ValueType = Enum.DewDropValueType.Count;
            tt1.IsActive = true;
            tt1.GDID = "9B7616C6";


            var tt2 = new BasicDewdrop();
            tt2.ID = 2;
            tt2.Name = "ThisVote";
            tt2.ParentGDID = tt1.GDID;
            tt2.ValueType = Enum.DewDropValueType.Count;
            tt2.IsActive = true;
            tt2.GDID = "708C1604";

            var tt3 = new BasicDewdrop();
            tt3.ID = 3;
            tt3.Name = "ThatVote";
            tt3.ParentGDID = tt1.GDID;
            tt3.ValueType = Enum.DewDropValueType.Count;
            tt3.IsActive = true;
            tt3.GDID = "A51816AC";

            var tt4 = new BasicDewdrop();
            tt4.ID = 4;
            tt4.Name = "LastVoteDate";
            tt4.ParentGDID = 0.ToString();
            tt4.ValueType = Enum.DewDropValueType.DateTick;
            tt4.IsActive = true;
            tt4.GDID = "DDB01769";

            var tt5 = new BasicDewdrop();
            tt5.ID = 5;
            tt5.Name = "ConsecutiveVotes";
            tt5.ParentGDID = 0.ToString();
            tt5.ValueType = Enum.DewDropValueType.Flags;
            tt5.IsActive = true;
            tt5.GDID = "BFC2171B";




            tt1.ApplicationID = tt2.ApplicationID = tt3.ApplicationID = tt4.ApplicationID = tt5.ApplicationID = "9F0199E";

            list.Add(tt1);
            list.Add(tt2);
            list.Add(tt3);
            list.Add(tt4);
            list.Add(tt5);

            return list;


        }

        static IEnumerable<IAchievement> ConfigureThisThatAchievements()
        {
            var json = "[{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":1,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":1,\"UniqueToken\":\"86A19AC > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"86A19AC\",\"Name\":\"First Vote\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":10,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":2,\"UniqueToken\":\"39F01A29 > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"39F01A29\",\"Name\":\"10 This Votes\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":10,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":3,\"UniqueToken\":\"C58198F > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"C58198F\",\"Name\":\"10 That Votes\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":100,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":4,\"UniqueToken\":\"FAEF1947 > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"FAEF1947\",\"Name\":\"100 This Votes\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":100,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":5,\"UniqueToken\":\"3F521A16 > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"3F521A16\",\"Name\":\"100 That Votes\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":8,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":6,\"UniqueToken\":\"73301A98 > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"73301A98\",\"Name\":\"8 Votes in a Row\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":12,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":7,\"UniqueToken\":\"3B4E1A2E > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"3B4E1A2E\",\"Name\":\"Vote every month\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":50,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":8,\"UniqueToken\":\"26DC1A0D > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"26DC1A0D\",\"Name\":\"50 Votes in a Row\"}]";
            var achievements = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BasicAchievement>>(json);


            return achievements;
            
            //var vservice = JB2.Bowtie.Service.AchievementService.Instance;


            //foreach (var a in achievements)
            //{
            //    //if (a.ID == "86A19AC")
            //    //    a.StepsRequired = 1;
            //    //if (a.ID == "39F01A29")
            //    //    a.StepsRequired = 10;
            //    //if (a.ID == "C58198F")
            //    //    a.StepsRequired = 10;
            //    //if (a.ID == "FAEF1947")
            //    //    a.StepsRequired = 100;
            //    //if (a.ID == "3F521A16")
            //    //    a.StepsRequired = 100;
            //    //if (a.ID == "73301A98")
            //    //    a.StepsRequired = 8;
            //    //if (a.ID == "3B4E1A2E")
            //    //    a.StepsRequired = 12;
            //    //if (a.ID == "26DC1A0D")
            //    //    a.StepsRequired = 50;


            //    vservice.Save(a);

            //}


            
            //var json = "_items = new Dictionary<string, IAchievement>();
            //_steprules = new List<AchievementStepRule>();




            //var thisthat = aservice.RetrieveApplicationById("9F0199E").ToObject();
            //var vservice = JB2.Bowtie.Service.AchievementService.Instance;

            //var a1 = vservice.GenerateNew("First Vote", 10, thisthat, 1);
            //var a2 = vservice.GenerateNew("10 This Votes", 10, thisthat, 2);
            //var a3 = vservice.GenerateNew("10 That Votes", 10, thisthat, 3);
            //var a4 = vservice.GenerateNew("100 This Votes", 10, thisthat, 4);
            //var a5 = vservice.GenerateNew("100 That Votes", 10, thisthat, 5);
            //var a6 = vservice.GenerateNew("8 Votes in a Row", 10, thisthat, 6);
            //var a7 = vservice.GenerateNew("Vote every month", 10, thisthat, 7);
            //var a8 = vservice.GenerateNew("50 Votes in a Row", 10, thisthat, 8);
        }

        static IEnumerable<AchievementStepRule> ConfigureThisThatAchievementStepRules()
        {

            List<AchievementStepRule> list = new List<AchievementStepRule>();

            //var vservice = JB2.Bowtie.Service.AchievementService.Instance;
            var s1 = AchievementStepRule.NewDewdropIncrementRule("86A19AC", "9B7616C6"); //First Vote
            var s2 = AchievementStepRule.NewDewdropIncrementRule("39F01A29", "708C1604"); //10 This Votes
            var s3 = AchievementStepRule.NewDewdropIncrementRule("C58198F", "A51816AC");  //10 That Votes
            var s4 = AchievementStepRule.NewDewdropIncrementRule("FAEF1947", "708C1604"); //100 This Votes
            var s5 = AchievementStepRule.NewDewdropIncrementRule("3F521A16", "A51816AC"); //100 That Votes
            var s6 = AchievementStepRule.NewDewdropValueRule("73301A98", "DDB01769",Enum.Comparisons.GreaterThan, 0); //8 Votes in a Row
            //var s7 = AchievementStepRule.NewDewdropValueRule("3B4E1A2E",) //Vote every month
            var s8 = AchievementStepRule.NewDewdropValueRule("26DC1A0D", "DDB01769", Enum.Comparisons.GreaterThan, 0); //50 Votes in a Row


            list.Add(s1);
            list.Add(s2);
            list.Add(s3);
            list.Add(s4);
            list.Add(s5);
            list.Add(s6);
            list.Add(s8);


            return list;


            //vservice.Save(s1);
            //vservice.Save(s2);
            //vservice.Save(s3);
            //vservice.Save(s4);
            //vservice.Save(s5);
            //vservice.Save(s6);
            ////vservice.Save(s7);
            //vservice.Save(s8);
        }


        static void ConfigureThisThatApp()
        {
            var aservice = JB2.Bowtie.Service.ApplicationService.Instance;
            var thisthatjson = "{\"Dewdrops\":[{\"ApplicationID\":\"9F0199E\",\"GDID\":\"9B7616C6\",\"ParentGDID\":\"883C167E\",\"IsActive\":true,\"ID\":1,\"Name\":\"Vote\",\"ValueType\":0},{\"ApplicationID\":\"9F0199E\",\"GDID\":\"708C1604\",\"ParentGDID\":\"9B7616C6\",\"IsActive\":true,\"ID\":2,\"Name\":\"ThisVote\",\"ValueType\":0},{\"ApplicationID\":\"9F0199E\",\"GDID\":\"A51816AC\",\"ParentGDID\":\"9B7616C6\",\"IsActive\":true,\"ID\":3,\"Name\":\"ThatVote\",\"ValueType\":0},{\"ApplicationID\":\"9F0199E\",\"GDID\":\"DDB01769\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":4,\"Name\":\"LastVoteDate\",\"ValueType\":2},{\"ApplicationID\":\"9F0199E\",\"GDID\":\"BFC2171B\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":5,\"Name\":\"ConsecutiveVotes\",\"ValueType\":1}],\"Achievements\":[{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":1,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":1,\"UniqueToken\":\"86A19AC > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"86A19AC\",\"Name\":\"First Vote\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":10,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":2,\"UniqueToken\":\"39F01A29 > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"39F01A29\",\"Name\":\"10 This Votes\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":10,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":3,\"UniqueToken\":\"C58198F > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"C58198F\",\"Name\":\"10 That Votes\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":100,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":4,\"UniqueToken\":\"FAEF1947 > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"FAEF1947\",\"Name\":\"100 This Votes\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":100,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":5,\"UniqueToken\":\"3F521A16 > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"3F521A16\",\"Name\":\"100 That Votes\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":8,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":6,\"UniqueToken\":\"73301A98 > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"73301A98\",\"Name\":\"8 Votes in a Row\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":12,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":7,\"UniqueToken\":\"3B4E1A2E > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"3B4E1A2E\",\"Name\":\"Vote every month\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":50,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":8,\"UniqueToken\":\"26DC1A0D > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"26DC1A0D\",\"Name\":\"50 Votes in a Row\"}],\"AchievementStepRules\":[{\"AchievementID\":\"86A19AC\",\"StepType\":3,\"StepFx\":\"9B7616C6|1\",\"IsActive\":true},{\"AchievementID\":\"39F01A29\",\"StepType\":3,\"StepFx\":\"708C1604|1\",\"IsActive\":true},{\"AchievementID\":\"C58198F\",\"StepType\":3,\"StepFx\":\"A51816AC|1\",\"IsActive\":true},{\"AchievementID\":\"FAEF1947\",\"StepType\":3,\"StepFx\":\"708C1604|1\",\"IsActive\":true},{\"AchievementID\":\"3F521A16\",\"StepType\":3,\"StepFx\":\"A51816AC|1\",\"IsActive\":true},{\"AchievementID\":\"73301A98\",\"StepType\":2,\"StepFx\":\"DDB01769|1|0\",\"IsActive\":true},{\"AchievementID\":\"26DC1A0D\",\"StepType\":2,\"StepFx\":\"DDB01769|1|0\",\"IsActive\":true}],\"Website\":\"http://bluffstreet.fun/thisthat\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"UniqueToken\":\"9F0199E>*<Bowtie>*<Application\",\"Kind\":3,\"Tags\":[],\"RNG\":1,\"ID\":\"9F0199E\",\"Name\":\"ThisThat\"}";


            Bowtie.Manager.LoadApplication(thisthatjson);


            //var thisthat = aservice.ImportApplication(thisthatjson);

            //var thisthatAchievements = ConfigureThisThatAchievements();
            //var thisthatdewdrops = ConfigureThisThatDewdrops();
            //var thisthatsteps = ConfigureThisThatAchievementStepRules();


            //var aservice = JB2.Bowtie.Service.ApplicationService.Instance;
            //var vservice = JB2.Bowtie.Service.AchievementService.Instance;
            //var dservice = JB2.Bowtie.Service.DewdropService.Instance;

            //foreach(var d in thisthatdewdrops)
            //{
            //    dservice.Save(d);
            //}

            //foreach(var a in thisthatAchievements)
            //{
            //    vservice.Save(a);
            //}

            //foreach(var s in thisthatsteps)
            //{
            //    vservice.Save(s);
            //}

        }


        static void Main(string[] args)
        {
            InitBowtie();

            ConfigureThisThatApp();

           

            var uofw = new JB2.Bowtie.UnitofWork();
            var aservice = JB2.Bowtie.Service.ApplicationService.Instance;
            var vservice = JB2.Bowtie.Service.AchievementService.Instance;
            var dds = JB2.Bowtie.Service.DewdropService.Instance;

            JB2.Events.Bowtie.Instance.DewdropDataUpdated += dewdropAdded;
            JB2.Events.Bowtie.Instance.AchievementAchieved += achievementComplated;

            var JB2Company = new JB2.Common.Business();
            JB2Company.ID = "jb2-centreville";
            JB2Company.Name = "JBsquared LLC";

            var dewdrops = GetStandardDewdrops();

            //AddThisThatAchievements();

            var thisthat = aservice.RetrieveApplicationById("9F0199E").ToObject();

           // var thisthatdewdrops = dds.RetrieveByApplication(thisthat).ToObject();
            //var thisthatAchievements = vservice.RetrieveByApplication(thisthat).ToObject();

            IPlayer p = new BasicPlayer();
            p.ID = "m3";


            JB2.Bowtie.Engine.Instance.StartTimer(p);

            var dataset = DewdropData.Empty;

            var bits = (BitArray)dataset;

            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);

            var bitstr = Convert.ToBase64String(ret);

            //var bits2 = new BitArray(ret);

            Console.WriteLine(bitstr.Length);

            //Console.WriteLine(ret.Length);
            //Console.WriteLine(bits.Length);

            //Console.WriteLine();

            //Console.WriteLine(bits2.Length);

            //for(int i = 0; i < bits.Length; i++)
            //{
            //    if (bits[i] != bits2[i])
            //        Console.WriteLine(i.ToString().PadLeft(5, '0') + " : " + bits[i] + "|" + bits2[i]);
            //}

           

            //Console.WriteLine(bitstr);

            //ret = new byte[(bits.Length - 1) / 8 + 1];
            //bits2.CopyTo(ret, 0);

            //var bitstr2 = Convert.ToBase64String(ret);

            //Console.WriteLine(bitstr == bitstr2);



            //Console.WriteLine(bits == bits2);


            //for (int i = 0; i < 110; i++)
            //{
            //    var e = p.AddDewdrop(thisthat, "708C1604");
            //}


            //var achievements = p.GetAchievements(thisthat);

            //foreach(var a in achievements)
            //{
            //    var ac = vservice.RetrieveByID(a.AchievementID);

            //    if(ac)
            //    {
            //        Console.WriteLine(ac.ToObject().Name + " : " + a.Status + " : " + a.PointsEarned);
            //    }
            //}



            //var adata = AchievementData.Empty;


            //adata.SetDateAcheived(1, DateTime.UtcNow);
            //adata.SetStepValue(1, 9);
            //adata.SetPoints(1, 100);
            //adata.SetStatus(1, Enum.AchievementStatusType.Achieved);

            //Console.WriteLine(((BitArray)adata).ToBitString());

            //Console.WriteLine(adata.AchievementDataID);
            //Console.WriteLine(adata.GetDateAchieved(1));
            //Console.WriteLine(adata.GetStepValue(1));
            //Console.WriteLine(adata.GetPoints(1));

            //Console.WriteLine(adata.GetStatus(1));

            //Console.WriteLine(adata.isValid);

            //BasicApplication a = new BasicApplication();

            //a.ID = "a600dcba";
            //a.Name = "Link Fence";
            //a.Website = "http://linkfence.io";
            //a.Company = JB2Company;

            //BasicApplication a2 = new BasicApplication();
            //a2.ID = "a4cc70f2";
            //a2.Name = "FiveTwo";
            //a2.Website = "http://fivetwo.io";
            //a2.Company = JB2Company;



            //// var a3 = aservice.GenerateNewApplication("ThisThat", "http://bluffstreet.fun/thisthat", JB2Company).ToObject();

            //List<IApplication> apps = new List<IApplication>();

            //apps.Add(a);
            //apps.Add(a2);
            //apps.Add(a3);
            //Console.WriteLine(JB2.Helper.Bowtie.ConvertToJsonString(apps));

            //Console.WriteLine();

            //Console.WriteLine(JB2.Helper.Bowtie.ConvertToJsonString(thisthat.GetAchievements()));


            //Console.WriteLine();

            //Console.WriteLine(JB2.Helper.Bowtie.ConvertToJsonString(thisthat.GetDewdrops()));



            //foreach (var a in thisthat.GetAchievements())
            //{
            //    Console.WriteLine(a.Name + ":" + string.Join(",", a.GetDewdropTriggers().ToArray()));
            //}


            //Console.WriteLine();

            //Console.WriteLine(aservice.ExportApplicationToJson(thisthat).ToObject());



            //var thisthatjson = "{\"Dewdrops\":[{\"ApplicationID\":\"9F0199E\",\"GDID\":\"9B7616C6\",\"ParentGDID\":\"8D38169B\",\"IsActive\":true,\"ID\":1,\"Name\":\"Vote\",\"ValueType\":0},{\"ApplicationID\":\"9F0199E\",\"GDID\":\"708C1604\",\"ParentGDID\":\"9B7616C6\",\"IsActive\":true,\"ID\":2,\"Name\":\"ThisVote\",\"ValueType\":0},{\"ApplicationID\":\"9F0199E\",\"GDID\":\"A51816AC\",\"ParentGDID\":\"9B7616C6\",\"IsActive\":true,\"ID\":3,\"Name\":\"ThatVote\",\"ValueType\":0},{\"ApplicationID\":\"9F0199E\",\"GDID\":\"DDB01769\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":4,\"Name\":\"LastVoteDate\",\"ValueType\":2},{\"ApplicationID\":\"9F0199E\",\"GDID\":\"BFC2171B\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":5,\"Name\":\"ConsecutiveVotes\",\"ValueType\":1}],\"Achievements\":[{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":1,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":1,\"UniqueToken\":\"86A19AC > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"86A19AC\",\"Name\":\"First Vote\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":10,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":2,\"UniqueToken\":\"39F01A29 > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"39F01A29\",\"Name\":\"10 This Votes\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":10,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":3,\"UniqueToken\":\"C58198F > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"C58198F\",\"Name\":\"10 That Votes\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":100,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":4,\"UniqueToken\":\"FAEF1947 > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"FAEF1947\",\"Name\":\"100 This Votes\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":100,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":5,\"UniqueToken\":\"3F521A16 > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"3F521A16\",\"Name\":\"100 That Votes\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":8,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":6,\"UniqueToken\":\"73301A98 > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"73301A98\",\"Name\":\"8 Votes in a Row\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":12,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":7,\"UniqueToken\":\"3B4E1A2E > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"3B4E1A2E\",\"Name\":\"Vote every month\"},{\"ApplicationID\":\"9F0199E\",\"SortOrder\":0,\"AchievementType\":0,\"Category\":\"\",\"StepsRequired\":50,\"EarnedIconUrl\":null,\"HiddenIconUrl\":null,\"ShownIconUrl\":null,\"TimeBoundStart\":\"0001 - 01 - 01T00: 00:00\",\"TimeBoundEnd\":\"9999 - 12 - 31T23: 59:59.9999999\",\"Points\":10,\"StorageSlot\":8,\"UniqueToken\":\"26DC1A0D > *< Bowtie > *< Achievement\",\"Kind\":7,\"Tags\":[],\"RNG\":1,\"ID\":\"26DC1A0D\",\"Name\":\"50 Votes in a Row\"}],\"AchievementStepRules\":[{\"AchievementID\":\"86A19AC\",\"StepType\":3,\"StepFx\":\"9B7616C6|1\",\"IsActive\":true},{\"AchievementID\":\"39F01A29\",\"StepType\":3,\"StepFx\":\"708C1604|1\",\"IsActive\":true},{\"AchievementID\":\"C58198F\",\"StepType\":3,\"StepFx\":\"A51816AC | 1\",\"IsActive\":true},{\"AchievementID\":\"FAEF1947\",\"StepType\":3,\"StepFx\":\"708C1604 | 1\",\"IsActive\":true},{\"AchievementID\":\"3F521A16\",\"StepType\":3,\"StepFx\":\"A51816AC | 1\",\"IsActive\":true},{\"AchievementID\":\"73301A98\",\"StepType\":2,\"StepFx\":\"DDB01769 | 1 | 0\",\"IsActive\":true},{\"AchievementID\":\"26DC1A0D\",\"StepType\":2,\"StepFx\":\"DDB01769 | 1 | 0\",\"IsActive\":true}],\"Website\":\"http://bluffstreet.fun/thisthat\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"UniqueToken\":\"9F0199E>*<Bowtie>*<Application\",\"Kind\":3,\"Tags\":[],\"RNG\":1,\"ID\":\"9F0199E\",\"Name\":\"ThisThat\"}";



            //var test = aservice.ImportApplication(thisthatjson);



            // var dew = uofw.DewdropRepository.GetByGDID("5E0215BA");




            // //var mydataset = dds.GenerateDewdropData("LS-001", "m1");
            // //var mydataset2 = dds.GenerateDewdropData("LS-001", "m2");

            //// dds.RetrieveDewdropData("m1", "LS-001").ToObject();





            // var mydataset = p.GetDewdropData("LS-001");




            ////var e = dds.AddDewdropEntry(p,app,1, "Message", "5E0215BA");



            // var e = p.AddDewdrop(thisthat, "708C1604"); // "dds.AddDewdropEntry(p, thisthat, 1, string.Empty, "708C1604");
            // var e2 = p.AddDewdrop(thisthat, "A51816AC"); // dds.AddDewdropEntry(p, thisthat, 1, string.Empty, "A51816AC");

            // Console.WriteLine(p.GetDewdropValue(thisthat, "A51816AC"));

            // var thisthatdewdata = dds.RetrieveDewdropData(p, thisthat).ToObject();

            // var dewdroplog = p.GetDewdropLog();


            //var result = dds.SaveDewdropData("m1", "LS-001", mydataset2);




            //dataset.DewDropDataID = 5660304; // ulong.MinValue;

            // var test = BitConverter.GetBytes(ulong.MaxValue);

            // ulong foo = ulong.MinValue;
            // Console.WriteLine(Convert.ToString((long)foo, 2));



            // BitArray ba = (BitArray)dataset;

            // for (int i = 0; i < ba.Length;i++)
            // {
            //     Console.Write(ba[i] ? 1 : 0);
            // }

            // Stopwatch sw = new Stopwatch();
            // sw.Start();
            // dataset.IncrementDrewDrop(1);
            // sw.Stop();
            // Console.WriteLine(sw.Elapsed);
            // dataset.IncrementDrewDrop(2);

            // Console.WriteLine("ID -> " + dataset.DewDropDataID);
            // Console.WriteLine("Count -> " + dataset.GetDrewDropCount(1));


            // for (int i = 1; i <= 250; i++)
            // {
            //     ba = dataset.GetDrewDropRow(i);
            //     Console.Write("row " + i.ToString().PadLeft(3, '0') + " -> ");
            //     foreach (bool b in ba)
            //     {
            //         Console.Write(b ? 1 : 0);
            //     }
            //     Console.Write("\n");
            // }


            // BitArray hacked = (BitArray)dataset;

            //// hacked[] = true;

            // dataset = new DrewdropData(hacked);

            // Console.Write(dataset.Validate());





            //ushort rng = 0256;

            //int x = (rng >> ( * 3)) & 0xf;

            //Console.WriteLine(x);




            //BitArray t = new BitArray(64);

            //string s = Convert.ToString(3, 2);

            //int[] bits = s.PadLeft(8, '0') // Add 0's from left
            // .Select(c => int.Parse(c.ToString())) // convert each char to int
            // .ToArray(); // Convert IEnumerable from select to Array

            //BitArray t2 = new BitArray(8);


            //Array.Reverse(bits);

            //for (int i=0;i < 8; i++)
            //{
            //    t[10+i] = bits[i] == 1 ? true : false;


            //}




            //for (int i = 0; i < 8; i++)
            //{
            //    t2[i] = t[10+i];


            //}



            //foreach (var b in t)
            //{
            //    Console.Write( (bool)b == false ? 0 : 1);

            //}
            //Console.WriteLine();
            //foreach (var b in t2)
            //{
            //    Console.Write((bool)b == false ? 0 : 1);

            //}
            //Console.WriteLine();

            //bool[] b2 = new bool[t2.Count];
            //t2.CopyTo(b2, 0);

            //foreach (var b in b2)
            //{
            //    Console.Write((bool)b == false ? 0 : 1);

            //}

            //Console.WriteLine();

            //int[] final = new int[1];

            //t2.CopyTo(final, 0);
            //Console.WriteLine(final[0]);

            //BitArray smallInt = convertToBitArray(short.MaxValue);


            //foreach (var b in smallInt)
            //{
            //    Console.Write((bool)b == false ? 0 : 1);

            //}
            //Console.WriteLine();

            //var array = new byte[8];
            //smallInt.CopyTo(array, 0);
            //Console.WriteLine(BitConverter.ToUInt64(array, 0));

            //Console.WriteLine(convertToNumber<ulong>(smallInt));



            Console.ReadLine();

            JB2.Bowtie.Engine.Instance.PauseTimer(p);


            Console.WriteLine(p.GetDewdropValue(thisthat, "898E1697"));

            Console.ReadLine();

            JB2.Bowtie.Engine.Instance.StartTimer(p);


            Console.ReadLine();

            JB2.Bowtie.Engine.Instance.PauseTimer(p);


            Console.WriteLine(p.GetDewdropValue(thisthat, "898E1697"));

            Console.ReadLine();








        }
    }
}
