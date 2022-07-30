// See https://aka.ms/new-console-template for more information
using HRM.PasswordHashing;

string hashedPassword = PasswordHash.HashText();
Console.WriteLine(hashedPassword);
