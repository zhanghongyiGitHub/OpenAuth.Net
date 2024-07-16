﻿using System;
using System.Linq;
using Infrastructure;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace OpenAuth.Repository.Test
{
    class TestDynamicLinq : TestBase
    {
        [Test]
        public void GenerateFilter()
        {
            var dbcontext = _autofacServiceProvider.GetService<OpenAuthDBContext>();
            var json = @"
                {
	                ""Operation"": ""and"",
                                ""Filters"": [{
                                ""Key"": ""Name"",
                                ""Contrast"": ""=="",
                                ""Value"": ""admin""

                        },
                        {
                            ""Key"": ""Name"",
                                ""Contrast"": ""=="",
                                ""Value"": ""admin""
                        }
                        ],
                        ""Children"": [{
                        ""Operation"": ""or"",
                        ""Filters"": [{
                        ""Key"": ""Name"",
                        ""Contrast"": ""=="",
                        ""Value"": ""admin""
                    },
                    {
                    ""Key"": ""Name"",
                    ""Contrast"": ""=="",
                    ""Value"": ""admin""
                    }
                    ],
                    ""Children"": [


                    ]
                }

                ]
                }
                ";

            var query = dbcontext.Users.GenerateFilter("c",json);
            Console.WriteLine(query.Expression.ToString());

            Console.WriteLine(JsonHelper.Instance.Serialize(query.ToList()));
        }

        [Test]
        public void TestDynamic()
        {
            QueryObject sub = new QueryObject
            {
                Operation = "or"
            };
            sub.Filters = new[]
            {
                new Filter {Key = "Name", Value = "name", Contrast = "=="},
                new Filter {Key = "Sex", Value = "10", Contrast = "=="}
            };

            QueryObject queryObject = new QueryObject
            {
                Operation = "and"
            };
            queryObject.Filters = new[]
            {
                new Filter {Key = "Account", Value = "name", Contrast = "=="},
                new Filter {Key = "Password", Value = "10", Contrast = "=="}
            };

            queryObject.Children = new[]
            {
                sub
            };

            var dbcontext = _autofacServiceProvider.GetService<OpenAuthDBContext>();

            var query = dbcontext.Users.GenerateFilter("c",JsonHelper.Instance.Serialize(queryObject));
            Console.WriteLine(query.Expression.ToString());
        }
    }
}