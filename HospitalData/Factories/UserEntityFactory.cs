using Microsoft.Data.SqlClient;
using HospitalData.Enums;
using System;

namespace HospitalData.Factories
{
    public class UserEntityFactory : IUserEntityFactory
    {
        public object[] CreateParameters(string firstName, string lastName, string email, string phone, UserType type, int? extraId)
        {
            string entityTypeString = type switch
            {
                UserType.Paciente => "Paciente",
                UserType.Medico => "Medico",
                UserType.Laboratorista => "Laboratorista",
                UserType.Administrativo => "Administrativo",
                _ => throw new ArgumentException("Rol no soportado")
            };

            return new object[]
            {
                new SqlParameter("@FirstName", firstName),
                new SqlParameter("@LastName", lastName),
                new SqlParameter("@Email", email),
                new SqlParameter("@Phone", phone ?? (object)DBNull.Value),
                new SqlParameter("@EntityType", entityTypeString),
                new SqlParameter("@SpecialtyID", extraId ?? (object)DBNull.Value)
            };
        }
    }
}