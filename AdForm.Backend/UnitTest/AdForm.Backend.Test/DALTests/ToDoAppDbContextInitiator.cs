using AdForm.DBService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdForm.Backend.Test.DALTests
{
    public class ToDoAppDbContextInitiator : MapperInitiator
    {
        public HomeworkDBContext DBContext { get; }
        public ToDoAppDbContextInitiator()
        {
            DbContextOptionsBuilder<HomeworkDBContext> builder = new DbContextOptionsBuilder<HomeworkDBContext>()
                .UseInMemoryDatabase(databaseName: "ToDoAppDb");

            HomeworkDBContext _toDoDbContext = new HomeworkDBContext(builder.Options);
            DBContext = _toDoDbContext;
        }
    }
}
