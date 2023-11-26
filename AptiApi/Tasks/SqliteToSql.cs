using Newtonsoft.Json;
using System.Collections;
using System.Data.SQLite;

namespace AptiApi.Tasks
{
    public class SqliteToSql : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<SqliteToSql> _logger;
        private Timer? _timer = null;

        public SqliteToSql(ILogger<SqliteToSql> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(" Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            List<Thread> workerThreads = new List<Thread>();
            string rootPath = "/home/uploads";
            string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly);
            int count = 1;
            foreach (string dir in dirs)
            {
                Console.WriteLine("Start save Price and Name");
                string regdate = Path.GetFileName(dir);
                string[] files = Directory.GetFiles(dir);
                //STCore.SQL.Queries Query = new STCore.SQL.Queries(new STCore.connect.DAL("Server=.;Database=Basket;Trusted_Connection=True;"), null);
                //STCore.SQL.Queries Query = new STCore.SQL.Queries(new STCore.connect.DAL("SERVER=localhost;DATABASE=Basket;User ID=sa;Password=Aj253672728282@;Pooling=False;MultipleActiveResultSets=true;"), null);
                string cs = @"URI=file:" + files[0];
                using var con = new SQLiteConnection(cs);
                con.Open();
                string stm = "SELECT * FROM products";

                using var cmd = new SQLiteCommand(stm, con);
                using SQLiteDataReader rdr = cmd.ExecuteReader();

                //int c = 0;
                //while (rdr.Read())
                //{
                //    Console.Write("\r{0}   ", c);
                //    string name = rdr.GetString(1).ToString();

                //    Query.CRUD().Execute("dbo.tbl_Products_IU_Name_Irancode", new STCore.CLS.QueryValues.Cmd_Parameters(
                //        new string[] { "Barcode", "name" },
                //        new int[] { 1, 1 },
                //        new string[] { rdr.GetInt64(0).ToString(), name }));
                //    Query.CRUD().Execute("dbo.InsertPrices", new STCore.CLS.QueryValues.Cmd_Parameters(
                //        new string[] { "barcode", "price", "finalprice", "REGDATE" },
                //        new int[] { 1, 2, 2, 2 },
                //        new string[] { rdr.GetInt64(0).ToString(), rdr.GetInt32(2).ToString(), rdr.GetInt32(3).ToString(), regdate }));
                //    c++;
                //}
                Console.WriteLine("End save Price and Name");


                foreach (var i in files)
                {
                    Thread a = new Thread(() => job(i, count));
                    workerThreads.Add(a);
                    a.Start();
                    count++;
                }


            }
            for (int k = 0; k < workerThreads.Count(); k++)
            {
                workerThreads[k].Join();

            }

            Console.WriteLine("Ended All Added");


        }

        public static void job(string file, int number)
        {
            string? date = System.IO.Directory.GetParent(file)?.Name;
            string filename = Path.GetFileName(file);
            Console.WriteLine("thread " + number.ToString() + " started");

            //STCore.SQL.Queries Query = new STCore.SQL.Queries(new STCore.connect.DAL("Server=.;Database=Basket;Trusted_Connection=True;"), null);
            //STCore.SQL.Queries Query = new STCore.SQL.Queries(new STCore.connect.DAL("SERVER=localhost;DATABASE=Basket;User ID=sa;Password=Aj253672728282@;Pooling=False;MultipleActiveResultSets=true;"), null);

            string cs = @"URI=file:" + file;
            using var con = new SQLiteConnection(cs);
            con.Open();
            string stm = "select p.*,pf.w1,pf.w2,pf.w3,pf.w4,pf.w5,pf.w6,pf.w7,pf.w8,pf.w9,pf.w10,pf.mean,pf.tolerance,pf.InsertedWeight,pf.IranCode from Products as p inner join ProductsFeatures as pf on p.barcode = pf.barcode where pf.w1 > 8";

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var irancode = "";
                if (!rdr.IsDBNull(17))
                {
                    irancode = rdr.GetString(17);
                }
                //Query.CRUD().Execute("dbo.tbl_weights_IU", new STCore.CLS.QueryValues.Cmd_Parameters(
                //    new string[] { "Barcode", "w1", "w2", "w3", "w4", "w5", "w6", "w7", "w8", "w9", "w10", "irancode" },
                //    new int[] { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
                //    new string[] { rdr.GetInt64(0).ToString(), rdr.GetInt32(4).ToString(), rdr.GetInt32(5).ToString(), rdr.GetInt32(6).ToString(), rdr.GetInt32(7).ToString(), rdr.GetInt32(8).ToString(), rdr.GetInt32(9).ToString(),
                //                rdr.GetInt32(10).ToString(), rdr.GetInt32(11).ToString(), rdr.GetInt32(12).ToString(), rdr.GetInt32(13).ToString(),irancode }));

            }
            using var con1 = new SQLiteConnection(cs);
            con1.Open();
            string stm1 = "SELECT *,(select sum(FinalPrice) from userFactor where uid = u.id) as finalprice, " +
                "(select sum(price) from userFactor where uid = u.id) as price, " +
                "(select sum(s.Counter) from(select DISTINCT barcode, Counter from userFactor where uid = u.id) as s) as productCount , " +
                "cast((select state from userLog where state > 11 and uid = u.id) as int) as finalweight " +
                "FROM user as u where productCount > 0";

            using var cmd1 = new SQLiteCommand(stm1, con1);
            using SQLiteDataReader rdr1 = cmd1.ExecuteReader();

            while (rdr1.Read())
            {
                using var con2 = new SQLiteConnection(cs);
                con2.Open();
                string stm2 = "select * from userFactor where uid = '" + rdr1.GetInt32(0) + "'";

                using var cmd2 = new SQLiteCommand(stm2, con2);
                using SQLiteDataReader rdr2 = cmd2.ExecuteReader();
                ArrayList objs = new ArrayList();
                while (rdr2.Read())
                {
                    objs.Add(new
                    {
                        barcode = rdr2.GetString(2),
                        count = rdr2.GetInt32(3),
                        price = rdr2.GetInt32(4),
                        finalprice = rdr2.GetInt32(5),
                    });
                }
                string list = JsonConvert.SerializeObject(objs);

                string fid = "";
                if (!rdr1.IsDBNull(3))
                {
                    fid = rdr1.GetString(3);
                }
                string sfid = "";
                if (!rdr1.IsDBNull(4))
                {
                    sfid = rdr1.GetString(4);
                }
                string rate = "0";
                if (!rdr1.IsDBNull(5))
                {
                    rate = rdr1.GetInt32(5).ToString();
                }

                //Query.CRUD().Execute("dbo.tbl_customerLog_I", new STCore.CLS.QueryValues.Cmd_Parameters(
                //    new string[] { "basketName", "regdate", "basketRegDate", "priod", "factorID", "suspendedFacorID", "Rate", "totalPrice", "totalFinalPrice", "productCount", "FinalWeight", "list" },
                //    new int[] { 1, 2, 2, 2, 3, 1, 4, 2, 2, 4, 2, 1 },
                //    new string[] { filename, date, date, "0", fid, sfid, rate, rdr1.GetInt32(7).ToString(), rdr1.GetInt32(6).ToString(), rdr1.GetInt32(8).ToString(), rdr1.GetInt32(9).ToString(), list }));
            }


            Console.WriteLine("thread " + number.ToString() + " ended");

        }



        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

}
