using System;
using System.Data.Odbc;
using System.Diagnostics;

namespace cs_console_mysql_02
{
    class Helper
    {
        static public OdbcConnection CreateConnection()
        {
            // 接続文字列の作成
            OdbcConnectionStringBuilder builder = new OdbcConnectionStringBuilder();
            builder.Driver = "MySQL ODBC 8.0 Unicode Driver";
            // 接続用のパラメータを追加
            builder.Add("server", "localhost");
            builder.Add("database", "lightbox");
            builder.Add("uid", "root");
            builder.Add("pwd", "");

            Console.WriteLine(builder.ConnectionString);
            Debug.WriteLine($"Debug:{builder.ConnectionString}");

            // 接続の作成
            OdbcConnection myCon = new OdbcConnection();

            // MySQL の接続準備完了
            myCon.ConnectionString = builder.ConnectionString;

            // MySQL に接続
            myCon.Open();

            return myCon;
        }
    }
}
