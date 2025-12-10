using Microsoft.Data.SqlClient;
using HospitalData.Enums;

namespace HospitalData.Factories
{
    public interface IUserEntityFactory
    {
        object[] CreateParameters(string firstName, string lastName, string email, string phone, UserType type, int? extraId);
    }
}