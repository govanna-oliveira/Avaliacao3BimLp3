using Dapper;
using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Microsoft.Data.SqliteConnection;

namespace Avaliacao3BimLp3.Repositories;

class StudentRepository
{
private readonly DatabaseConfig _databaseConfig;

public StudentRepository(DatabaseConfig databaseConfig)
{
_databaseConfig = databaseConfig;
}

public Student Save(Student student)
{
using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

connection.Execute("INSERT INTO Students VALUES(@Registration, @Name, @City, @Former)", student);

return student;
}
public void Delete(string id)
{
using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
connection.Execute("DELETE FROM Students WHERE registration = @Registration;", new {Registration = id});

}

class Former
    {
    }

public void MarkAsFormed(string id)
{
using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

connection.Executute(@"
UPDATE Students
SET former = @Former
WHERE registration = @Registration;
", new {Registration = id, Former = true});
}
public List GetAll()
{
using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

var students = connection.Query("SELECT * FROM Students").ToList();

return students;
}

public bool ExistsByRegistration(string registration)
{
using (var connection = new SqliteConnection(_databaseConfig.ConnectionString))
{
var result = connection.ExecuteScalar("SELECT count(registration) FROM Students WHERE registration = @Registration;", new{Registration = registration});
return result;
}
}
}