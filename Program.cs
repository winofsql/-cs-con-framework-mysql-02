using System;
using System.Data.Odbc;
using System.Diagnostics;

namespace cs_console_mysql_02
{
    class Program
    {
        static void Main(string[] args)
        {

            OdbcConnection myCon = Helper.CreateConnection();

            // MySQL の処理

            // SQL
            string myQuery =
                @"SELECT * from 社員マスタ order by 社員コード";

            // SQL実行用のオブジェクトを作成
            OdbcCommand myCommand = new OdbcCommand();
            OdbcCommand updCommand = new OdbcCommand();


            // 実行用オブジェクトに必要な情報を与える
            myCommand.CommandText = myQuery;    // SQL
            myCommand.Connection = myCon;       // 接続
            updCommand.Connection = myCon;      // 更新用接続

            // 次でする、データベースの値をもらう為のオブジェクトの変数の定義
            OdbcDataReader myReader;

            // SELECT を実行した結果を取得
            myReader = myCommand.ExecuteReader();

            // myReader からデータが読みだされる間ずっとループ
            while (myReader.Read())
            {
                // 列名より列番号を取得
                int index = myReader.GetOrdinal("社員コード");
                // 列番号で、値を取得して文字列化
                string text = myReader.GetString(index);
                // 実際のコンソールに出力
                Console.WriteLine(text);
                // 出力ウインドウに出力
                Debug.WriteLine($"Debug:{text}");

                // 生年月日を日付型で取得
                index = myReader.GetOrdinal("生年月日");
                DateTime dt = myReader.GetDate(index);
                // 1日以前に変更
                dt = dt.AddDays(-1);

                // 更新用SQL を作成
                updCommand.CommandText = $@"update
                    社員マスタ
                    set 生年月日 = '{dt.ToString("yyyy/MM/dd")}'
                    where 社員コード = '{text}'";

                Debug.WriteLine($"Debug:{updCommand.CommandText}");

                updCommand.ExecuteNonQuery();

            }

            myReader.Close();

            myCon.Close();
        }

    }
}
