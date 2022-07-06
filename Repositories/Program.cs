using Microsoft.Data.Sqlite;
using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Repositories;
using Avaliacao3BimLp3.Models;

var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);


var modelName = args[0];
var modelAction = args[1];

if (modelName == "Student")
{
var studentRepository = new StudentRepository(databaseConfig);

switch(modelAction)
{

case "New" :
{
var registration = args[2];
var name = args[3];
var city = args[4];


var student = new Student(registration, name, city);


if(studentRepository.ExistsByRegistration(registration)){
Console.WriteLine($"Estudante com Registro {student.Registration} já existe!");
}

else {
studentRepository.Save(student);
Console.WriteLine($"Estudante {student.Name} foi cadastrado com sucesso!");
}
break;
}

case "Delete":
{
var registration = args[2];

if(studentRepository.ExistsByRegistration(registration))
{
studentRepository.Delete(registration);
Console.WriteLine($"Estudante {registration} foi removido com sucesso!");
}

else Console.WriteLine($"Estudante {registration} não foi encontrado!");

break;
}

case "MarkAsFormed":
{
var registration = args[2];

if(studentRepository.ExistsByRegistration(registration))
{
studentRepository.MarkAsFormed(registration);
Console.WriteLine($"Estudante {registration} formado!");
}

else Console.WriteLine($"Estudante {registration} não encontrado!");

break;
}

case "List" :
{
if(studentRepository.GetAll().Any())
{
Console.WriteLine("Lista de Estudantes:");

foreach (var student in studentRepository.GetAll())
{
var formed = Convert.ToString(student.Former);

if (formed == "True")
{
formed = "Formado!";
}
else formed = "Não formado!";

Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, {formed}.");
}
}
else Console.WriteLine("Nenhum estudante foi cadastrado!");

break;
}
}
}

