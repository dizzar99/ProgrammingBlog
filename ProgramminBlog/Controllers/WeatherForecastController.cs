using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using ProgBlog.DataAccess;
using ProgBlog.DataAccess.Test;

namespace ProgramminBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApplicationContext context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<DbTest> Get()
        {
            //var dbValue = new DbValue
            //{
            //    Value = "sdf"
            //};
            ////context.Values.InsertOne(dbValue);

            //var db = new DbValue
            //{
            //    Value = "vvv"
            //};
            ////context.Values.InsertOne(db);
            //var dbTest = new DbTest
            //{
            //    Key = "aaa",
            //    Values = new List<DbValue>
            //    {
            //        dbValue,
            //        db
            //    }
            //};

            //context.Tests.InsertOne(dbTest);

            return context.Tests.Find(d => true).ToList();
        }
    }
}
