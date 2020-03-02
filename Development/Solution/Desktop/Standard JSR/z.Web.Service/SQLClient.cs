using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace z.Web.Service
{
   public class SQLClient : Client
    {
       public SQLClient(string mServer) : base(mServer) {}

       public DataSet ExecuteQuery(string Query)
       {
           Model.SQLContext db = new Model.SQLContext();
           db.Qry = Query;
           string data = this.PostData("SQL/ExecuteQuery", GetJSONBytes(db));
           return this.GetDataSet<DataSet>(this.GetResult(data, "ExecuteQueryResult"));
       }

       public DataSet ExecuteQuery(string Query, string[] Parameter, object[] Value)
       {
           Model.SQLContext db = new Model.SQLContext();
           db.Qry = Query;
           db.Parameters = Parameter;
           db.Values = Value;

           string data = this.PostData("SQL/ExecuteQueryPrepared", GetJSONBytes(db));
           return this.GetDataSet<DataSet>(this.GetResult(data, "ExecuteQueryPreparedResult"));
       }

       public void ExecuteNonQuery(string Query)
       {
           try
           {
               Model.SQLContext db = new Model.SQLContext();
               db.Qry = Query;
               string data = this.PostData("SQL/ExecuteNonQuery", GetJSONBytes(db));
               this.GetResult(data, "ExecuteNonQueryResult");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void ExecuteNonQuery(string Query, string[] Parameter, object[] Value)
       {
           try
           {
               Model.SQLContext db = new Model.SQLContext();
               db.Qry = Query;
               db.Parameters = Parameter;
               db.Values = Value;
               string data = this.PostData("SQL/ExecuteNonQueryPrepared",GetJSONBytes(db));
               this.GetResult(data, "ExecuteNonQueryPreparedResult");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public object ExecuteScalar(string Query)
       {
           try
           {
               Model.SQLContext db = new Model.SQLContext();
               db.Qry = Query;
               string data = this.PostData("SQL/ExecuteScalar", GetJSONBytes(db));
               return this.GetResult(data, "ExecuteScalarResult").ResultSet;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

      public static byte[] GetJSONBytes(Model.SQLContext db)
       {
           string j = Newtonsoft.Json.JsonConvert.SerializeObject(db);
           return System.Text.ASCIIEncoding.Default.GetBytes(j);
       }

    }
}
