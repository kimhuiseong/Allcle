﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Server.IRepository;
using Server.Models;

namespace Server.Repository
{
    public class SubjectRepository2 : ISubjectRepository2
    {
        private IConfiguration _config;
        private SqlConnection db;
        public SubjectRepository2(IConfiguration config)                                             // db 설정하는 메소드
        {
            _config = config;

            // IConfiguration 개체를 통해서 
            // appsettings.json의 데이터베이스 연결 문자열을 읽어온다. 
            db = new SqlConnection(
                _config.GetSection("ConnectionStrings").GetSection(
                    "DefaultConnection").Value);
        }


        public List<Subject> GetSubjects()                             //전체 모든 과목 보기
        {
            string sql = "Select * From SubjectTable2$ Order by NO Asc";
            return this.db.Query<Subject>(sql).ToList();
        }


    }
}
