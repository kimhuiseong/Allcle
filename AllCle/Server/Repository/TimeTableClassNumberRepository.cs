﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Server.IRepository;
using Server.Models;

namespace Server.Repository
{
    public class TimeTableClassNumberRepository : ITimeTableClassNumberRepository
    {
        private IConfiguration _config;
        private SqlConnection db;
        public TimeTableClassNumberRepository(IConfiguration config)                                             // db 설정하는 메소드
        {
            _config = config;

            // IConfiguration 개체를 통해서 
            // appsettings.json의 데이터베이스 연결 문자열을 읽어온다. 
            db = new SqlConnection(
                _config.GetSection("ConnectionStrings").GetSection(
                    "DefaultConnection").Value);
        }

        public List<TimeTableClassNumber> GetTimeTableClassNumbers(string _id, string _timeTableName)
        {
            string sql = "Select TTCN.ClassNumber " +
                         "From TimeTableClassNumber TTCN, UserTimeTable UTT " +
                         "Where UTT.TimeTableName = N'" + _timeTableName + "' " +
                         "and UTT.ID = N'" + _id+ "' " +
                         "and UTT.NO = TTCN.NO";
            return db.Query<TimeTableClassNumber>(sql).ToList();
        }


        public void PostTimeTable(TimeTableClassNumber _timeTableClassNumber)
        {
            string sql = "Insert Into UserTimeTable (NO, TimeTableClassNumber) Values (@NO, @TimeTableClassNumber)";
            db.Execute(sql, _timeTableClassNumber);
        }
    }
}
