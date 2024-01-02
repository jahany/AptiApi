using AptinetDataAccessLibrary.DataAccess;
using AptinetDataAccessLibrary.Models;
using CliWrap;
using CliWrap.Buffered;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Data.SQLite;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AptiApi.Tasks
{
    public class SqliteToSql : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<SqliteToSql> _logger;
        private Timer? _timer = null;
        Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
        readonly AP_DBContext _db;


        public SqliteToSql(ILogger<SqliteToSql> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IServiceScopeFactory factory)
        {
            _logger = logger;
            Environment = environment;
            _db = factory.CreateScope().ServiceProvider.GetRequiredService<AP_DBContext>();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            //Console.WriteLine(p.StandardOutput.ReadToEnd());


            _logger.LogInformation(" Hosted Service running.");

            _timer = new Timer(DoWorkAsync, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private async Task DoWorkAsync(object? state)
        {
            _logger.LogInformation("test");
            List<Thread> workerThreads = new List<Thread>();
            Environment.WebRootPath = Directory.GetCurrentDirectory();
            Directory.CreateDirectory("/home/uploads");
            string uploads = Path.Combine(Environment.WebRootPath, "/home/uploads");


            string rootPath = uploads;
            string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly);
            int count = 1;
            foreach (string dir in dirs)
            {
                Console.WriteLine("Start save Price and Name");
                string regdate = Path.GetFileName(dir);
                string[] files = Directory.GetFiles(dir);
                //STCore.SQL.Queries Query = new STCore.SQL.Queries(new STCore.connect.DAL("Server=.;Database=Basket;Trusted_Connection=True;"), null);
                //STCore.SQL.Queries Query = new STCore.SQL.Queries(new STCore.connect.DAL("SERVER=localhost;DATABASE=Basket;User ID=sa;Password=Aj253672728282@;Pooling=False;MultipleActiveResultSets=true;"), null);
                //string cs123 = @"URI=file:" + files[0];
                //using var con123 = new SQLiteConnection(cs123);
                //con123.Open();
                //string stm = "SELECT * FROM products";

                //using var cmd = new SQLiteCommand(stm, con);
                //using SQLiteDataReader rdr = cmd.ExecuteReader();

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
                //Console.WriteLine("End save Price and Name");


                foreach (var file in files)
                {
                    string? date = System.IO.Directory.GetParent(file)?.Name;
                    string filename = Path.GetFileName(file);


                    string cs = @"URI=file:" + file;
                    using var con = new SQLiteConnection(cs);
                    con.Open();
                    string stm = "select pf.barcode,pf.w1,pf.w2,pf.w3,pf.w4,pf.w5,pf.w6,pf.w7,pf.w8,pf.w9,pf.w10,pf.meanWeight,pf.tolerance,pf.insertedWeight from product as pf where pf.w1 > 8";

                    using var cmd = new SQLiteCommand(stm, con);
                    using SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Weights product = new Weights();
                        product.barcode = rdr.GetString(0);
                        product.w1 = rdr.GetInt32(1);
                        product.w2 = rdr.GetInt32(2);
                        product.w3 = rdr.GetInt32(3);
                        product.w4 = rdr.GetInt32(4);
                        product.w5 = rdr.GetInt32(5);
                        product.w6 = rdr.GetInt32(6);
                        product.w7 = rdr.GetInt32(7);
                        product.w8 = rdr.GetInt32(8);
                        product.w9 = rdr.GetInt32(9);
                        product.w10 = rdr.GetInt32(10);
                        _db.Weights.Add(product);
                        _db.SaveChanges();
                    }

                    //try
                    //{
                    //    con.Close();
                    //    File.Delete(file);

                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine(ex.ToString());
                    //}

                    //Console.WriteLine("thread " + number.ToString() + " ended");
                    var result = await Cli.Wrap("/python/bin/python")
                                .WithArguments(new[] { "weights_and_irancode_correction.py", "arg1", "arg2" })
                                .WithWorkingDirectory("/app")
                                .ExecuteBufferedAsync();

                    var output = result.StandardOutput;
                    // do something with the output





                    //ProcessStartInfo start = new ProcessStartInfo();
                    //start.FileName = "/python/bin/python";
                    //start.Arguments = string.Format("{0} {1}", Directory.GetCurrentDirectory() + "\\weights_and_irancode_correction.py", "");
                    //start.UseShellExecute = false;
                    //start.RedirectStandardOutput = true;
                    //using (Process process = Process.Start(start))
                    //{
                    //    using (StreamReader reader = process.StandardOutput)
                    //    {
                    //        string result = reader.ReadToEnd();
                    //        Console.Write(result);
                    //    }
                    //}

                    try
                    {
                        File.Delete(file);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                    }
                }


            }

            Console.WriteLine("Ended All Added");


        }

        public void job(string file, int number)
        {





            //var p = new Process
            //{
            //    StartInfo =
            //        {
            //            FileName = "python",
            //            WorkingDirectory = @"C:\web",
            //            Arguments = "weights_and_irancode_correction.py"
            //        }
            //};
            //p.Start();
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
